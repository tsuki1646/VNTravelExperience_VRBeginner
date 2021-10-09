using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevel : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetTalkLvl(float talkLvl)
    {
        masterMixer.SetFloat("talkVol", talkLvl);
    }

    public void SetMusicLvl(float musicLvl)
    {
        masterMixer.SetFloat("musicVol", musicLvl);
    }
}
