using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void OnClickStart()
    {
        Debug.Log("押された!");
        SceneManager.LoadScene("develop-reset");
    }
}
