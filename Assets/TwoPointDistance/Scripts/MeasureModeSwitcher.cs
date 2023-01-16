using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MeasureModeSwitcher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI modeText;
    protected bool measureMode = false;
    public GameObject modeButton;
    public void pushModeSwitcher()
    {
        measureMode = !measureMode;
        if (measureMode)
        {
            modeText.text = "OFF";
            modeText.color = new Color32(50, 50, 50, 255);
            modeButton.GetComponent<Image>().color = new Color32(250, 250, 250, 255);
        }
        else
        {
            modeText.text = "ON";
            modeText.color = new Color32(250, 250, 250, 255);
            modeButton.GetComponent<Image>().color = new Color32(50, 50, 50, 255);
        }
    }

}
