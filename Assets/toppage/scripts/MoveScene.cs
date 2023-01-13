using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public SceneObject m_nextScene;
    public void OnClick() //シーンの遷移
    {
        Delay Delay = new Delay(1, () =>
        {
            // delay後， ()内のシーンに遷移
            SceneManager.LoadScene(m_nextScene);
        });
        Debug.Log(m_nextScene + "への遷移ボタンが押された!");
        StartCoroutine(Delay.DelayMoving(Delay.seconds, Delay.action));
    }
}

public class Delay
{
    public float seconds;
    public Action action;
    public Delay(float _seconds, Action _action){
        seconds = _seconds;
        action = _action;
    }
    public IEnumerator DelayMoving(float seconds, Action action)
    {
        Debug.Log("start wait");
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
        Debug.Log("finish wait");
    }
}
