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
        Debug.Log("���� ���� �Ϸ�");
        GUIManager.instance.LogText.text = "���� ���ӿϷ�";
        GUIManager.instance.currentState = MenuState.Login;        
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("���� ����");
        GUIManager.instance.LogText.text = "���� ����";        
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ����ӿϷ�");
        GUIManager.instance.NickText.text = "'" + GoogleSheetManager.instance.nickname + "'�� ȯ�� �մϴ�.";
        GUIManager.instance.currentState = MenuState.Ingame;
        GUIManager.instance.LogText.text = "�κ����ӿϷ�";
    }
}
