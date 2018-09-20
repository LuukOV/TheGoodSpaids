using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _shootingPoint;

    [SerializeField] private float _globalAmplifier = 10f;
    [SerializeField] private float _bulletSpeed = 1000f;
    [SerializeField] private float _loadSpeed = 0.01f;
    [SerializeField] private float _maxIncreasedLoadSpeed = 2f;
    private float _increasedLoadSpeed;

    private float _rotationSpeed = 0f;
    [SerializeField] private float _maxRotationSpeed = 10f;
    private Vector3 _rotationZ = new Vector3(0, 0, 0);

    [SerializeField] AudioClip _startupClip;
    [SerializeField] AudioClip _spinClip;
    [SerializeField] AudioClip _endClip;

    private DrivingScript _drivingScript;
    private AudioSource _audioSource;

	// Use this for initialization
	void Start () {
        _drivingScript = GetComponent<DrivingScript>();
        _audioSource = GetComponent<AudioSource>();
        _increasedLoadSpeed = _loadSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0)
            return; // don't update when time is paused

        CheckAudio();


        if (Input.GetButton("Fire1"))
        {

            _rotationSpeed += _increasedLoadSpeed * Time.deltaTime * _globalAmplifier;
            _increasedLoadSpeed += _loadSpeed * Time.deltaTime * _globalAmplifier;

            if(_rotationSpeed >= _maxRotationSpeed)
            {
                _rotationSpeed = 0f;

                GameObject bullet = Instantiate(_bullet, _shootingPoint.transform.position, Quaternion.FromToRotation(Vector3.up, transform.forward));
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * _bulletSpeed);
            }
        }
        else
        {
            _increasedLoadSpeed -= (_loadSpeed * 2) * Time.deltaTime * _globalAmplifier;
            _rotationSpeed -= (_loadSpeed * 2) * Time.deltaTime * _globalAmplifier;
        }


        _increasedLoadSpeed = Mathf.Clamp(_increasedLoadSpeed, 0, _maxIncreasedLoadSpeed);
        _rotationSpeed = Mathf.Clamp(_rotationSpeed, 0, _maxRotationSpeed + 1f);

        _rotationZ.z = _rotationSpeed;
        transform.Rotate(_rotationZ);
	}

    void CheckAudio()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _audioSource.clip = _startupClip;
            _audioSource.loop = false;
            _audioSource.Play();
        }

        if (Input.GetButton("Fire1") && !_audioSource.isPlaying)
        {
            _audioSource.clip = _spinClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            _audioSource.clip = _endClip;
            _audioSource.loop = false;
            _audioSource.Play();
        }
    }
}
