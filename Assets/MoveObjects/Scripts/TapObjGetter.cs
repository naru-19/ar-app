using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TapObjGetter : MonoBehaviour
{

    GameObject tapedObject;

    // Start is called before the first frame update
    void Start()
    {

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

    }


}
