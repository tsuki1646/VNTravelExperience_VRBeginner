using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandMovement : MonoBehaviour
{
    public bool isMoving = false;

    private float lerpTime = 15;

    private float currentLerpTime = 0;

    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject effect;

    [SerializeField]
    Animator anim;

    //private Vector3 playerPos;

    private Vector3 startPos;

    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        startPos = this.transform.position;
        endPos = target.transform.position;
        //rb = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MovePlatform();
        }

        if (this.transform.position == endPos)
        {
            anim.SetTrigger("Act");
            effect.SetActive(true);
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

}
