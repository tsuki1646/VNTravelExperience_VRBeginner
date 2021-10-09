using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraRigColl : MonoBehaviour
{
    public CapsuleCollider coll;
    public GameObject centerEyeAnchor;
    private Vector3 place;
    private void LateUpdate() {
        place = new Vector3(centerEyeAnchor.transform.localPosition.x, 0, centerEyeAnchor.transform.localPosition.z);
        coll.center = place;
    }
}