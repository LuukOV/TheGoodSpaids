using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private GameObject _shootingPoint;

    [SerializeField]
    private float _bulletSpeed = 1000f;
    [SerializeField]
    private float _loadSpeed = 0.01f;
    private float _increasedLoadSpeed;
    [SerializeField]
    private float _maxIncreasedLoadSpeed = 2f;

    private float _rotationSpeed = 0f;
    [SerializeField]
    private float _maxRotationSpeed = 10f;
    private Vector3 _rotationZ = new Vector3(0, 0, 0);

    private DrivingScript _drivingScript;

	// Use this for initialization
	void Start () {
        _drivingScript = GetComponent<DrivingScript>();
        _increasedLoadSpeed = _loadSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0)
            return; // don't update when time is paused

        if (Input.GetButton("Fire1"))
        {
            _rotationSpeed += _increasedLoadSpeed;
            _increasedLoadSpeed += _loadSpeed;

            if(_rotationSpeed >= _maxRotationSpeed)
            {
                _rotationSpeed = 0f;

                GameObject bullet = Instantiate(_bullet, _shootingPoint.transform.position, Quaternion.FromToRotation(Vector3.up, transform.forward));
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * _bulletSpeed);
            }
        }
        else
        {
            _increasedLoadSpeed -= _loadSpeed * 2;
            _rotationSpeed -= _loadSpeed * 2;
        }
        _increasedLoadSpeed = Mathf.Clamp(_increasedLoadSpeed, 0, _maxIncreasedLoadSpeed);
        _rotationSpeed = Mathf.Clamp(_rotationSpeed, 0, _maxRotationSpeed + 1f);

        _rotationZ.z = _rotationSpeed;
        transform.Rotate(_rotationZ);
	}
}
