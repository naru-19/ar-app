using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScaleManager : MonoBehaviour
{
    [SerializeField]
    Slider scaleslider;
    public FeaturingObject featurer;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnValueChange()
    {
        rescale(scaleslider.value, featurer.FeaturedObject);
    }

    public void rescale(float scale, GameObject obj)
    {
        obj.transform.localScale = new Vector3(scale, scale, scale);
    }

}
