using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBird : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalSpeed;
    public float aplitude;

    public Vector3 temPosition;

    // Start is called before the first frame update
    void Start()
    {
        temPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        temPosition.x += horizontalSpeed;
        temPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * aplitude;
        transform.position = temPosition;
    }
}
