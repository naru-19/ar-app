using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public bool buttonFlag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 押されたとき
    public void OnTap()
    {
        buttonFlag = true;
    }

    public void ChangeFlag()
    {
        buttonFlag = false;
    }
}
