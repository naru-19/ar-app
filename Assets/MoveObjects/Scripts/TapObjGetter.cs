using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TapObjGetter : MonoBehaviour
{

    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    GameObject resizePanel;
    GameObject tappedObject;
    public ButtonManager buttonManager;

    // Start is called before the first frame update
    void Start()
    {
        resizePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        Ray ray = arCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;

        bool taped = Physics.Raycast(ray.origin, ray.direction * 100, out hit, Mathf.Infinity);

        if (taped)
        {
            tappedObject = hit.collider.gameObject;
            if (tappedObject != null)
            {
                buttonManager.tappedObject = this.tappedObject;
            }
        }

    }


}
