using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GUIManager;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 접속 완료");
        GUIManager.instance.LogText.text = "서버 접속완료";
        GUIManager.instance.currentState = MenuState.Login;        
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("연결 끊김");
        GUIManager.instance.LogText.text = "연결 끊김";        
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비접속완료");
        GUIManager.instance.NickText.text = "'" + GoogleSheetManager.instance.nickname + "'님 환영 합니다.";
        GUIManager.instance.currentState = MenuState.Ingame;
        GUIManager.instance.LogText.text = "로비접속완료";
    }
}
