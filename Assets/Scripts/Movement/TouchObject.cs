using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    [SerializeField]
    GameObject Player, rightHand, r_effect;
    [SerializeField]
    GameObject leftHand, l_effect;

    public RightHandMovement4 r_handMovement;

    public LeftHandMovement l_handMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            r_effect.SetActive(true);
            other.transform.parent = rightHand.transform;
            r_handMovement.isMoving = true;
            Invoke("TouchObjectDisapear", 1f);
            l_handMovement.isMoving = true;
        }
    }

    void TouchObjectDisapear()
    {
        Destroy(this.gameObject);
    }
}
