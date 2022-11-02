using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
[RequireComponent(typeof(ARPointCloudManager))]
public class getDepthImage : MonoBehaviour
{
    public ARPointCloudManager manager;
    // Awake is called before Start()
    void Awake()
    {
        manager = GetComponent<ARPointCloudManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log($"{manager.trackables}");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var point in manager.trackables)
        {
            Debug.Log($"poisiton↓");
            Debug.Log(point.positions.ToString());
            Debug.Log($"position↑");
            break;
        }
    }
}
