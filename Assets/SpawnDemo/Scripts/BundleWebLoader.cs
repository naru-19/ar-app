using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BundleWebLoader : MonoBehaviour
{
    private string bundleUrl = "http://192.168.10.178:8000/assetbundles/testbundle";
    private string assetName = "house_v4_prefab";

    public IEnumerator GetBundle(Action<GameObject> callback)
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
            callback(bundle.LoadAsset(assetName) as GameObject);
            bundle.Unload(false);
        }
    }
}
