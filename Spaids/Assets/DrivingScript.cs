using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour {

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
        if (Input.GetKey(KeyCode.W))
        {
            _velocity += _speed;
        }
        else if (Input.GetKey(KeyCode.S))
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
        else if(_velocity > 0)
        {
            _velocity -= _speed * _decelerationMagnitude;
        }
        else if(_velocity < 0)
        {
            _velocity *= _speed * _decelerationMagnitude;
        }

        if (/*Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)*/_velocity != 0)
        {

            if (Input.GetKey(KeyCode.A))
            {
                _rotationVelocity -= _rotationSpeed * _velocity;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _rotationVelocity += _rotationSpeed * _velocity;
            }
            _rotationVelocity = Mathf.Clamp(_rotationVelocity, -_maxRotationSpeed, _maxRotationSpeed);
            Debug.Log(_rotationVelocity);
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


        _rotationVelocity = Mathf.Clamp(_rotationVelocity, -1f, 1f);

        transform.Rotate(new Vector3(0, _rotationVelocity, 0));


        if (_velocity > _maxSpeed)
        {
            _velocity = _maxSpeed;
        }
        if(_velocity < _maxBackwardsSpeed )
        {
            _velocity = _maxBackwardsSpeed;
        }

        transform.position += transform.forward * _velocity;


    }
}
