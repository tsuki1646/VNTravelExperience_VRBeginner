using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager2 : MonoBehaviour
{
    [SerializeField]
    Transform rayStart;

    [SerializeField]
    float rayDistance;

    //[SerializeField]
    //Dance_Controller danceController;

    [SerializeField]
    GameObject chuTeu, aodaiAnime, spotLight, danceController;

    [SerializeField]
    GameObject GroupLight1, GroupLight2, spotLight2, panel;

    AudioManager audioManager;

    private float startTime;

    [SerializeField]
    AudioClip[] clips;

    [SerializeField]
    Animator anim;

    public bool isFinished = false;

    bool isPlayed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(rayStart.position, rayStart.forward);
        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(rayStart.position, rayStart.forward * rayDistance, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Before");
            if (hit.collider.tag == "AodaiAnime")
            {
                Debug.Log("Access");

                //anim.SetTrigger("StartTalking");
                if (!isPlayed)
                {
                    AudioSource.PlayClipAtPoint(clips[0], aodaiAnime.transform.position);
                    isPlayed = true;
                }
                //Destroy(aodaiAnime, clips[0].length);
                //StartCoroutine(Dance());
            }
            else if(hit.collider.tag == "ChuTeu")
            {
                Debug.Log("Access");
                GroupLight1.SetActive(false);
                GroupLight2.SetActive(false);
                spotLight.SetActive(true);
                if (!isPlayed)
                {
                    AudioSource.PlayClipAtPoint(clips[0], chuTeu.transform.position);
                    isPlayed = true;
                }
                //StartCoroutine(AodaiAnimeAppear());
            }
        }

    }

    /*
    IEnumerator Dance()
    {
        yield return new WaitForSeconds(clips[0].length);
        aodaiAnime.SetActive(false);
        danceController.SetActive(true);
    }

    IEnumerator AodaiAnimeAppear()
    {
        yield return new WaitForSeconds(clips[0].length);
        aodaiAnime.SetActive(true);
        spotLight2.SetActive(true);
    }*/
}
