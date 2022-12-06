// using System;
// using Unity.Collections.LowLevel.Unsafe;
// using UnityEngine;
// using UnityEngine.XR.ARFoundation;
// using UnityEngine.XR.ARSubsystems;

// public class CamerImageController : MonoBehaviour
// {
//     public XRCpuImage image;
//     private void Start()
//     {

//     }

//     unsafe void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
//     {

//     }
// }

using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class CameraImageController : MonoBehaviour
{
    public ARCameraManager cameraManager;

    private Texture2D mTexture;
    private MeshRenderer mRenderer;

    private void Start()
    {
        mRenderer = GetComponent<MeshRenderer>();
    }

    void OnEnable()
    {
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }

    unsafe void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        var transformations = new List<XRCpuImage.Transformation>() { XRCpuImage.Transformation.MirrorY, XRCpuImage.Transformation.MirrorX };
        XRCpuImage image;
        if (!cameraManager.TryGetLatestImage(out image))
            return;

        var conversionParams = new XRCpuImage.ConversionParams
        (
            image,
            TextureFormat.RGBA32,
            XRCpuImage.Transformation.None
        );

        if (mTexture == null || mTexture.width != image.width || mTexture.height != image.height)
        {
            mTexture = new Texture2D(conversionParams.outputDimensions.x,
                                     conversionParams.outputDimensions.y,
                                     conversionParams.outputFormat, false);
            // Debug.Log(mTexture.alphaIsTransparency);
        }

        var buffer = mTexture.GetRawTextureData<byte>();
        image.Convert(conversionParams, new IntPtr(buffer.GetUnsafePtr()), buffer.Length);

        mTexture.Apply();
        mRenderer.material.mainTexture = mTexture;

        buffer.Dispose();
        image.Dispose();
    }
}