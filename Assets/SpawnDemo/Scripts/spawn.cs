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
            // ダウンロードに成功していればインスタンス化
            if(objectPrefab != null)
            {
                Instantiate(objectPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }

    public void load_id_object(string id_input)
    {
        int id = Int32.Parse(id_input);
        StartCoroutine(loader.GetBundle((GameObject obj) => {
            objectPrefab = obj;
        }, id));
    }
    // void Update()
    // {   
    //     Inputfield.GetInputid();
    //     if (String.IsNullOrWhiteSpace(id_input))
    //     {
    //         if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
    //         {
    //             return;
    //         }

    //         var hits = new List<ARRaycastHit>();
    //         if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
    //         {
    //             var hitPose = hits[0].pose;
    //             // ダウンロードに成功していればインスタンス化
    //             if(objectPrefab != null)
    //             {
    //                 Instantiate(objectPrefab, hitPose.position, hitPose.rotation);
    //             }
    //         }
    //     }
    //     else
    //     {
    //         int id = int.Parse(id_input);
    //         if(id != prev_id)
    //         {
    //             prev_id = id;
    //             Debug.Log(prev_id);
    //             StartCoroutine(loader.GetBundle((GameObject obj) => {
    //                 objectPrefab = obj;
    //             }, prev_id));
    //             if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
    //             {
    //                 return;
    //             }

    //             var hits = new List<ARRaycastHit>();
    //             if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
    //             {
    //                 var hitPose = hits[0].pose;
    //                 // ダウンロードに成功していればインスタンス化
    //                 if(objectPrefab != null)
    //                 {
    //                     Instantiate(objectPrefab, hitPose.position, hitPose.rotation);
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
    //             {
    //                 return;
    //             }

    //             var hits = new List<ARRaycastHit>();
    //             if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
    //             {
    //                 var hitPose = hits[0].pose;
    //                 // ダウンロードに成功していればインスタンス化
    //                 if(objectPrefab != null)
    //                 {
    //                     Instantiate(objectPrefab, hitPose.position, hitPose.rotation);
    //                 }
    //             }
    //         }
    //     }

    // }
}
