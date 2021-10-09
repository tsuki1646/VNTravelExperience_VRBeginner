using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandMovement : MonoBehaviour
{
    bool isMoving = false;

    [SerializeField]
    GameObject rightHand;

    [SerializeField]
    Vector3 direction;

    /// <summary>
    /// パラメータの変化量[1/s]
    /// 10.0f、の場合は、1.0/10.0 = 0.1[s]で遷移が完了する、という意味になる
    /// </summary>
    [SerializeField]
    float speed = 10.0f;

    // 現在の変化量
    [SerializeField]
    float _progress = 0.0f;

    AudioSource source;

    private Vector3 _startPosition = Vector3.zero;
    //private Vector3 _startPosition = rightHand.transform.position;

    private Vector3 _goalPosition = new Vector3(10, 0, 0);    

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(direction * speed * Time.deltaTime);
        // Vector3.Lerpの第三引数に与えられるパラメータは「全体の変化の割合量[1]」
        // [1] = [1/s] * [s]
        if (isMoving)
        {
            _progress = _progress + speed * Time.deltaTime;
            transform.position = Vector3.Lerp(_startPosition, _goalPosition, _progress);
        }        
    }

    void OnCollisionEnter(Collision other )
    {
        if (other.gameObject.tag =="RightHand" && !isMoving)
        {
            isMoving = true;
            transform.parent = other.transform;
            source.Play();
            Destroy(gameObject, source.clip.length);
        }
    }
}
