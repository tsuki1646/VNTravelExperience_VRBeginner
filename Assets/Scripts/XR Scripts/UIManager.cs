using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject UI_VRMenuGameobject;
    //public GameObject UI_OpenWorldsGameobject;

    // Start is called before the first frame update
    void Start()
    {
        UI_VRMenuGameobject.SetActive(false);
        //UI_OpenWorldsGameobject.SetActive(false);
    }


    public void OnOtherExperience_ButtonClicked()
    {
        SceneManager.LoadScene(2);
    }

    public void OnFinishGame_ButtonClicked()
    {
        SceneManager.LoadScene(4); 

    }

    public void OnRestart_ButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

}

