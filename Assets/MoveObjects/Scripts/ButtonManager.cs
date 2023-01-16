using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject tappedObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 押されたとき
    public void OnTapRight()
    {
        tappedObject.transform.Translate(1f, 0, 0);
    }

    public void OnTapLeft()
    {
        tappedObject.transform.Translate(-1f, 0, 0);
    }

    public void OnTapFront()
    {
        tappedObject.transform.Translate(0, 0, -1f);
    }

    public void OnTapBack()
    {
        tappedObject.transform.Translate(0, 0, 1f);
    }

    public void OnTapRotate()
    {
        tappedObject.transform.Rotate(new Vector(0, 90, 0));
    }
}
