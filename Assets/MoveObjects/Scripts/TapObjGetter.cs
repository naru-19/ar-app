using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TapObjGetter : MonoBehaviour
{

    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    GameObject resizePanel;
    GameObject tappedObject;
    private Vector2 localPoint = Vector2.zero;
    public ButtonManager buttonManager;

    // Start is called before the first frame update
    void Start()
    {
        resizePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        RectTransform rc = resizePanel.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rc, touch.position, null, out localPoint);
        if (rc.rect.xMin < localPoint.x && localPoint.x < rc.rect.xMax
                && rc.rect.yMin < localPoint.y && localPoint.y < rc.rect.yMax)
        {
            return;
        }
        Ray ray = arCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;
        int layerMask = 1 << 8; // 衝突するlayerを指定する変数．https://kan-kikuchi.hatenablog.com/entry/RayCast2
        if (Physics.Raycast(ray.origin, ray.direction * 100, out hit, Mathf.Infinity, layerMask)) // もしタップ先がオブジェクトだったなら、選択する
        {
            bool taped = Physics.Raycast(ray.origin, ray.direction * 100, out hit, Mathf.Infinity);

            if (taped)
            {
                tappedObject = hit.collider.gameObject;
                if (tappedObject != null)
                {
                    buttonManager.tappedObject = this.tappedObject;
                }
            }
        }
    }


}
