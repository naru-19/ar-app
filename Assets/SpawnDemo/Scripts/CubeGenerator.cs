using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CubeGenerator : MonoBehaviour
{
    //平面に生成するオブジェクト
    [SerializeField] GameObject CubeObject;

    //ARRaycastManager
    ARRaycastManager raycastManager;

    //RaycastとPlaneが衝突した情報を格納
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    // Start is called before the first frame update
    void Start()
    {
        //ARRaycastManagerを格納する
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //画面がタッチされたかチェック
        if (Input.touchCount > 0)
        {
            //タッチ情報を格納
            Touch touch = Input.GetTouch(0);

            //タッチした位置からRayを飛ばして、Planeにヒットした情報をhitsに格納する
            if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
            {
                //Cubeを生成する
                Instantiate(CubeObject, hits[0].pose.position, Quaternion.identity);
            }
        }
    }
}
