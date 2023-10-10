using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.instance.localPlayer != null)
        {
            GameObject playerObj = GameManager.instance.localPlayer;
            transform.position = playerObj.transform.position;
            transform.rotation = playerObj.transform.rotation;
        }
    }
}
