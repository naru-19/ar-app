using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeSubScreenSize : MonoBehaviour
{


    [SerializeField] RawImage subScreen;
    [SerializeField] Slider sizeSlider;
    [SerializeField] bool rotateScreen;
    private float screenHeight; // main screen height
    private float screenWidth; // main screen width

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.currentResolution.height;
        screenWidth = Screen.currentResolution.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateScreen)
        {
            subScreen.rectTransform.sizeDelta = new Vector2(
                screenHeight * sizeSlider.value, screenWidth * sizeSlider.value
            );
        }
        else
        {
            subScreen.rectTransform.sizeDelta = new Vector2(
                screenWidth * sizeSlider.value, screenHeight * sizeSlider.value
            );
        }
    }
}
