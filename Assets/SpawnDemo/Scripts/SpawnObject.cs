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

        var hits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            // FeaturingObjectが起動していなかったら
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                // prefabのダウンロードが失敗していたら
                // if (objectPrefab == null)
                // {
                //     return;
                // }
                // Instantiate(objectPrefab, hitPose.position, hitPose.rotation);
                Instantiate(Testprefab, hitPose.position, hitPose.rotation);
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

