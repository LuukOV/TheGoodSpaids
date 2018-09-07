using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {

    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private float _bulletSpeed = 1000f;

    private DrivingScript _drivingScript;

	// Use this for initialization
	void Start () {
        _drivingScript = GetComponent<DrivingScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(_bullet, transform.position + transform.forward * 3, Quaternion.FromToRotation(Vector3.up, transform.forward));
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * _bulletSpeed);
        }
	}
}
