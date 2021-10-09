using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandMovement4 : MonoBehaviour
{
    public bool isMoving = false;

    public bool isOnTarget = false;

    private float lerpTime = 35;

    private float currentLerpTime = 0;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject target;

    //private Vector3 playerPos;

    private Vector3 startPos;

    private Vector3 endPos;

    [SerializeField]
    float speed = 0.05f;

    //private Rigidbody rb;
    //[SerializeField]
    //float _progress = 0.0f;

    //AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        endPos = target.transform.position;
        //rb = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isMoving)
        {
            MovePlatform();
        }

        if (this.transform.position == endPos)
        {
            isOnTarget = true;
        }
    }

    public void MovePlatform()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float Perc = currentLerpTime / lerpTime;

        //_progress = _progress + speed * Time.deltaTime;
        this.transform.position = Vector3.Lerp(startPos, endPos, Perc);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            other.transform.parent = transform;
            //rb.useGravity = false;
            //rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            other.transform.parent = null;
        }
    }
}
