using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RaycastManager : MonoBehaviour
{
    [SerializeField]
    Transform rayStart;

    [SerializeField]
    float rayDistance;

    //[SerializeField]
    //Dance_Controller danceController;

    /* Dance Scene */
    [SerializeField]
    GameObject aodaiAnime, chuTeu, otherCharacters, danceController, spotLight;

    /* Water_Puppet Scene */
    [SerializeField]
    GameObject GroupLight1, GroupLight3, spotLight2, panel;

    /* Giant Hand Scene */
    [SerializeField]
    GameObject Sphere_Hologram, effect, Scene_Controller, timeLine_GH, TouchObject_Controller;

    [SerializeField]
    GameObject rightHand, leftHand, touchObject;

    public RightHandMovement4 r_handMovement;

    public LeftHandMovement l_handMovement;


    AudioManager audioManager;

    private float startTime;

    [SerializeField]
    AudioClip[] clips;

    [SerializeField]
    AudioSource audioSourceAnime;

    [SerializeField]
    Animator anim;

    public bool isFinished = false;

    bool isPlayed;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSourceAnime = GetComponent<AudioSource>();
        anim = aodaiAnime.GetComponent<Animator>();
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
                Debug.Log("Access1");
                //
                if (!isPlayed)
                {
                    anim.SetTrigger("StartTalking");
                    AudioSource.PlayClipAtPoint(clips[0], aodaiAnime.transform.position);                    
                    isPlayed = true;
                }                
                Destroy(aodaiAnime, clips[0].length);
                StartCoroutine(Dance());
                //isFinished = true;                                          
            }
            else if(hit.collider.tag == "ChuTeu")
            {
                Debug.Log("Access2");
                //anim.SetTrigger("StartTalking");
                if (!isPlayed)
                {
                    //audioManager.BGM.enabled = false;
                    otherCharacters.SetActive(false);
                    GroupLight1.SetActive(false);
                    GroupLight3.SetActive(false);
                    spotLight.SetActive(true);
                    AudioSource.PlayClipAtPoint(clips[0], chuTeu.transform.position);
                    isPlayed = true;
                }
                StartCoroutine(Continue());
            }
            /* TEMPORARY*/

            else if (hit.collider.tag == "TouchObject")
            {
                Debug.Log("Access3");
                ParticleSystem ps = hit.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
                if (!isPlayed)
                {
                    //Sphere_Hologram.SetActive(true);
                    this.transform.parent = rightHand.transform;
                    r_handMovement.isMoving = true;
                    Invoke("TouchObjectDisapear", 1f);
                    l_handMovement.isMoving = true;
                    isPlayed = true;
                }

                //StartCoroutine(TouchObject());
            }
            /* TEMPORARY*/
            
            else if(hit.collider.tag == "AodaiAnime_GH")
            {
                Debug.Log("Access4");
                //anim.SetTrigger("TalkStart");
                if (!isPlayed)
                {
                    AudioSource.PlayClipAtPoint(clips[0], aodaiAnime.transform.position);
                    isPlayed = true;
                }
                Destroy(aodaiAnime, clips[0].length + 1);
                //spotLight.SetActive(false);
                StartCoroutine(GH_SceneController());

            }

            else if (hit.collider.tag == "ChuTeu_LS")
            {
                Debug.Log("Access2");
                //anim.SetTrigger("StartTalking");
                if (!isPlayed)
                {
                    //audioManager.BGM.enabled = false;
                    otherCharacters.SetActive(false);
                    GroupLight1.SetActive(false);
                    GroupLight3.SetActive(false);
                    spotLight.SetActive(true);
                    AudioSource.PlayClipAtPoint(clips[0], chuTeu.transform.position);
                    isPlayed = true;
                }
                StartCoroutine(Continue_LS());
            }
        }

    }

    
    IEnumerator Dance()
    {
        yield return new WaitForSeconds(clips[0].length);
        //aodaiAnime.SetActive(false);
        spotLight.SetActive(false);
        danceController.SetActive(true);
    }

    IEnumerator Continue()
    {
        yield return new WaitForSeconds(clips[0].length);
        yield return new WaitForSeconds(1.0f);
        panel.SetActive(true);
        aodaiAnime.SetActive(true);
        spotLight2.SetActive(true);
        //yield return new WaitForSeconds(1.0f);
        AudioSource.PlayClipAtPoint(clips[1], aodaiAnime.transform.position);
    }

    IEnumerator Continue_LS()
    {
        yield return new WaitForSeconds(clips[0].length);
        panel.SetActive(true);
    }


    IEnumerator GH_SceneController()
    {

        yield return new WaitForSeconds(clips[0].length);
        spotLight.SetActive(false);
        Scene_Controller.SetActive(true);
    }

    /*
    IEnumerator TouchObject()
    {
        yield return new WaitForSeconds(1.0f);
        TouchObject_Controller.SetActive(true);
    }*/
    void TouchObjectDisapear()
    {
        Destroy(touchObject);
    }
}
