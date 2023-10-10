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
        LobbyConnect,
        Ingame
    };

    public TextMeshProUGUI LogText;
    public TextMeshProUGUI NickText;
    public GameObject MainUI;
    public GameObject ConnectUI;
    public GameObject LoginUI;
    public GameObject NickNameUI;
    public GameObject LobbyUI;
    public GameObject IngameUI;

    public MenuState currentState;
    MenuState tempState = MenuState.Connect;
    float TimeSet = 0f;

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
        
        if (currentState == MenuState.LobbyConnect)
        {
            TimeSet -= Time.deltaTime;
        }

        if (TimeSet < 0f)
        {
            currentState = MenuState.Ingame;
            GameManager.instance.inLobby = true;
            TimeSet = 0f;
        }
    }

    void SetMenu()
    {
        switch (currentState)
        {
            case MenuState.Connect:
                MainUI.SetActive(true);
                ConnectUI.SetActive(true);
                LoginUI.SetActive(false);
                NickNameUI.SetActive(false);
                LobbyUI.SetActive(false);
                IngameUI.SetActive(false);
                break;
            case MenuState.Login:
                MainUI.SetActive(true);
                ConnectUI.SetActive(false);
                LoginUI.SetActive(true);
                NickNameUI.SetActive(false);
                LobbyUI.SetActive(false);
                IngameUI.SetActive(false);
                break;
            case MenuState.NickName:
                MainUI.SetActive(true);
                ConnectUI.SetActive(false);
                LoginUI.SetActive(false);
                NickNameUI.SetActive(true);
                LobbyUI.SetActive(false);
                IngameUI.SetActive(false);
                break;
            case MenuState.LobbyConnect:
                MainUI.SetActive(false);
                ConnectUI.SetActive(false);
                LoginUI.SetActive(false);
                NickNameUI.SetActive(false);
                LobbyUI.SetActive(true);
                IngameUI.SetActive(false);
                TimeSet = 5f;
                break;
            case MenuState.Ingame:
                MainUI.SetActive(false);
                ConnectUI.SetActive(false);
                LoginUI.SetActive(false);
                NickNameUI.SetActive(false);
                LobbyUI.SetActive(false);
                IngameUI.SetActive(true);
                LogText.text = "";
                break;
        }
        tempState = currentState;
    }

    public void ConnectBtn()
    {
        NetworkManager.instance.Connect();        
    }

    public void LoginBtn()
    {        
        LogText.text = "로그인 중";
        GoogleSheetManager.instance.Login();
    }

    public void RegisterBtn()
    {
        LogText.text = "회원가입 중";
        GoogleSheetManager.instance.Register();
    }

    public void NickNameBtn()
    {
        LogText.text = "닉네임 생성중";
        GoogleSheetManager.instance.SetValue();
    }

}
