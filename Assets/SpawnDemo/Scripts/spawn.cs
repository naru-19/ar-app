using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class spawn : MonoBehaviour
{
    ARRaycastManager raycastManager;
    BundleWebLoader loader;

    GameObject objectPrefab;

    public delegate void Callback(Action<GameObject> gameObject);

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    public TrackableType type;

    // Start is called before the first frame update
    void Start()
    {
        loader = new BundleWebLoader();

        StartCoroutine(loader.GetBundle((GameObject obj) => {
            objectPrefab = obj;
        }));
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
            // ダウンロードに成功していればインスタンス化
            if(objectPrefab != null)
            {
                Instantiate(objectPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
}
