using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour {

    public List<Vector3> _patrolPath = new List<Vector3>();
    private int _target = 0;
    Vector3 _velocity;

    [SerializeField]
    private float _speed = 2;

	// Use this for initialization
	void Start () {
        foreach (Transform gObject in GetComponentInChildren<Transform>())
        {
            Vector3 vec3 = new Vector3(gObject.position.x, gObject.position.y, gObject.position.z);
            _patrolPath.Add(vec3);
            
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (_patrolPath.Count < 1) return;

        if(Vector3.Distance(transform.position, _patrolPath[_target]) < 10f){
            if(_target >= _patrolPath.Count - 1)
            {
                _target = 0;
            }
            else
            {
                _target++;
            }
        }

        _velocity = (_patrolPath[_target] - transform.position).normalized * _speed;
        _velocity.y = 0;
        transform.position += _velocity;
        transform.LookAt(_patrolPath[_target]);

	}
}
