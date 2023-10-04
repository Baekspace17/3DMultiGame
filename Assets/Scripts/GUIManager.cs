using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;
    public enum MenuState
    {
        None,
        Connect,
        Login,
        NickName,
        Ingame
    };

    public TextMeshProUGUI LogText;
    public TextMeshProUGUI NickText;
    public GameObject MainUI;
    public GameObject ConnectUI;
    public GameObject LoginUI;
    public GameObject NickNameUI;

    public GameObject IngameUI;

    public MenuState currentState;
    MenuState tempState = MenuState.Connect;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        MainUI.SetActive(true);
        currentState = MenuState.Connect;
        SetMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != tempState)
        {
            SetMenu();
        }
    }

    void SetMenu()
    {
        switch (currentState)
        {
            case MenuState.Connect:
                ConnectUI.SetActive(true);
                LoginUI.SetActive(false);
                NickNameUI.SetActive(false);
                IngameUI.SetActive(false);
                break;
            case MenuState.Login:
                ConnectUI.SetActive(false);
                LoginUI.SetActive(true);
                NickNameUI.SetActive(false);
                IngameUI.SetActive(false);
                break;
            case MenuState.NickName:
                ConnectUI.SetActive(false);
                LoginUI.SetActive(false);
                NickNameUI.SetActive(true);
                IngameUI.SetActive(false);
                break;
            case MenuState.Ingame:
                ConnectUI.SetActive(false);
                LoginUI.SetActive(false);
                NickNameUI.SetActive(false);
                IngameUI.SetActive(true);
                break;
        }
        tempState = currentState;
    }

    public void ConnectBtn()
    {
        currentState = MenuState.Login;
        LogText.text = "접속완료";
    }

    public void LoginBtn()
    {        
        LogText.text = "로그인 중";
        GoogleSheetManager.instance.Login();
    }

    public void RegisterBtn()
    {
        GoogleSheetManager.instance.Register();
    }

    public void NickNameBtn()
    {
        GoogleSheetManager.instance.SetValue();
    }

}
