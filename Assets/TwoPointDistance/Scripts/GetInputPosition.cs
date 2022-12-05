using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
// ジャイロセンサや加速度センサ使って手ぶれ的な影響を軽減する機能作りたい
public class TapEventManager
{
    public float timeStamp;
    public Vector3 tapPosition;
    public float depth;
    public TapEventManager(float _depth, Vector3 _tapPosition)
    {
        Debug.Log($"step-1.depth{_depth}");
        DateTime now = DateTime.Now;
        timeStamp = ConvertDatetimeToFloat(now);

        tapPosition = _tapPosition;
        depth = _depth;
    }
    public float ConvertDatetimeToFloat(DateTime datetime)
    {
        DateTime now = DateTime.Now;
        // ちょうど月が変わるタイミングの0時だけバグる。
        return now.Day * 24 * 60 * 60 * 1000 +
                now.Hour * 60 * 60 * 1000 +
                now.Minute * 60 * 1000 +
                now.Second * 1000 +
                now.Millisecond;
    }
}
public class GetInputPosition : MonoBehaviour
{
    [SerializeField]
    private float interval = 500f; // milli sec
    // Start is called before the first frame update

    [SerializeField]
    private RawImage subScreen;
    [SerializeField]
    private GameObject subScreenManager;
    public ARCameraManager CameraManager
    {
        get => _cameraManager;
        set => _cameraManager = value;
    }

    [SerializeField]
    private ARCameraManager _cameraManager;
    private List<TapEventManager> tapEventList;

    private bool rotateScreen;
    private float screenHeight; // main screen height
    private float screenWidth; // main screen width
    private float depthHeight; // depth image height
    private float depthWidth; // depth image width

    // カメラの内部パラメータ取得



    void Start()
    {
        // listの要素は最大でも1
        tapEventList = new List<TapEventManager>();
        screenHeight = Screen.currentResolution.height;
        screenWidth = Screen.currentResolution.width;
        rotateScreen = subScreenManager.GetComponent<ChangeSubScreenSize>().rotateScreen;

    }

    // Update is called once per frame
    void Update()
    {
        CameraManager.TryGetIntrinsics(out XRCameraIntrinsics intrinsics);
        var texture = subScreen.texture as Texture2D;
        if (Input.GetMouseButtonDown(0) && texture != null)
        {
            Vector3 tapPosition;
            if (rotateScreen)
            {
                depthHeight = texture.width;
                depthWidth = texture.height;
                tapPosition = new Vector3(
                    depthHeight * Input.mousePosition.y / screenHeight,
                    depthWidth - depthWidth * Input.mousePosition.x / screenWidth,
                    1.0f
                );
            }
            else
            {
                depthHeight = texture.height;// texture 90回転に注意
                depthWidth = texture.width;
                tapPosition = new Vector3(
                    depthWidth * Input.mousePosition.x / screenWidth,
                    depthHeight * Input.mousePosition.y / screenHeight,
                    1.0f
                );
            }
            Debug.Log($"input:{Input.mousePosition.x} x {Input.mousePosition.y}");
            Debug.Log($"texture wxh:{texture.width} x {texture.height}");
            float depth = texture.GetPixel(
                (int)(tapPosition.x), (int)(tapPosition.y)
            ).r;
            TapEventManager tapEvent = new TapEventManager(depth, tapPosition);
            Debug.Log($"depth is {tapEvent.depth}");
            if (tapEventList.Count > 0)
            {
                if (tapEvent.timeStamp - tapEventList[0].timeStamp < interval)
                {
                    Debug.Log($"十分な間隔をあけてtapしてください");
                }
                else
                {
                    // TODO
                    Debug.Log($"intrinsics={intrinsics}");
                    Vector2 scale = new Vector2(depthWidth / screenWidth, depthHeight / screenHeight);

                    Vector3 firstPoint = Get3DPositionFromDepth(tapEventList[0].depth, scale, tapEventList[0].tapPosition, intrinsics);
                    Vector3 secondPoint = Get3DPositionFromDepth(tapEvent.depth, scale, tapEvent.tapPosition, intrinsics);
                    Debug.Log($"first point position {firstPoint}");
                    Debug.Log($"first point position {secondPoint}");
                    Debug.Log($"The distance is {Distance(firstPoint, secondPoint)}");
                    // remove first tap event
                    tapEventList.Clear();
                }
            }
            else
            {
                tapEventList.Add(tapEvent);
                Debug.Log($"set first point");
            }
        }

        // Debug.Log($"{inputPosition}");
    }
    public Vector3 Get3DPositionFromDepth(float depth, Vector2 scale, Vector3 tapPosition, XRCameraIntrinsics intrinsics)
    {
        float fx = intrinsics.focalLength[0] * scale.x;
        float fy = intrinsics.focalLength[1] * scale.y;
        float cx = intrinsics.principalPoint[0] * scale.x;
        float cy = intrinsics.principalPoint[1] * scale.y;
        return new Vector3(
            (depth * (tapPosition.x - cx)) / fx,
            (depth * (tapPosition.y - cy)) / fy,
            depth
        );
    }
    public float Distance(Vector3 u, Vector3 v)
    {
        return Mathf.Sqrt(
            MathF.Pow(u.x - v.x, 2f) + MathF.Pow(u.y - v.y, 2f) + MathF.Pow(u.z - v.z, 2f)
        );
    }
}
