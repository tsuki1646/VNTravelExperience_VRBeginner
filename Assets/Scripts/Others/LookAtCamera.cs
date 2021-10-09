using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera myCamera;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(transform.position + myCamera.transform.rotation * Vector3.back,
        //myCamera.transform.rotation * Vector3.up);

        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.y = transform.position.y;
        transform.LookAt(cameraPosition);
    }
}
