using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    GameObject Testprefab; // テスト用(白い家)
    ARRaycastManager raycastManager;
    BundleWebLoader loader;

    [SerializeField]
    private Camera arCamera;

    GameObject objectPrefab;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    public TrackableType type;

    // Start is called before the first frame update
    void Start()
    {
        loader = new BundleWebLoader();
        StartCoroutine(loader.GetBundle((GameObject obj) =>
        {
            objectPrefab = obj;
        }, 0));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            return;
        }

        // ここからオブジェクト選択と配置が同時に起こらないようにするための部分（FeaturingObject.csと実装は同じ）
        Touch touch = Input.GetTouch(0);
        Ray ray = arCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;
        int layerMask = 1 << 8; // 衝突するlayerを指定する変数．https://kan-kikuchi.hatenablog.com/entry/RayCast2
        if (Physics.Raycast(ray.origin, ray.direction * 100, out hit, Mathf.Infinity, layerMask)) // もしタップ先がオブジェクトだったなら、配置しないで返す
        {
            return;
        }

        var hits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            // FeaturingObjectが起動していなかったら
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                // // prefabのダウンロードが失敗していたら例外Logを出力
                // if (objectPrefab == null)
                // {
                //     Debug.Log("------------------------[Exception] Failed to download prefab-----------------------");
                //     return;
                // }
                // Instantiate(objectPrefab, hitPose.position, hitPose.rotation);
                Instantiate(Testprefab, hitPose.position, hitPose.rotation); // 僕のスマホではダウンロードがうまくいかないので、一旦既存のオブジェクトを使ってます
            }
        }
    }

    public void load_id_object(string id_input)
    {
        // 入力文字をintに変更
        int id = Int32.Parse(id_input);
        StartCoroutine(loader.GetBundle((GameObject obj) =>
        {
            objectPrefab = obj;
        }, id));
    }
}

