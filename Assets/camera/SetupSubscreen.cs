using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetupSubscreen : MonoBehaviour
{
    public RawImage subScreen;
    public float subScreenrate;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Main Screen size: {Screen.currentResolution}");
        float mainH = Screen.currentResolution.height;
        float mainW = Screen.currentResolution.width;
        Debug.Log($"{mainH}x{mainW}");
        subScreen.rectTransform.sizeDelta = new Vector2(
            mainW * subScreenrate, mainH * subScreenrate
        );
        Debug.Log($"Setup sub screen size");
        Debug.Log($"Sub screen size(WxH): {subScreen.rectTransform.sizeDelta}");

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"{Screen.currentResolution.width}");
        // int h = (int)(Screen.currentResolution.width * subScreenrate);
        // int w = (int)(Screen.currentResolution.height * subScreenrate);
        // subScreen.rectTransform.sizeDelta = new Vector2(w, h);
        // // new ReferenceEqualityComparer(0, 0, 10, 10);

        // // float x = subScreen.GetComponent<RawImage>().uvRect.x;
        // // float y = subScreen.GetComponent<RawImage>().uvRect.y;
        // Debug.Log($"Setup sub screen size");
        // Debug.Log($"Sub screen size(HxW): {h} x {w}");
        // Debug.Log($"Camera pixel size: {h} x {w}");
    }
}
