using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MeasureModeSwitcher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI modeText;
    protected bool measureMode = false;
    public void pushModeSwitcher()
    {
        measureMode = !measureMode;
        if (measureMode)
        {
            modeText.text = "OFF";

        }
        else
        {
            modeText.text = "ON";
        }
    }

}
