using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputfieldSpawnManager : MonoBehaviour
{
    //InputFieldを格納するための変数
    TMP_InputField inputField;
    public SpawnObject spawner;


    // Start is called before the first frame update
    void Start()
    {
        //InputFieldコンポーネントを取得
        inputField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }

    public void SetText()
    {
        spawner.load_id_object(inputField.text);
        inputField.text = "";
    }
}
