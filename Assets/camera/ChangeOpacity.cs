using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeOpacity : MonoBehaviour
{
    public Material target_material;
    public Slider opacitySlider;
    private float alpha;

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
        var color = new Color(1, 1, 1, 1 - opacitySlider.value);
        target_material.SetColor("_Color", color);
        Debug.Log($"Now opacity is {1 - opacitySlider.value}");
    }
}
