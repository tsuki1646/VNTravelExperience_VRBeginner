using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusPetalsFalling : MonoBehaviour
{
    public GameObject rightHand;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == rightHand)
        {
            //rightHand.transform.parent = transform;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == rightHand)
        {
            //rightHand.transform.parent = null;
        }
    }
}
