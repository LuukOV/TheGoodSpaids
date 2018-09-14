using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockYRotationScript : MonoBehaviour {

	// Update is called once per frame
	void LateUpdate () {;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
        if((transform.localEulerAngles.x < 1 || transform.localEulerAngles.x > 359) && transform.localEulerAngles.x != 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
        }
        if((transform.localEulerAngles.z < 1 || transform.localEulerAngles.z > 359) && transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x , 0, 0);
        }
    }
}
