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
        Debug.Log("----------------Delete button pushed--------------");
        Destroy(featurer.FeaturedObject);
        Delay Delay = new Delay(1, () =>
        {
            // delay後， Panelの非表示
            resizePanel.SetActive(false);
        });
        StartCoroutine(Delay.DelayMoving(Delay.seconds, Delay.action));
    }
}
