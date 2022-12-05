using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PxPosition // デプスから点群に変換するための座標u,v
{
    public float u;
    public float v;
    public PxPosition(float _u, float _v)
    {
        u = _u;
        v = _v;
    }

}
public class GetTwoPointDistance : MonoBehaviour
{
    [SerializeField] private float interval = 500f; // milli sec
    // Start is called before the first frame update
    [SerializeField] private RawImage subScreen;
    public ARCameraManager CameraManager
    {
        get => _cameraManager;
        set => _cameraManager = value;
    }
    [SerializeField] private ARCameraManager _cameraManager;


    private Vector2 scale;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        CameraManager.TryGetIntrinsics(out XRCameraIntrinsics intrinsics);
        var texture = subScreen.texture as Texture2D;
        if (Input.GetMouseButtonDown(0) && texture != null)
        {
            float depthWidth = texture.height; // depthは縦横逆
            float depthHeight = texture.width;
            Debug.Log($"depth W x H ={depthWidth} x {depthHeight}");
            scale = new Vector2(
                (float)texture.height / Screen.currentResolution.width,
                (float)texture.width / Screen.currentResolution.height
            ); // subscreenのrotateの影響でtextureのwidthとheightは入れ替わっている。

            Debug.Log($"input position {Input.mousePosition}");
            // デプスを点群にする際のu,v
            // PxPosition pxPosition = new PxPosition(
            //     (int)(Input.mousePosition.y * scale.x),
            //     (int)((Screen.currentResolution.width - Input.mousePosition.x) * scale.y)
            // // (Screen.currentResolution.height - Input.mousePosition.y)// * scale.y
            // );
            PxPosition pxPosition = new PxPosition(
                (int)Input.mousePosition.x * scale.x,
                (int)((Screen.currentResolution.height - Input.mousePosition.y) * scale.y)
            );
            float depth = texture.GetPixel(
                (int)(Input.mousePosition.y * scale.y),
                (int)((Screen.currentResolution.width - Input.mousePosition.x) * scale.x)
            ).r;
            Vector3 point = Get3DpositionFromDepth(
                depth, scale, pxPosition, intrinsics, depthHeight
            );

            // Debug.Log($"scale={scale.x * 100000}");
            // Debug.Log($"surrent size {Screen.currentResolution}");
            // Debug.Log($"(u,v)=({pxPosition.u},{pxPosition.v})");
            // Debug.Log($"depth={depth}");
            Debug.Log($"point={point}");
        }
    }
    public Vector3 Get3DpositionFromDepth(
        float depth, Vector2 scale, PxPosition pxPosition, XRCameraIntrinsics intrinsics, float depthHeight
    )
    {
        float fx = intrinsics.focalLength.x * scale.x;
        float fy = intrinsics.focalLength.y * scale.y;
        float cx = intrinsics.principalPoint.x * scale.x;
        float cy = intrinsics.principalPoint.y * scale.y;
        Debug.Log($"cx x cy = {cx} x {cy},fx x fy = {fx} x {fy}");
        return new Vector3(
            depth * (pxPosition.u - cx) / fx,
            depth * (pxPosition.v - cy) / fy,
            depth
        );

    }
}
