using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNameRot : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = cam.transform.rotation;
        //transform.LookAt(cam.transform.position);
    }
}
