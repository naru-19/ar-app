using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnReset : MonoBehaviour
{
    [SerializeField]
    GameObject objectPrefab;

    [SerializeField]
    private Camera arCamera;

    public TrackableType type;

    ARRaycastManager raycastManager;
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    private int furnitureNum = 0;
    private string furnitureName = "furniture";

    public GameObject FeaturedObject;

    // private bool resizeMode = false;

    public GameObject resizePanel;

    public Slider sizeSlider;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        resizePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (resizePanel.activeSelf)
        {
            ResizeObject();
            return;
        }

        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        var ray = arCamera.ScreenPointToRay(touch.position);

        // RaycastHit hit;
        // if (Physics.Raycast(ray, out hit))
        // {
        //     Debug.Log("-----------------------------Enter--------------------------");
        //     FeaturedObject = hit.collider.gameObject;
        //     Debug.Log("--------------FeaturedObject----------------");
        //     Debug.Log(FeaturedObject.name);
        //     Debug.Log("--------------FeaturedObject----------------");
        //     resizePanel.SetActive(true);
        //     return;
        // }

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

    public void ResizeObject()
    {
        // var sizeSlider = GameObject.Find("SizeSlider");
        float scale = sizeSlider.value;
        FeaturedObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void EndResizeMode()
    {
        resizePanel.SetActive(false);
    }

}