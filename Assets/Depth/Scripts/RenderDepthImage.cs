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
        // depthを貼るtexture 
        var texture = rawImage.texture as Texture2D;
        if (texture == null || texture.width != cpuImage.width || texture.height != cpuImage.height)
        {
            texture = new Texture2D(cpuImage.width, cpuImage.height, cpuImage.format.AsTextureFormat(), false);
            rawImage.texture = texture;
        }

        // 生データはy軸でn反転しているのでレンダリング用に反転
        var conversionParams = new XRCpuImage.ConversionParams(cpuImage, cpuImage.format.AsTextureFormat(), XRCpuImage.Transformation.MirrorY);
        
        var rawTextureData = texture.GetRawTextureData<byte>();
        Debug.Assert(
            rawTextureData.Length == cpuImage.GetConvertedDataSize(
                conversionParams.outputDimensions, conversionParams.outputFormat
            ),
            "The Texture2D is not the same size as the converted data."
        );

        // cpuImageをtextureにレンダリング
        cpuImage.Convert(conversionParams, rawTextureData);
        texture.Apply();
    }
}
