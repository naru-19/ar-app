using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using TMPro;

// 画像内座標u,v
public class PixelPosition 
{
    public float u;
    public float v;
    public PixelPosition(float _u, float _v)
    {
        u = _u;
        v = _v;
    }

}

public class TapEvent
{
    public Vector3 pos;
    public int time;
    public TapEvent(Vector3 _pos, int _time)
    {
        pos = _pos;
        time = _time;
    }
}

public class GetTwoPointDistance : MeasureModeSwitcher/* measureModeにアクセスしたいため、継承 */
{
    [SerializeField] private float interval = 500f; // milli sec
    [SerializeField] private RawImage subScreen;
    public ARCameraManager CameraManager
    {
        get => _cameraManager;
        set => _cameraManager = value;
    }
    [SerializeField] private ARCameraManager _cameraManager;
    [SerializeField] private GameObject marker1;
    [SerializeField] private GameObject marker2;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private GameObject distanceTextObj;
    private Vector2 scale; // texture/screenのh,wそれぞれ
    private List<TapEvent> eventList = new List<TapEvent>();

    void Start()
    {
        marker1.SetActive(false);
        marker2.SetActive(false);
        distanceText.text = "";
    }

    void Update()
    {
        if (base.measureMode)
        {
            CameraManager.TryGetIntrinsics(out XRCameraIntrinsics intrinsics);
            var texture = subScreen.texture as Texture2D;
            if (Input.GetMouseButtonDown(0) && texture != null)
            {
                // 入力位置がボタンなら何もしない
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    return;
                }

                // depthは縦横逆なため(subscreenのrotateの影響でtextureのwidthとheightは入れ替わっている)
                float depthWidth = texture.height;
                float depthHeight = texture.width;
                scale = new Vector2(
                    (float)depthWidth / Screen.currentResolution.width,
                    (float)depthHeight / Screen.currentResolution.height
                );

                // depth画像内のtap位置をscaleを用いて取得
                PixelPosition pxPosition = new PixelPosition(
                    (int)Input.mousePosition.x * scale.x,
                    (int)((Screen.currentResolution.height - Input.mousePosition.y) * scale.y)
                );
                // depthを取得
                float depth = texture.GetPixel(
                    (int)(Input.mousePosition.y * scale.y),
                    (int)((Screen.currentResolution.width - Input.mousePosition.x) * scale.x)
                ).r;
                // カメラ座標系でのtap位置
                Vector3 point = Get3DpositionFromDepth(
                    depth, scale, pxPosition, intrinsics
                );
                DateTime now = DateTime.Now;

                // tap時刻
                // 日を跨いでtapすると正確なtap間隔を計算できない
                int eventTime = now.Hour * 60 * 60 * 1000
                    + now.Minute * 60 * 1000 + now.Second * 1000
                    + now.Millisecond;
                if (eventList.Count > 0)
                {
                    // 前回のtapよりinterval[ms]経過していれば距離を計算
                    int prevEventTime = eventList[0].time;
                    if (eventTime - prevEventTime > interval)
                    {
                        double distance = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            distance += Math.Pow((eventList[0].pos[i] - point[i]), 2);
                        }
                        distance = Math.Pow(distance, 0.5);
                        Debug.Log($"Two point distance is {distance * 100}[cm]");
                        SetMaker(Input.mousePosition, marker2);
                        ShowDistance(distance);
                        eventList.Clear();
                    }
                }
                else
                {
                    // 初期化処理
                    marker1.SetActive(false);
                    marker2.SetActive(false);

                    eventList.Add(
                        new TapEvent(point, eventTime)
                    );
                    distanceText.text = "";
                    SetMaker(Input.mousePosition, marker1);

                }
            }
        }
        else
        {
            marker1.SetActive(false);
            marker2.SetActive(false);
            distanceText.text = "";
        }
    }
    private Vector3 Get3DpositionFromDepth(
        float depth, Vector2 scale, PixelPosition pxPosition, XRCameraIntrinsics intrinsics
    )
    {
        /*
            depth,scale, 画像内の位置と内部パラメータをもとに3次元座標(カメラ座標)を取得
        */
        float fx = intrinsics.focalLength.x * scale.x;
        float fy = intrinsics.focalLength.y * scale.y;
        float cx = intrinsics.principalPoint.x * scale.x;
        float cy = intrinsics.principalPoint.y * scale.y;
        return new Vector3(
            depth * (pxPosition.u - cx) / fx,
            depth * (pxPosition.v - cy) / fy,
            depth
        );

    }
    private void SetMaker(Vector2 tapPosition, GameObject marker)
    {
        marker.SetActive(true);
        marker.transform.position = new Vector3(tapPosition.x, tapPosition.y, marker.transform.position.z);
    }

    private void ShowDistance(double distance)
    {
        distance *= 100;
        string text = distance.ToString("f1");
        distanceTextObj.transform.position = new Vector3(
            (marker1.transform.position.x + marker2.transform.position.x) / 2,
            (marker1.transform.position.y + marker2.transform.position.y) / 2,
            (marker1.transform.position.z + marker2.transform.position.z) / 2
        );
        distanceText.text = $"{text}[cm]";
    }
}
