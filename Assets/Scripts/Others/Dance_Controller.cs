using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Dance_Controller : MonoBehaviour
{

    [SerializeField]
    float endingTime;
    [SerializeField]
    GameObject aodai, aodai2, effect, effect_smoke, spotLight, Env_01, Env_02, Env_03, lotus;

    [SerializeField]
    GameObject binhPhong1, binhPhong2, directionalLight, effect2, effect3;

    [SerializeField]
    GameObject BP_4R, BP_1L, panel;

    [SerializeField]
    GameObject visualGroup;

    [SerializeField]
    OVRScreenFade screenFade;

    float pictureDelay = 2;
    float danceDelay = 5;

    [SerializeField]
    float startLightIntensity;

    [SerializeField]
    float fadeOutTime, fadeInTime;

    [SerializeField]
    float fadeTime;

    [SerializeField]
    //AudioSource BGM;
    GameObject BGM;

    [SerializeField]
    Animator anim_Amee1, anim_Amee2, anim_BP_Left, anim_BP_Right;

    private bool isPlaying;


    void Awake()
    {
        anim_Amee1 = aodai.GetComponent<Animator>();
        anim_Amee2 = aodai2.GetComponent<Animator>();
        anim_BP_Left = BP_1L.GetComponent<Animator>();
        anim_BP_Right = BP_4R.GetComponent<Animator>();
    }

    void Start()
    {
        fadeTime = screenFade.fadeTime;

        StartCoroutine(Delay());
    }

    void Update()
    {
        screenFade.FadeOut();
    }

    private IEnumerator Delay()
    {
        spotLight.SetActive(true);
        //yield return new WaitForSeconds(1.0f);
        //BGM.Play();
        BGM.SetActive(true);
        yield return new WaitForSeconds(2f);
        aodai.SetActive(true);
        visualGroup.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        //picture.SetActive(true);
        yield return StartCoroutine("AodaiAppear");

        yield return null;
    }

    
    private IEnumerator AodaiAppear()
    {
        effect.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Env_01.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        Env_02.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        Env_03.SetActive(true);
        yield return new WaitForSeconds(40f);
        yield return StartCoroutine("AodaiAppear_part2");
    }

    private IEnumerator AodaiAppear_part2()
    {
        //effect.SetActive(true);        
        yield return new WaitForSeconds(2f);
        Env_03.SetActive(false);
        yield return new WaitForSeconds(1f);
        Env_02.SetActive(false);
        yield return new WaitForSeconds(1f);
        Env_01.SetActive(false);
        yield return new WaitForSeconds(1f);
        aodai.SetActive(false);
        yield return new WaitForSeconds(2f);
        directionalLight.SetActive(true);
        visualGroup.SetActive(true);
        binhPhong1.SetActive(true);
        yield return new WaitForSeconds(10f);
        binhPhong1.SetActive(false);
        binhPhong2.SetActive(true);
        yield return new WaitForSeconds(2f);
        effect_smoke.SetActive(true);
        effect2.SetActive(true);
        effect3.SetActive(true);
        yield return new WaitForSeconds(1f);
        aodai2.SetActive(true);
        yield return new WaitForSeconds(160f);
        anim_BP_Left.SetTrigger("MoveLtoR");
        anim_BP_Right.SetTrigger("MoveRtoL");
        yield return new WaitForSeconds(2f);
        aodai2.SetActive(false);
        yield return new WaitForSeconds(1f);
        binhPhong2.SetActive(false);
        yield return new WaitForSeconds(1f);
        effect.SetActive(false);
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);

    }



}
