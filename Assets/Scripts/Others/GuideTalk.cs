using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideTalk : MonoBehaviour
{
    RaycastManager rayMa;

    //public AudioClip talkClip;//正しいオブジェクトを切ったときの音
    private float startTime;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void Start()
    {
        StartCoroutine(Talk());
    }



    IEnumerator Talk()
    {
        yield return new WaitForSeconds(2.0f);
    } 


}
