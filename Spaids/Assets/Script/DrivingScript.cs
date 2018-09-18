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
    [SerializeField] float _maxJetRotationZ = 30f;
    [SerializeField] float _jetRotationSpeed = 0.5f;
    float _jetRotationOffset = 30f;

    public GameObject _backLeftJet;
    public GameObject _backRightJet;
    public GameObject _frontLeftJet;
    public GameObject _frontRightJet;
    private List<GameObject> _jets;
    private Mode _drivingMode = Mode.Normal;

    float _rotationVelocity = 0f;
    float _velocity = 0f;
    Rigidbody _rigidbody;
    CharacterController _charControler;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _charControler = GetComponent<CharacterController>();
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

        if (_rotationVelocity > 0)
        {
            _rotationVelocity -= _rotationDecelerationMagnitude;
        }
        else if (_rotationVelocity < 0)
        {
            _rotationVelocity += _rotationDecelerationMagnitude;
        }
        if(_drivingMode == Mode.Normal && Mathf.Abs(_rotationVelocity) < 0.05f)
        {
            _rotationVelocity = 0;
        }

        if (Mathf.Abs(_rotationVelocity) < 0.01f && Input.GetAxis("Horizontal") == 0)
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

        _charControler.Move((transform.forward * _velocity) * (Time.deltaTime * _globalAmplifier));
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
                if(jet.transform.localEulerAngles.x <= _maxJetRotationX || jet.transform.localEulerAngles.x >= 360 - _maxJetRotationX)
                {
                    jet.transform.Rotate(1, 0, 0);
                }
            }
        }
        else if (_drivingMode == Mode.Backwards) // move jets forwards
        {
            foreach (GameObject jet in _jets)
            {
                if (jet.transform.localEulerAngles.x > 360 - _maxJetRotationX || jet.transform.localEulerAngles.x <= _maxJetRotationX + 1)
                {
                    jet.transform.Rotate(-1, 0, 0);
                }
            }
        }
        else // stabelize
        {
            foreach (GameObject jet in _jets)
            {
                if (jet.transform.localEulerAngles.x > 359 - _maxJetRotationX)
                {
                    jet.transform.Rotate(_jetRotationSpeed, 0, 0);
                }
                else if (jet.transform.localEulerAngles.x > 0)
                {
                    jet.transform.Rotate(-_jetRotationSpeed, 0, 0);
                }

            }
        }

         if (_velocity >= 0) // --FORWARD--
        {
            //stabelize rearjets
            if (_backLeftJet.transform.localEulerAngles.z < (_maxJetRotationZ + _jetRotationOffset))
            {
                _backLeftJet.transform.localEulerAngles = new Vector3(_backLeftJet.transform.localEulerAngles.x, _backLeftJet.transform.localEulerAngles.y, _backLeftJet.transform.localEulerAngles.z - 0.5f);
            }
            else if (_backLeftJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset))
            {
                _backLeftJet.transform.localEulerAngles = new Vector3(_backLeftJet.transform.localEulerAngles.x, _backLeftJet.transform.localEulerAngles.y, _backLeftJet.transform.localEulerAngles.z + 0.5f);
            }
            if (_backRightJet.transform.localEulerAngles.z < (_maxJetRotationZ + _jetRotationOffset))
            {
                _backRightJet.transform.localEulerAngles = new Vector3(_backRightJet.transform.localEulerAngles.x, _backRightJet.transform.localEulerAngles.y, _backRightJet.transform.localEulerAngles.z - 0.5f);
            }
            else if (_backLeftJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset))
            {
                _backRightJet.transform.localEulerAngles = new Vector3(_backRightJet.transform.localEulerAngles.x, _backRightJet.transform.localEulerAngles.y, _backRightJet.transform.localEulerAngles.z + 0.5f);
            }

            if (Mathf.Abs(_rotationVelocity) <= 0.1f)
            {
                if (_frontLeftJet.transform.localEulerAngles.z < (_maxJetRotationZ + _jetRotationOffset))
                {
                    _frontLeftJet.transform.localEulerAngles = new Vector3(_frontLeftJet.transform.localEulerAngles.x, _frontLeftJet.transform.localEulerAngles.y, _frontLeftJet.transform.localEulerAngles.z - 0.5f);
                }
                else if (_frontLeftJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset))
                {
                    _frontLeftJet.transform.localEulerAngles = new Vector3(_frontLeftJet.transform.localEulerAngles.x, _frontLeftJet.transform.localEulerAngles.y, _frontLeftJet.transform.localEulerAngles.z + 0.5f);
                }
                if (_frontRightJet.transform.localEulerAngles.z < (_maxJetRotationZ + _jetRotationOffset))
                {
                    _frontRightJet.transform.localEulerAngles = new Vector3(_frontRightJet.transform.localEulerAngles.x, _frontRightJet.transform.localEulerAngles.y, _frontRightJet.transform.localEulerAngles.z - 0.5f);
                }
                else if (_frontLeftJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset))
                {
                    _frontRightJet.transform.localEulerAngles = new Vector3(_frontRightJet.transform.localEulerAngles.x, _frontRightJet.transform.localEulerAngles.y, _frontRightJet.transform.localEulerAngles.z + 0.5f);
                }
            }
            else if (_rotationVelocity > 0) // right
            {
                if (_frontLeftJet.transform.localEulerAngles.z > 359 - _maxJetRotationZ || _frontLeftJet.transform.localEulerAngles.z < _maxJetRotationZ + _jetRotationOffset)
                { // left jet
                    _frontLeftJet.transform.localEulerAngles = new Vector3(_frontLeftJet.transform.localEulerAngles.x, _frontLeftJet.transform.localEulerAngles.y, _frontLeftJet.transform.localEulerAngles.z - 1);
                }
                if (_frontRightJet.transform.localEulerAngles.z > 359 - _maxJetRotationZ || _frontRightJet.transform.localEulerAngles.z < _maxJetRotationZ + _jetRotationOffset) // right jet
                {
                    _frontRightJet.transform.localEulerAngles = new Vector3(_frontRightJet.transform.localEulerAngles.x, _frontRightJet.transform.localEulerAngles.y, _frontRightJet.transform.localEulerAngles.z - 1);
                }
            }
            else if (_rotationVelocity < 0) // left
            {
                if (_frontLeftJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset) || _frontLeftJet.transform.localEulerAngles.z < _maxJetRotationZ)
                { // left jet
                    _frontLeftJet.transform.localEulerAngles = new Vector3(_frontLeftJet.transform.localEulerAngles.x, _frontLeftJet.transform.localEulerAngles.y, _frontLeftJet.transform.localEulerAngles.z + 1);
                }
                if (_frontRightJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset) || _frontRightJet.transform.localEulerAngles.z < _maxJetRotationZ) // right jet
                {
                    _frontRightJet.transform.localEulerAngles = new Vector3(_frontRightJet.transform.localEulerAngles.x, _frontRightJet.transform.localEulerAngles.y, _frontRightJet.transform.localEulerAngles.z + 1);
                }
            }
        }
        else // -- BACKWARDS --
        {
            // stabelize front jets
            if (_frontLeftJet.transform.localEulerAngles.z < (_maxJetRotationZ + _jetRotationOffset))
            {
                _frontLeftJet.transform.localEulerAngles = new Vector3(_frontLeftJet.transform.localEulerAngles.x, _frontLeftJet.transform.localEulerAngles.y, _frontLeftJet.transform.localEulerAngles.z - 0.5f);
            }
            else if (_frontLeftJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset))
            {
                _frontLeftJet.transform.localEulerAngles = new Vector3(_frontLeftJet.transform.localEulerAngles.x, _frontLeftJet.transform.localEulerAngles.y, _frontLeftJet.transform.localEulerAngles.z + 0.5f);
            }
            if (_frontRightJet.transform.localEulerAngles.z < (_maxJetRotationZ + _jetRotationOffset))
            {
                _frontRightJet.transform.localEulerAngles = new Vector3(_frontRightJet.transform.localEulerAngles.x, _frontRightJet.transform.localEulerAngles.y, _frontRightJet.transform.localEulerAngles.z - 0.5f);
            }
            else if (_frontLeftJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset))
            {
                _frontRightJet.transform.localEulerAngles = new Vector3(_frontRightJet.transform.localEulerAngles.x, _frontRightJet.transform.localEulerAngles.y, _frontRightJet.transform.localEulerAngles.z + 0.5f);
            }

            if (Mathf.Abs(_rotationVelocity) <= 0.1f)
            {
                if (_backLeftJet.transform.localEulerAngles.z < (_maxJetRotationZ + _jetRotationOffset))
                {
                    _backLeftJet.transform.localEulerAngles = new Vector3(_backLeftJet.transform.localEulerAngles.x, _backLeftJet.transform.localEulerAngles.y, _backLeftJet.transform.localEulerAngles.z - 0.5f);
                }
                else if (_backLeftJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset))
                {
                    _backLeftJet.transform.localEulerAngles = new Vector3(_backLeftJet.transform.localEulerAngles.x, _backLeftJet.transform.localEulerAngles.y, _backLeftJet.transform.localEulerAngles.z + 0.5f);
                }
                if (_backRightJet.transform.localEulerAngles.z < (_maxJetRotationZ + _jetRotationOffset))
                {
                    _backRightJet.transform.localEulerAngles = new Vector3(_backRightJet.transform.localEulerAngles.x, _backRightJet.transform.localEulerAngles.y, _backRightJet.transform.localEulerAngles.z - 0.5f);
                }
                else if (_backLeftJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset))
                {
                    _backRightJet.transform.localEulerAngles = new Vector3(_backRightJet.transform.localEulerAngles.x, _backRightJet.transform.localEulerAngles.y, _backRightJet.transform.localEulerAngles.z + 0.5f);
                }
            }
            else if (_rotationVelocity < 0) // right
            {
                if (_backLeftJet.transform.localEulerAngles.z > 359 - _maxJetRotationZ || _backLeftJet.transform.localEulerAngles.z < _maxJetRotationZ + _jetRotationOffset)
                { // left jet
                    _backLeftJet.transform.localEulerAngles = new Vector3(_backLeftJet.transform.localEulerAngles.x, _backLeftJet.transform.localEulerAngles.y, _backLeftJet.transform.localEulerAngles.z - 1);
                }
                if (_backRightJet.transform.localEulerAngles.z > 359 - _maxJetRotationZ || _backRightJet.transform.localEulerAngles.z < _maxJetRotationZ + _jetRotationOffset) // right jet
                {
                    _backRightJet.transform.localEulerAngles = new Vector3(_backRightJet.transform.localEulerAngles.x, _backRightJet.transform.localEulerAngles.y, _backRightJet.transform.localEulerAngles.z - 1);
                }
            }
            else if (_rotationVelocity > 0) // left
            {
                if (_backLeftJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset) || _backLeftJet.transform.localEulerAngles.z < _maxJetRotationZ)
                { // left jet
                    _backLeftJet.transform.localEulerAngles = new Vector3(_backLeftJet.transform.localEulerAngles.x, _backLeftJet.transform.localEulerAngles.y, _backLeftJet.transform.localEulerAngles.z + 1);
                }
                if (_backRightJet.transform.localEulerAngles.z > 359 - (_maxJetRotationZ + _jetRotationOffset) || _backRightJet.transform.localEulerAngles.z < _maxJetRotationZ) // right jet
                {
                    _backRightJet.transform.localEulerAngles = new Vector3(_backRightJet.transform.localEulerAngles.x, _backRightJet.transform.localEulerAngles.y, _backRightJet.transform.localEulerAngles.z + 1);
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
