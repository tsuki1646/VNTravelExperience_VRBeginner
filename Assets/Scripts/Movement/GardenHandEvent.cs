using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardenHandEvent : MonoBehaviour
{
    [SerializeField]
    public bool isRight;
    [SerializeField]
    float amp, vibrationTime;
    [SerializeField]
    AudioClip explosionSound, doorSound;
    [SerializeField]
    GameObject plasmaEffect, doorBell;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "touchObject")
        {

            VibrationStart();
            AudioSource.PlayClipAtPoint(explosionSound, other.transform.position);
            Instantiate(plasmaEffect, other.transform.position, Quaternion.identity);
            doorBell.SetActive(true);
            other.gameObject.SetActive(false);

        }

        else if (other.tag == "doorHandle")
        {
            VibrationStart();
            AudioSource.PlayClipAtPoint(doorSound, other.transform.position);
            Invoke("LoadNextScene", doorSound.length);
        }
    }


    void VibrationStart()
    {
        if (isRight)
        {
            OVRInput.SetControllerVibration(1, amp, OVRInput.Controller.RTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(1, amp, OVRInput.Controller.LTouch);
        }

        Invoke("VibrationStop", vibrationTime);
    }
    void VibrationStop()
    {
        OVRInput.SetControllerVibration(1, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(1, 0, OVRInput.Controller.LTouch);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(2);
    }
}

