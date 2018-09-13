using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockYRotationScript : MonoBehaviour {

	// Update is called once per frame
	void LateUpdate () {
        Debug.Log(transform.localEulerAngles);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
	}
}
