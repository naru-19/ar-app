using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour
{
    [SerializeField]
    GameObject resizePanel;
    public FeaturingObject featurer;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnPushDeleteButton()
    {
        Debug.Log("----------------Push Delete Button--------------");
        Destroy(featurer.FeaturedObject);
        resizePanel.SetActive(false);
    }
}
