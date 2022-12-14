using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FeaturingObject : MonoBehaviour
{
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    Slider scaleslider;
    [SerializeField]
    GameObject resizePanel;
    public GameObject FeaturedObject;

    // Start is called before the first frame update
    void Start()
    {
        resizePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (resizePanel.activeSelf)
        {
            return;
        }
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        Ray ray = arCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;
        // https://dreameaters5239.hatenablog.com/entry/2020/05/07/205415 消してもうまくいくかも
        int layerMask = 1 << 8;
        bool success = Physics.Raycast(ray.origin, ray.direction * 100, out hit, Mathf.Infinity, layerMask);
        if (success)
        {
            // Spawnが起動していなかったら
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                resizePanel.SetActive(true);
                FeaturedObject = hit.collider.gameObject;
                // obj.transform.localScaleだと参照できないけど受け渡すと出来るらしい
                Vector3 scale = FeaturedObject.transform.localScale;
                scaleslider.value = scale.x;
                Debug.Log(scaleslider.value);
            }
        }
    }
}