using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DollController : MonoBehaviour
{
    [SerializeField]
    Light candleLight;
    [SerializeField]
    float flickerRate, flickerRange, partyEndTime, autoEndPartyTime, endingTime, backToTitleTime;
    [SerializeField]
    GameObject dolls, dolls_Big, girl, skeleton, endRoll, camera1, camera2, tempListener, shutwall, girlVoiceInCube, rabbit, clock;

    [SerializeField]
    SphereCollider partyCollider;
    [SerializeField]
    OVRScreenFade screenFade1;

    [SerializeField]
    TextMeshProUGUI textMesh;

    GuideTalk guideTalk;


    float candleRangeTime = 5; // ライトの広がる時間
    float candleRangeMax = 20;
    float lightRangeDiff;
    float totalGazeTime = 0;
    float whisperDelay = 2;
    float skeletonVoiceDelay = 5;
    float endRollInterval = 6;
    float fontSize = 60;
    float stayTime = 0;
    float chasingTime = 0;
    [SerializeField]
    float fadeOutTime, fadeInTime;
    float startLightIntensity;

    //int n = 10;

    [SerializeField]
    bool partyEnd, isChasing;
    [SerializeField]
    AudioClip disapearSound;

    bool candleFadeOut, candleFadeIn;
    bool isFlickering = true;
    bool isPlayerEnterRoom = false;

    [SerializeField]
    float fadeTime;

    [SerializeField]
    AudioSource voice, endMusic;

    [SerializeField]
    Vector3 startRotation, dollPlayerRotation;

    int countNumber;
    bool finalCount;
    [SerializeField]
    float countFloatNumber;
    public bool IsPlayerEnterRoom
    {
        get { return isPlayerEnterRoom; }
        set { isPlayerEnterRoom = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        camera1.transform.rotation = Quaternion.Euler(startRotation);
        lightRangeDiff = candleRangeMax - candleLight.range;
        startLightIntensity = candleLight.intensity;
        fadeTime = screenFade1.fadeTime;
    }

    // Update is called once per frame
    void Update()
    {
        LightFlicker();

        AutomaticPartyEnd();

        ActivateBigDolls();

        Chasing();

        FinalCountDown();

        if (candleFadeOut)
        {
            candleLight.intensity -= Time.deltaTime / fadeOutTime;
            if (candleLight.intensity <= 0.01f)
            {
                Endroll();
                candleFadeOut = false;
            }
        }
        else if (candleFadeIn)
        {
            candleLight.intensity += Time.deltaTime / fadeInTime;
            if (candleLight.intensity >= startLightIntensity)
            {
                candleFadeIn = false;
            }
        }
    }
    void LightFlicker()
    {
        if (isFlickering)
        {
            candleLight.intensity += Random.Range(-flickerRate, flickerRate);

            if (candleLight.intensity > startLightIntensity + flickerRange)
            {
                candleLight.intensity = startLightIntensity + flickerRange / 2;
            }

            else if (candleLight.intensity < startLightIntensity - flickerRange)
            {
                candleLight.intensity = startLightIntensity - flickerRange / 2;
            }
        }

    }
    void AutomaticPartyEnd()
    {
        if (isPlayerEnterRoom)
        {
            stayTime += Time.deltaTime;
            if (stayTime > autoEndPartyTime)
            {
                PartyEnd();
                partyCollider.enabled = false;
                isPlayerEnterRoom = false;
            }
        }
    }
    void ActivateBigDolls()
    {
        if (partyEnd)
        {

            candleLight.range += Time.deltaTime * lightRangeDiff / candleRangeTime;
            if (candleLight.range > candleRangeMax)
            {

                dolls_Big.SetActive(true);
                isChasing = true;
                stayTime = 0;
                partyEnd = false;

            }

        }
    }


    public void PartyGazeTime(float time)
    {
        totalGazeTime += time;
        if (totalGazeTime > partyEndTime && !partyEnd)
        {
            AudioSource.PlayClipAtPoint(disapearSound, girl.transform.position);
            PartyEnd();

            Invoke("WhisperVoice", whisperDelay);

        }
    }

    void PartyEnd()
    {
        dolls.SetActive(false);
        partyCollider.enabled = false;
        flickerRange += 0.1f;
        partyEnd = true; 　　//Update へ
        isPlayerEnterRoom = false;
    }
    void WhisperVoice() { voice.Play(); }
    void Chasing()
    {
        if (isChasing)
        {
            chasingTime += Time.deltaTime;
            if (chasingTime >= endingTime)
            {
                isChasing = false;
                screenFade1.FadeOut();
                Invoke("Camera1Null", fadeTime);
            }
        }
    }

    void FinalCountDown()
    {
        if (finalCount)
        {
            countFloatNumber -= Time.deltaTime;
            countNumber = (int)countFloatNumber;
            if (countFloatNumber <= 0f)
            {
                countFloatNumber = 0;
            }
            textMesh.text = "プレイして頂き、誠にありがとうございました！\n" + countNumber + "秒後にタイトルに戻ります。";
        }
    }
    void Camera1Null()
    {

        Destroy(dolls_Big);
        dolls.SetActive(true);
        girl.SetActive(false);
        skeleton.SetActive(true);

        tempListener.SetActive(true);

        camera1.SetActive(false);

        Invoke("GirlVoice", 1.5f);

    }

    void GirlVoice()
    {
        girlVoiceInCube.SetActive(true);
        Invoke("YouAreMyDoll", 7);
    }

    void YouAreMyDoll()
    {

        shutwall.SetActive(false);

        camera2.SetActive(true);
        camera2.transform.rotation = Quaternion.Euler(dollPlayerRotation);
        tempListener.SetActive(false);
        Invoke("SkeletonVoice", skeletonVoiceDelay);
    }

    void SkeletonVoice()
    {
        AudioSource skeletonVoice = skeleton.gameObject.GetComponent<AudioSource>();
        skeletonVoice.Play();

        isFlickering = false;
        candleFadeOut = true;
    }

    void Endroll()
    {

        endRoll.SetActive(true);
        skeleton.SetActive(false);
        Vector3 direction = endRoll.transform.position - tempListener.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
        endMusic.Play();
        Invoke("TextChange1", endRollInterval);
    }
    void TextChange1()
    {
        textMesh.text = "Thank you for playing!!";
        Invoke("TextChange2", endRollInterval);
        candleFadeIn = true;
    }
    void TextChange2()
    {
        StartCoroutine(MiniDollAnimInterval());
        rabbit.SetActive(true);
        clock.SetActive(true);
        textMesh.fontSize = fontSize;
        finalCount = true;

        Invoke("BackToTitle", backToTitleTime);

    }

    IEnumerator MiniDollAnimInterval()
    {
        GameObject[] miniDolls = GameObject.FindGameObjectsWithTag("DollMini");
        foreach (GameObject miniDoll in miniDolls)
        {
            miniDoll.GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(1);
        }
    }

    void BackToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
