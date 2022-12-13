using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void OnClickStart()
    {
        Debug.Log("押された!");
        StartCoroutine(DelayMoving());
        SceneManager.LoadScene("develop-reset");
    }

    private IEnumerator DelayMoving()
    {
        yield return new WaitForSeconds(2);
    }
}
