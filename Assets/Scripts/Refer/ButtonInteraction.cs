﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonInteraction : MonoBehaviour
{
    public TextMeshProUGUI simpleUIText;

    public void OnButton1Clicked()
    {
        simpleUIText.text = "Button1 is clicked";
        Debug.Log("Button1 is clicked");
    }

    public void OnButton2Clicked()
    {
        simpleUIText.text = "Button2 is clicked";
        Debug.Log("Button2 is clicked");
    }


    public void OnButton3Clicked()
    {
        simpleUIText.text = "Button3 is clicked";
        Debug.Log("Button3 is clicked");
    }

    public void OnStartClicked()
    {
        SceneManager.LoadScene(1);
    }
}

