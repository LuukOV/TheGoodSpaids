using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockYPositionScript : MonoBehaviour {


    float _initialY;
	// Use this for initialization
	void Start () {
        _initialY = transform.position.y;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (transform.position.y != _initialY) {
            transform.position = new Vector3(transform.position.x, _initialY, transform.position.z);
        }
	}
}
