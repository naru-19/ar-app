using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSizeManager : MonoBehaviour
{
    public FeaturingObject featurer;
    public Slider sizeslider;

    // Start is called before the first frame update
    void Start()
    {
        sizeslider.value = 1.0f;
    }

    public void OnValueChange()
    {
        resize(sizeslider.value, featurer.FeaturedObject);
    }

    public void resize(float size, GameObject obj)
    {
        Vector3 oldScale = obj.transform.localScale;
        obj.transform.localScale = oldScale * size;
    }

}
