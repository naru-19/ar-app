using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnReset : MonoBehaviour
{
    [SerializeField]
    GameObject objectPrefab;

    public TrackableType type;

    ARRaycastManager raycastManager;
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    private int furnitureNum = 0;
    private string furnitureName = "furniture";

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);

        if (raycastManager.Raycast(touch.position, hitResults, TrackableType.PlaneWithinBounds))
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }

            GameObject furniture = Instantiate(objectPrefab, hitResults[0].pose.position, Quaternion.identity);
            furniture.name = furnitureName + furnitureNum.ToString("00000");
            furnitureNum++;
        }
    }

    public void ResetAllObject()
    {
        while(furnitureNum > 0){
            furnitureNum--;
            GameObject obj = GameObject.Find(furnitureName + furnitureNum.ToString("00000"));
            Destroy(obj);
        }
    }

}