using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void OnClickStart() //ARmodeへの遷移
    {
        Debug.Log("AR modeが押された!");
        StartCoroutine(DelayMoving(1, () =>
        {
            // delay後の処理 ()内のシーンに遷移
            SceneManager.LoadScene("develop-reset");
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
