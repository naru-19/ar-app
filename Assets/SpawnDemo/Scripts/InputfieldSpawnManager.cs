using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputfieldSpawnManager : MonoBehaviour
{
    //InputFieldを格納するための変数
    TMP_InputField inputField;
    public Spawn spawner;


    // Start is called before the first frame update
    void Start()
    {
        //InputFieldコンポーネントを取得
        inputField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }


    //入力された名前情報を読み取ってコンソールに出力する関数
    public void SetText()
    {
        //InputFieldからテキスト情報を取得する
        spawner.load_id_object(inputField.text);

        //入力フォームのテキストを空にする
        inputField.text = "";
    }
}
