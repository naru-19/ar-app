using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TapObjGetter : MonoBehaviour
{

    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    GameObject EscButton;
    GameObject tappedObject;
    EventSystem eventSystem;
    public ButtonManager buttonManager;
    public MoveObject moveObject;

    // Start is called before the first frame update
    void Start()
    {
        EscButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        */

        if (Input.touchCount == 0)
        {
            return;
        }
        else if (Input.touchCount == 1)
        {
            // tapされたobjが無ければtapして獲得
            if (tappedObject == null)
            {
                //Touch touch = Input.GetTouch(0);

                Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
                //touch.position
                RaycastHit hit;

                bool taped = Physics.Raycast(ray.origin, ray.direction * 100, out hit, Mathf.Infinity);

                if (taped)
                {
                    tappedObject = hit.collider.gameObject;
                    EscButton.SetActive(true);
                    Debug.Log(tappedObject.transform.position);
                }
            }
            //when esc button is taped -> tappedObject = null
            else if (buttonManager.buttonFlag)
            {
                buttonManager.ChangeFlag();
                tappedObject = null;
                EscButton.SetActive(false);
            }
            else
            {
                Touch touch = Input.GetTouch(0);
                Debug.Log(tappedObject.transform.position);
                //Debug.Log(touch);
                moveObject.MoveObj(tappedObject);
            }
        }
        else if (Input.touchCount == 2)
        {
            // resize?
            return;
        }
        else
        {
            return;
        }

        /*
        if (Input.GetMouseButtonDown(0))
        {
            tapedObject = null;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                tapedObject = hit.collider.gameObject;
                Debug.Log("光線が当たったオブジェクトの情報");
                Debug.Log("名前：　" + tapedObject.name);
                //Debug.Log("位置：　" + tapedObject.position);
                tapedObject.GetComponent<MeshRenderer>().material.color = Color.green;
            }

        }
        */

    }


}
