using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandMovement2 : MonoBehaviour
{
    bool isMoving = false;

    [SerializeField]
    GameObject cube;

    private Vector3 normalizeDirection;

    [SerializeField]
    Transform target;

    [SerializeField]
    float speed = 10.0f;


    //AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>();
        normalizeDirection = (target.position - transform.position).normalized;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isMoving)
        {
            transform.position += normalizeDirection * speed * Time.deltaTime;
            if (transform.position == target.position)
            {
                Debug.Log("GOAL");
                //transform.parent = target.transform;
                //transform.localPosition = new Vector3(0, 0, 0);
                speed = 0;
                isMoving = false;
            }
        }           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isMoving)
        {
            
            //this.transform.child = other.transform;
            //this.transform.position = cube.transform.position;
            //transform.parent = other.transform;
            //other.transform = rightHand.transform;
            other.transform.parent = this.transform;
            //other.transform.parent = cube.transform;
            other.transform.localPosition = new Vector3(0, 0, 0);
            isMoving = true;
        }
    }
}
