using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RenderDepthImage : MonoBehaviour
{
    public AROcclusionManager OcclusionManager
    {
        get => _occlusionManager;
        set => _occlusionManager = value;
    }

    [SerializeField]
    private AROcclusionManager _occlusionManager;

    public RawImage RawImage
    {
        get => _rawImage;
        set => _rawImage = value;
    }

    [SerializeField]
    private RawImage _rawImage; // display depth image


    void Update()
    {
        if (OcclusionManager.TryAcquireEnvironmentDepthCpuImage(out XRCpuImage image))
        {
            using (image)
            {
                // Use the texture.
                UpdateRawImage(_rawImage, image);
            }
        }
    }

    private static void UpdateRawImage(RawImage rawImage, XRCpuImage cpuImage)
    {
        // Get the texture associated with the UI.RawImage that we wish to display on screen.
        var texture = rawImage.texture as Texture2D;  // cast

        // If the texture hasn't yet been created, or if its dimensions have changed, (re)create the texture.
        // Note: Although texture dimensions do not normally change frame-to-frame, they can change in response to
        //    a change in the camera resolution (for camera images) or changes to the quality of the human depth
        //    and human stencil buffers.
        if (texture == null || texture.width != cpuImage.width || texture.height != cpuImage.height)
        {
            texture = new Texture2D(cpuImage.width, cpuImage.height, cpuImage.format.AsTextureFormat(), false);
            rawImage.texture = texture;
        }

        // For display, we need to mirror about the vertical access.
        var conversionParams = new XRCpuImage.ConversionParams(cpuImage, cpuImage.format.AsTextureFormat(), XRCpuImage.Transformation.MirrorY);

        //Debug.Log("Texture format: " + cpuImage.format.AsTextureFormat()); -> RFloat

        // Get the Texture2D's underlying pixel buffer.
        var rawTextureData = texture.GetRawTextureData<byte>();

        // Make sure the destination buffer is large enough to hold the converted data (they should be the same size)
        Debug.Assert(
            rawTextureData.Length == cpuImage.GetConvertedDataSize(
                conversionParams.outputDimensions, conversionParams.outputFormat
            ),
            "The Texture2D is not the same size as the converted data."
        );

        // Perform the conversion.
        cpuImage.Convert(conversionParams, rawTextureData);
        // Color[] rawPixelvalue = texture.GetPixels();

        // for (int px = 0; px < 100 * 100; px++)
        // {
        //     float r = rawPixelvalue[px].r;
        //     rawPixelvalue[px] = new Color(0, 0, 0);
        // }
        // texture.SetPixels(rawPixelvalue);
        // "Apply" the new pixel data to the Texture2D.
        texture.Apply();

        // Debug.Log($"{pixels.Length}x{pixels[0]}");
    }
}
