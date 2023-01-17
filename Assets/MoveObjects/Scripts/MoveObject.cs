using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MoveObject : MonoBehaviour
{
    ARRaycastManager raycastManager;
    // List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveObj(GameObject tappedObject)
    {
        Touch touch = Input.GetTouch(0);
        var hits = new List<ARRaycastHit>();
        bool hit = raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon);
        Debug.Log(hit);
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Vector3 nextPosition = hits[0].pose.position;

            tappedObject.transform.position = nextPosition;
        }
    }

}
