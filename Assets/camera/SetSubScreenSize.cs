using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetSubScreenSize : MonoBehaviour
{


    [SerializeField] RawImage subScreen;
    [SerializeField] Slider sizeSlider;
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
        subScreen.rectTransform.sizeDelta = new Vector2(
            screenWidth * sizeSlider.value, screenHeight * sizeSlider.value
        );
    }
}
