using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeAlpha : MonoBehaviour
{
    [SerializeField] private Material targetMaterial;
    [SerializeField] private Slider alphaSlider;
    public void Change()
    {
        var color = new Color(1, 1, 1, alphaSlider.value);
        targetMaterial.SetColor("_Color", color);
    }
}
