using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionSetting : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI plane_preview_button_text;
    public bool PlanePreviewSettings { get; set; }
    public PlaneDetection planedetector;

    // Start is called before the first frame update
    void Start()
    {
        InitializeOptionSettings();    
    }

    public void InitializeOptionSettings()
    {
        if (PlanePreviewSettings)
        {
            planedetector.SetAllPlaneActive();
            plane_preview_button_text.text = "ON";
        }
        else
        {
            planedetector.SetAllPlaneActive();
            plane_preview_button_text.text = "OFF";
        }
    }

    public void OnPushPlanePreviewButton()
    {
        PlanePreviewSettings = !PlanePreviewSettings;
        if (PlanePreviewSettings)
        {
            planedetector.SetAllPlaneActive();
            plane_preview_button_text.text = "ON";
        }
        else
        {
            planedetector.SetAllPlaneActive();
            plane_preview_button_text.text = "OFF";
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
