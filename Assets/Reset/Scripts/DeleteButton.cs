using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Destroy(featurer);
        resizePanel.SetActive(false);
    }
}
