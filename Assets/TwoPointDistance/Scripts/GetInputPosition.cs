using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GetInputPosition : MonoBehaviour
{
    private TapEventManager first;
    private TapEventManager second;
    [SerializeField]
    private float interval = 0.5f;
    private List<TapEventManager> tapEventList;
    // Start is called before the first frame update
    void Start()
    {
        // listの要素は最大でも1
        tapEventList = new List<TapEventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TapEventManager tapEvent = new TapEventManager();
            if (tapEventList.Count > 0)
            {
                if (tapEvent.timeStamp - tapEventList[0].timeStamp < interval)
                {
                    Debug.Log($"十分な間隔をあけてtapしてください");
                }
                else{
                    // TODO
                    
                }
            }
            else
            {
                tapEventList.Add(tapEvent);
            }
        }

        // Debug.Log($"{inputPosition}");
    }
}
