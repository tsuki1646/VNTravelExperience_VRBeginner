using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
    [SerializeField]
    GameObject Player, rightHand, Sphere_Hologram, effect;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player)
        {
            Sphere_Hologram.SetActive(true);
            effect.SetActive(true);
            other.transform.parent = this.transform;
            other.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

}
