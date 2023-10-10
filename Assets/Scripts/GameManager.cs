using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool inLobby = false;

    public GameObject playerPrefab;
    public Transform playerSpawnPoint;
    public GameObject localPlayer;
    void Awake()
    {
        instance = this;    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (inLobby)
        {
            if (localPlayer != null) return;
            CreatePlayer();
        }
    }

    void CreatePlayer()
    {        
        localPlayer = Instantiate(playerPrefab, playerSpawnPoint);
        localPlayer.transform.rotation = playerSpawnPoint.rotation;
    }
}
