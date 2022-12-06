using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeAlpha : MonoBehaviour
{
    public Material targetMaterial;
    public Slider alphaSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Change()
    {
        var color = new Color(1, 1, 1, alphaSlider.value);
        targetMaterial.SetColor("_Color", color);
    }
}
