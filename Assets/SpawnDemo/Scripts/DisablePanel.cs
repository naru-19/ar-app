using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisablePanel : MonoBehaviour
{
    [SerializeField]
    GameObject resizePanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void OnPushDisablePanelButton()
    {
        // パネルの非表示
        resizePanel.SetActive(false);
    }
}
