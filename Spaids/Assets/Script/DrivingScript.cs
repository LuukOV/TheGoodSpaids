using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour {

    enum Mode
    {
        Normal,
        Forward,
        Backwards,
    }

    [SerializeField] float _globalAmplifier = 10f;
    [SerializeField] float _speed = 0.5f;
    [SerializeField] float _rotationSpeed = 3f;
    [SerializeField] float _maxRotationSpeed = 3f;
    [SerializeField] float _maxSpeed = 10f;
    [SerializeField] float _maxBackwardsSpeed = -3f;
    [SerializeField] float _decelerationMagnitude = 0.1f;
    [SerializeField] float _rotationDecelerationMagnitude = 0.1f;
    [SerializeField] float _backwardsSpeed = 0.25f;
    [SerializeField] float _breakSpeed = 2f;

    [SerializeField] float _maxJetRotationX = 30f;
    [SerializeField] float _maxJetRotationZ = 50f;
    [SerializeField] float _minJetRotationZ = 30f;
    float _jetRotationOffset = 10f;

    public GameObject _backLeftJet;
    public GameObject _backRightJet;
    public GameObject _frontLeftJet;
    public GameObject _frontRightJet;
    private List<GameObject> _jets;
    private Mode _drivingMode = Mode.Normal;

    float _rotationVelocity = 0f;
    float _velocity = 0f;
    Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _jets = new List<GameObject>() { _backLeftJet, _backRightJet, _frontLeftJet, _frontRightJet }; // fill list
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0)
            return; // don't update when time is paused

        checkVelocity();
        PositionJets();



        if (Input.GetAxis("Horizontal") < 0)
        {
            _rotationVelocity -= _rotationSpeed;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _rotationVelocity += _rotationSpeed;
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
        _drivingMode = Mode.Normal;
        if (Input.GetAxis("RT_TRIGGER") != 0) // Right Trigger
        {
            _velocity += _speed * Input.GetAxis("RT_TRIGGER");
            _drivingMode = Mode.Forward;
        }
        else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) // W/Up
        {
            _velocity += _speed * Input.GetAxis("Vertical");
            _drivingMode = Mode.Forward;
        }
        if (Input.GetAxis("LT_TRIGGER") != 0) // Left Trigger
        {
            if (_velocity > 0)
            {
                _velocity -= _breakSpeed * Input.GetAxis("LT_TRIGGER");
            }
            else
            {
                _velocity -= _backwardsSpeed * Input.GetAxis("LT_TRIGGER");
            }
            _drivingMode = Mode.Backwards;

        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))// S/Down
        {
            if (_velocity > 0)
            {
                _velocity -= _breakSpeed * -Input.GetAxis("Vertical");
            }
            else
            {
                _velocity -= _backwardsSpeed * -Input.GetAxis("Vertical");
            }
            _drivingMode = Mode.Backwards;
        }
        else
        {
            decreaseSpeed();
        }
    }

    void PositionJets()
    {
        if(_drivingMode == Mode.Forward) // move jets backwards
        {
            foreach(GameObject jet in _jets)
            {
                if(jet.transform.eulerAngles.x <= _maxJetRotationX || jet.transform.eulerAngles.x >= 360 - _maxJetRotationX)
                {
                    jet.transform.Rotate(1, 0, 0);
                }
            }
        }
        else if (_drivingMode == Mode.Backwards) // move jets forwards
        {
            foreach (GameObject jet in _jets)
            {
                if (jet.transform.eulerAngles.x > 360 - _maxJetRotationX || jet.transform.eulerAngles.x <= _maxJetRotationX + 1)
                {
                    jet.transform.Rotate(-1, 0, 0);
                }
            }
        }
        else
        {
            foreach (GameObject jet in _jets)
            {
                if (jet.transform.eulerAngles.x > 359 - _maxJetRotationX)
                {
                    jet.transform.Rotate(1, 0, 0);
                }
                else if (jet.transform.eulerAngles.x > 0)
                {
                    jet.transform.Rotate(-1, 0, 0);
                }

            }
        }

        if (_velocity >= 0) // forward
        {
            if (_rotationVelocity > 0) // right
            {
                if (_frontLeftJet.transform.eulerAngles.z > 359 - _minJetRotationZ || _frontLeftJet.transform.eulerAngles.z < _minJetRotationZ + _jetRotationOffset)
                { // left jet
                    _frontLeftJet.transform.eulerAngles = new Vector3(_frontLeftJet.transform.eulerAngles.x, _frontLeftJet.transform.eulerAngles.y, _frontLeftJet.transform.eulerAngles.z - 1);
                }
                if (_frontRightJet.transform.eulerAngles.z > 359 - _minJetRotationZ || _frontRightJet.transform.eulerAngles.z < _minJetRotationZ + _jetRotationOffset) // right jet
                {
                    _frontRightJet.transform.eulerAngles = new Vector3(_frontRightJet.transform.eulerAngles.x, _frontRightJet.transform.eulerAngles.y, _frontRightJet.transform.eulerAngles.z - 1);
                }
            }
            else if (_rotationVelocity < 0) // left
            {
                if (_frontLeftJet.transform.eulerAngles.z > 359 - (_minJetRotationZ + _jetRotationOffset) || _frontLeftJet.transform.eulerAngles.z < _minJetRotationZ)
                { // left jet
                    _frontLeftJet.transform.eulerAngles = new Vector3(_frontLeftJet.transform.eulerAngles.x, _frontLeftJet.transform.eulerAngles.y, _frontLeftJet.transform.eulerAngles.z + 1);
                }
                if (_frontRightJet.transform.eulerAngles.z > 359 - (_minJetRotationZ + _jetRotationOffset) || _frontRightJet.transform.eulerAngles.z < _minJetRotationZ) // right jet
                {
                    _frontRightJet.transform.eulerAngles = new Vector3(_frontRightJet.transform.eulerAngles.x, _frontRightJet.transform.eulerAngles.y, _frontRightJet.transform.eulerAngles.z + 1);
                }
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
