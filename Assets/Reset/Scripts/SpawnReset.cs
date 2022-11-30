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
    public Vector3 oldScale;

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
            Debug.Log("-----------------------------Enter activeself--------------------------");
            ResizeObject();
            return;
        }

        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            return;
        }
        Debug.Log("-----------------------------touched--------------------------");
        Touch touch = Input.GetTouch(0);
        Debug.Log("-----------------------------touched2--------------------------");
        Ray ray = arCamera.ScreenPointToRay(touch.position);
        Debug.Log("-----------------------------touched3--------------------------");

        RaycastHit hit;
        Debug.Log("-----------------------------ray0--------------------------");
        if (Physics.Raycast(ray, out hit))
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                Debug.Log("-----------------------------Enter !null--------------------------");
                return;
            }
            Debug.Log("-----------------------------Enter--------------------------");
            FeaturedObject = hit.collider.gameObject;
            oldScale = FeaturedObject.transform.localScale;
            Debug.Log("--------------FeaturedObject----------------");
            Debug.Log(FeaturedObject.name);
            Debug.Log("--------------FeaturedObject----------------");
            resizePanel.SetActive(true);
            return;
        }

        if (raycastManager.Raycast(touch.position, hitResults, TrackableType.PlaneWithinBounds))
        {
            Debug.Log("-----------------------------Enter raycast--------------------------");
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                Debug.Log("-----------------------------Enter !null--------------------------");
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
        FeaturedObject.transform.localScale = oldScale * scale;
    }

    public void EndResizeMode()
    {
        sizeSlider.value = 1.0f;
        resizePanel.SetActive(false);
    }

}