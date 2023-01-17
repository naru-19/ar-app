using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BundleWebLoader : MonoBehaviour
{
    private string bundleUrl = "http://192.168.10.185:8000/assetbundles/funiturebundle";
    // private string assetName = "house_v4_prefab";


    public IEnumerator GetBundle(Action<GameObject> callback, int id)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);

        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download AssetBundle");
            yield break;
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            string[] assetNames = bundle.GetAllAssetNames();
            if (id > assetNames.Length - 1)
            {
                id = 0;
            }
            // Debug.Log("↓↓↓↓↓↓↓↓↓↓↓objectArray↓↓↓↓↓↓↓↓↓↓↓↓");
            // Debug.Log("AssetName: " + assetNames[0]);
            // Debug.Log("↑↑↑↑↑↑↑↑↑↑↑objectArray↑↑↑↑↑↑↑↑↑↑↑↑");
            callback(bundle.LoadAsset(assetNames[id]) as GameObject);
            bundle.Unload(false);
        }
    }
}
