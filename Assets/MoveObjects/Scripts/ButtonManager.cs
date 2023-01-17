using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject tappedObject;
    [SerializeField]
    float moveDistance;
    [SerializeField]
    int rotateAngle;
    [SerializeField]
    private Camera arCamera;
    Vector3 cameraForward;
    float inputHorizontal = 0;
    float inputVertical = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inputHorizontal == 0 && inputVertical == 0)
        {
            return;
        }
        cameraForward = Vector3.Scale(arCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * inputVertical + arCamera.transform.right * inputHorizontal;
        tappedObject.transform.Translate(moveForward, Space.World);
        inputHorizontal = 0;
        inputVertical = 0;
    }

    // 押されたとき
    public void OnTapRight()
    {
        //tappedObject.transform.Translate(-moveDistance, 0, 0, Space.World);
        inputHorizontal = moveDistance;
    }

    public void OnTapLeft()
    {
        //tappedObject.transform.Translate(moveDistance, 0, 0, Space.World);
        inputHorizontal = -moveDistance;
    }

    public void OnTapFront()
    {
        //tappedObject.transform.Translate(0, 0, moveDistance, Space.World);
        inputVertical = -moveDistance;
    }

    public void OnTapBack()
    {
        //tappedObject.transform.Translate(0, 0, -moveDistance, Space.World);
        inputVertical = moveDistance;
    }

    public void OnTapRotate()
    {
        tappedObject.transform.Rotate(new Vector3(0, rotateAngle, 0), Space.World);
    }
}
