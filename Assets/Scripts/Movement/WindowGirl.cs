using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowGirl : MonoBehaviour
{
    bool isMoving = false;
    [SerializeField]
    Vector3 direction;
    [SerializeField]
    float speed;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isMoving)
        {
            isMoving = true;
            source.Play();
            Destroy(gameObject, source.clip.length);
        }
    }
}
