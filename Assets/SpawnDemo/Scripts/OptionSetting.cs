using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionSetting : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI plane_preview_button_text;
    public GameObject plane_preview_button; 
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
            plane_preview_button_text.color = new Color32(250, 250, 250, 255);
            plane_preview_button.GetComponent<Image>().color = new Color32(50, 50, 50, 255);
        }
        else
        {
            planedetector.SetAllPlaneActive();
            plane_preview_button_text.text = "OFF";
            plane_preview_button_text.color = new Color32(50, 50, 50, 255);
            plane_preview_button.GetComponent<Image>().color = new Color32(250, 250, 250, 255);
        }
    }

    public void OnPushPlanePreviewButton()
    {
        PlanePreviewSettings = !PlanePreviewSettings;
        if (PlanePreviewSettings)
        {
            planedetector.SetAllPlaneActive();
            plane_preview_button_text.text = "ON";
            plane_preview_button_text.color = new Color32(250, 250, 250, 255);
            plane_preview_button.GetComponent<Image>().color = new Color32(50, 50, 50, 255);
        }
        else
        {
            planedetector.SetAllPlaneActive();
            plane_preview_button_text.text = "OFF";
            plane_preview_button_text.color = new Color32(50, 50, 50, 255);
            plane_preview_button.GetComponent<Image>().color = new Color32(250, 250, 250, 255);
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
