using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnPushResetButton()
    {
        // シーンの初期化 (プレハブも初期化、平面検出から始まるので一瞬のラグあり)
        SceneManager.LoadScene(0);
    }
}
