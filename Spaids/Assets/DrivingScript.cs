using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour {

    [SerializeField]
    float _globalAmplifier = 10f;
    [SerializeField]
    float _speed = 0.5f;
    [SerializeField]
    float _rotationSpeed = 3f;
    [SerializeField]
    float _maxRotationSpeed = 3f;
    [SerializeField]
    float _maxSpeed = 10f;
    [SerializeField]
    float _maxBackwardsSpeed = -3f;
    [SerializeField]
    float _decelerationMagnitude = 0.1f;
    [SerializeField]
    float _rotationDecelerationMagnitude = 0.1f;
    [SerializeField]
    float _backwardsSpeed = 0.25f;
    [SerializeField]
    float _breakSpeed = 2f;

    float _rotationVelocity = 0f;
    float _velocity = 0f;
    Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        checkVelocity();

        if (/*Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)*/_velocity != 0)
        {

            if (Input.GetAxis("Horizontal") < 0)
            {
                _rotationVelocity -= _rotationSpeed * Mathf.Abs(_velocity);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                _rotationVelocity += _rotationSpeed * Mathf.Abs(_velocity);
            }
        }

        if (_velocity != 0)
        {
            if (_rotationVelocity > 0)
            {
                _rotationVelocity -= _rotationDecelerationMagnitude;
            }
            else if (_rotationVelocity < 0)
            {
                _rotationVelocity += _rotationDecelerationMagnitude;
            }

        }
        else
        {
            if (_rotationVelocity > 0)
            {

                _rotationVelocity -= _rotationDecelerationMagnitude / 5f;
            }
            else if (_rotationVelocity < 0)
            {
                _rotationVelocity += _rotationDecelerationMagnitude / 5f;
            }
        }
        if (Mathf.Abs(_rotationVelocity) < 0.005f)
        {
            _rotationVelocity = 0f;
        }


        _rotationVelocity = Mathf.Clamp(_rotationVelocity, -_maxRotationSpeed, _maxRotationSpeed);

        transform.Rotate(new Vector3(0, _rotationVelocity * (Time.deltaTime * _globalAmplifier), 0));


        if (_velocity > _maxSpeed)
        {
            _velocity = _maxSpeed;
        }
        if(_velocity < _maxBackwardsSpeed )
        {
            _velocity = _maxBackwardsSpeed;
        }

        transform.position += (transform.forward * _velocity) * (Time.deltaTime * _globalAmplifier);
    }

    void checkVelocity()
    {
        if (Input.GetJoystickNames().Length == 0)
        {
            if (Input.GetAxis("Vertical") > 0) //only check triggers if controller is connectected
            {
                _velocity += _speed;
            }
            else if (Input.GetAxis("Vertical") < 0) //here too
            {
                if (_velocity > 0)
                {
                    _velocity -= _breakSpeed;
                }
                else
                {
                    _velocity -= _backwardsSpeed;
                }

            }
            else
            {
                decreaseSpeed();
            }
        }
        else
        {
            if (Input.GetAxis("RT_TRIGGER") != 0) //only check triggers if controller is connectected
            {
                _velocity += _speed * Input.GetAxis("RT_TRIGGER");
            }
            else if (Input.GetAxis("LT_TRIGGER") != 0) //here too
            {
                if (_velocity > 0)
                {
                    _velocity -= _breakSpeed * Input.GetAxis("LT_TRIGGER"); 
                }
                else
                {
                    _velocity -= _backwardsSpeed * Input.GetAxis("LT_TRIGGER");
                }

            }
            else
            {
                decreaseSpeed();
            }
        }

    }

    void decreaseSpeed()
    {
        if (_velocity > 0)
        {
            _velocity -= _speed * _decelerationMagnitude;
        }
        else if (_velocity < 0)
        {
            _velocity *= _speed * _decelerationMagnitude;
        }
    }
}
