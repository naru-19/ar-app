using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public SceneObject m_nextScene;
    public void OnClick() //ARmodeへの遷移
    {
        Debug.Log(m_nextScene + "への遷移ボタンが押された!");
        StartCoroutine(DelayMoving(1, () =>
        {
            // delay後の処理 ()内のシーンに遷移
            SceneManager.LoadScene(m_nextScene);
        }));
    }

    private IEnumerator DelayMoving(float seconds, Action action)
    {
        Debug.Log("start wait");
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
        Debug.Log("finish wait");
    }
}
