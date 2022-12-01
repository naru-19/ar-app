using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Return : MonoBehaviour
{
    public GameObject resizePanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void OnPushReturnButton()
    {
        // パネルの非表示
        resizePanel.SetActive(false);
    }
}
