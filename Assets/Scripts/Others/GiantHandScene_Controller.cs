using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GiantHandScene_Controller : MonoBehaviour
{
    [SerializeField]
    float endingTime;

    [SerializeField]
    GameObject boat1, boat_Anime, aodai, aodaiAnime_GH, lotus, effect_1, effect_2,spotLight, directionalLight, phatTo, quanAm;

    [SerializeField]
    GameObject Rock5A, Rock2;

    [SerializeField]
    GameObject petal_01, petal_02, petal_12, petal_14;

    [SerializeField]
    GameObject First_camera, Second_camera, Third_camera, visual;

    [SerializeField]
    GameObject R_Hand_1, R_Hand_2, panel;

    [SerializeField]
    OVRScreenFade screenFade;

    [SerializeField]
    float fadeOutTime, fadeInTime;

    [SerializeField]
    float fadeTime;

    [SerializeField]
    float startLightIntensity;

    [SerializeField]
    //AudioSource BGM;
    GameObject BGM;

    [SerializeField]
    Animator anim_petal_01, anim_petal_02, anim_petal_12, anim_petal_14;

    [SerializeField]
    Animator anim_Rock5A, anim_Rock2;

    //UIManager isComeBack;

    RightHandMovement4 isMoving;

    RightHandMovement4 isOnTarget;

    public bool isOnTheHand;

    void Awake()
    {
        anim_Rock5A = Rock5A.GetComponent<Animator>();
        anim_Rock2 = Rock2.GetComponent<Animator>();

        anim_petal_01 = petal_01.GetComponent<Animator>();
        anim_petal_02 = petal_02.GetComponent<Animator>();
        anim_petal_12 = petal_12.GetComponent<Animator>();
        anim_petal_14 = petal_14.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

        fadeTime = screenFade.fadeTime;
        //BGM.Play();
        BGM.SetActive(true);
       
        StartCoroutine(FirstDelay());
    }

    // Update is called once per frame
    void Update()
    {
        screenFade.FadeOut();
    }


    private IEnumerator FirstDelay()
    {
        effect_1.SetActive(true);        
        //boat1.SetActive(true);
        //lotus.SetActive(true);
        //aodai.SetActive(true);
        //yield return new WaitForSeconds(15.0f);                
        //effect_2.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        anim_Rock5A.SetTrigger("Active");
        anim_Rock2.SetTrigger("Active");
        yield return new WaitForSeconds(1.0f);
        quanAm.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        phatTo.SetActive(true);
        yield return StartCoroutine("OnTheBridge");

    }


    private IEnumerator OnTheBridge()
    {
        Second_camera.SetActive(true);
        First_camera.SetActive(false);       
        visual.SetActive(true);

        yield return new WaitForSeconds(35f);
        yield return StartCoroutine("OnTheHand");

    }

    private IEnumerator OnTheHand()
    {     
        Third_camera.SetActive(true);
        Second_camera.SetActive(false);
        
        yield return new WaitForSeconds(25.0f);
        anim_petal_01.SetTrigger("Active");
        yield return new WaitForSeconds(1f);
        anim_petal_02.SetTrigger("Active");
        yield return new WaitForSeconds(1f);
        anim_petal_12.SetTrigger("Active");
        yield return new WaitForSeconds(1f);
        anim_petal_14.SetTrigger("Active");
        isOnTheHand = true;
        yield return null;


    }

    public void BackToBridge()
    {
        Third_camera.SetActive(false);
        Second_camera.SetActive(true);
        Destroy(R_Hand_1);
        R_Hand_2.SetActive(true);
        /*
        if (isOnTheHand && !isMoving && isOnTarget)
        {
            Third_camera.SetActive(false);
            Second_camera.SetActive(true);
            Destroy(R_Hand_1);
            R_Hand_2.SetActive(true);
            panel.SetActive(true);
        }*/

    }



}
