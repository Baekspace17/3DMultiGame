using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    public bool isLoginDone;
    public bool isgetValueDone;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLoginDone)
        {
            if(!isgetValueDone)
            {
                GoogleSheetManager.instance.GetValue();
                isgetValueDone = true;
            }            
        }
    }
}
