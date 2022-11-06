using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class spawn2 : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] private Camera arCamera;
    public TrackableType type;

    ARRaycastManager raycastManager;
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            var ray = arCamera.ScreenPointToRay(touch.position);

            if (raycastManager.Raycast(touch.position, hitResults, TrackableType.PlaneWithinBounds))
            {
                Instantiate(objectPrefab, hitResults[0].pose.position, Quaternion.identity);
            }

            // houseをタップした場合は削除する
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                var target = hit.collider.gameObject;
                if (target.name.Contains("house"))
                {
                    Destroy(target);
                    return;
                }
            }
        }
    }
}
