using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class GoogleData
{
    public string order, result, msg, value;
}

public class GoogleSheetManager : MonoBehaviour
{
    public static GoogleSheetManager instance;
    
    const string URL = "https://script.google.com/macros/s/AKfycbwoXXnFtsa2q1n77RyLun-ycoyQOzvRuGDm3Ni3znZe_m2jF1mQx3qY8BztkwzADaejpg/exec";
    public GoogleData GD;
    public TMP_InputField IDInput, PassInput, NickInput;
    TextMeshProUGUI LogText;
    public string id, pass, nickname;

    public bool isLoginDone;
    public bool isgetValueDone;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LogText = GUIManager.instance.LogText;
    }

    void Update()
    {
        if (isLoginDone)
        {
            if (!isgetValueDone)
            {
                GetValue();
                isgetValueDone = true;
            }
        }
    }

    bool SetIDPass()
    {
        id = IDInput.text.Trim();
        pass = PassInput.text.Trim();

        if (id == "" || pass == "") return false;
        else return true;
    }

    public void Register()
    {
        if (!SetIDPass())
        {
            LogText.text = "아이디 또는 비밀번호가 비어있습니다";
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }

    public void Login()
    {
        if (!SetIDPass())
        {
            LogText.text = "아이디 또는 비밀번호가 비어있습니다";
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("pass", pass);

        StartCoroutine(Post(form));
    }

    void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "logout");

        StartCoroutine(Post(form));
    }

    public void SetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "setValue");
        form.AddField("value", NickInput.text);

        StartCoroutine(Post(form));
    }

    public void GetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getValue");

        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

            if (www.isDone) Response(www.downloadHandler.text);
            else LogText.text = "웹의 응답이 없습니다.";
        }
    }


    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            LogText.text = GD.order + "을 실행할 수 없습니다. 에러 메시지 : " + GD.msg;
            return;
        }

        if (GD.order == "login")
        {
            isLoginDone = true;
            LogText.text = "로그인 완료";
        }

        if (GD.order == "register")
        {
            LogText.text = "회원가입 완료";
        }
        // LogText.text = GD.order + "을 실행했습니다. 메시지 : " + GD.msg;
        if (GD.order == "setValue")
        {
            isgetValueDone = false;
        }

        if (GD.order == "getValue")
        {
            if(GD.value == "")
            {
                GUIManager.instance.currentState = GUIManager.MenuState.NickName;
                LogText.text = "닉네임이 생성되지 않았거나 공백입니다.";
            }
            else
            {                
                nickname = GD.value;
                LogText.text = "닉네임 : " + nickname;
                GUIManager.instance.NickText.text = "'" + nickname + "'님 환영 합니다.";
                GUIManager.instance.currentState = GUIManager.MenuState.Ingame;
            }            
        }
    }
}