using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MoveTapedObjects : MonoBehaviour
{
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    GameObject FrontButton;
    [SerializeField]
    GameObject BackButton;
    [SerializeField]
    GameObject LeftButton;
    [SerializeField]
    GameObject RightButton;
    GameObject tapedObject;
    EventSystem eventSystem;
    GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        FrontButton.SetActive(false);
        BackButton.SetActive(false);
        LeftButton.SetActive(false);
        RightButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            return;
        }
        */

        if (Input.touchCount != 0)
        {
            Debug.Log(Input.touchCount);
        }

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        // tapされたobjが無ければtapして獲得
        if (tapedObject == null)
        {
            //Touch touch = Input.GetTouch(0);

            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            //touch.position
            RaycastHit hit;

            bool taped = Physics.Raycast(ray.origin, ray.direction * 100, out hit, Mathf.Infinity);

            if (taped)
            {
                tapedObject = hit.collider.gameObject;
                FrontButton.SetActive(true);
                BackButton.SetActive(true);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
            }
        }
        // tapされたobjがあればbutton操作で移動
        else
        {




            try
            {
                eventSystem = EventSystem.current;
                button = eventSystem.currentSelectedGameObject.gameObject;
                if (button == FrontButton)
                {
                    tapedObject.transform.Translate(0, 0, -1f);
                }
                if (button == BackButton)
                {
                    tapedObject.transform.Translate(0, 0, 1f);
                }
                if (button == LeftButton)
                {
                    tapedObject.transform.Translate(-1f, 0, 0);
                }
                if (button == RightButton)
                {
                    tapedObject.transform.Translate(1f, 0, 0);
                }
            }
            catch (System.NullReferenceException ex)
            {
                //Touch touch = Input.GetTouch(0);

                Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
                //touch.position
                RaycastHit hit;

                bool taped = Physics.Raycast(ray.origin, ray.direction * 100, out hit, Mathf.Infinity);

                if (!taped)
                {
                    tapedObject = null;
                    FrontButton.SetActive(false);
                    BackButton.SetActive(false);
                    LeftButton.SetActive(false);
                    RightButton.SetActive(false);
                }
            }


            /*
            switch (button)
            {
                case FrontButton:
                    tapedObject.transform.Translate(0, 0, -1f);
                    break;
                case BackButton:
                    tapedObject.transform.Translate(0, 0, 1f);
                    break;
                case LeftButton:
                    tapedObject.transform.Translate(-1f, 0, 0);
                    break;
                case RightButton:
                    tapedObject.transform.Translate(1f, 0, 0);
                    break;
            }
            */


        }


    }


}
