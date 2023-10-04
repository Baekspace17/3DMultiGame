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
            LogText.text = "���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�";
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
            LogText.text = "���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�";
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
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
        {
            yield return www.SendWebRequest();

            if (www.isDone) Response(www.downloadHandler.text);
            else LogText.text = "���� ������ �����ϴ�.";
        }
    }


    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        GD = JsonUtility.FromJson<GoogleData>(json);

        if (GD.result == "ERROR")
        {
            LogText.text = GD.order + "�� ������ �� �����ϴ�. ���� �޽��� : " + GD.msg;
            return;
        }

        if (GD.order == "login")
        {
            isLoginDone = true;
            LogText.text = "�α��� �Ϸ�";
        }

        if (GD.order == "register")
        {
            LogText.text = "ȸ������ �Ϸ�";
        }
        // LogText.text = GD.order + "�� �����߽��ϴ�. �޽��� : " + GD.msg;
        if (GD.order == "setValue")
        {
            isgetValueDone = false;
        }

        if (GD.order == "getValue")
        {
            if(GD.value == "")
            {
                GUIManager.instance.currentState = GUIManager.MenuState.NickName;
                LogText.text = "�г����� �������� �ʾҰų� �����Դϴ�.";
            }
            else
            {                
                nickname = GD.value;
                LogText.text = "�г��� : " + nickname;
                GUIManager.instance.NickText.text = "'" + nickname + "'�� ȯ�� �մϴ�.";
                GUIManager.instance.currentState = GUIManager.MenuState.Ingame;
            }            
        }
    }
}