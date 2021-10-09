using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toNextScene_Aodai()
    {
        SceneManager.LoadScene(2);
    }

    public void toNextScene_BanaHills()
    {
        SceneManager.LoadScene(3);
    }

    public void FinishGame()
    {
        SceneManager.LoadScene(4);
    }
}
