using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolScript : MonoBehaviour {

    public GameObject _checkpointsManager;
    public List<Vector3> _patrolPath = new List<Vector3>();
    public float _distanceOffset = 5f;
    [SerializeField]
    private float _globalSpeed = 50f;
    private int _target = 0;
    private NavMeshAgent _agent;
    Vector3 _velocity;

    [SerializeField]
    private float _speed = 2;

	// Use this for initialization
	void Start () {
        NavMeshHit closestHit;

        if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            gameObject.transform.position = closestHit.position;

        foreach (Transform gObject in _checkpointsManager.GetComponentInChildren<Transform>())
        {
            if (gObject != _checkpointsManager.transform)
            {
                Vector3 vec3 = new Vector3(gObject.position.x, gObject.position.y, gObject.position.z);
                _patrolPath.Add(vec3);
            }
            
        }

        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _patrolPath[0];
    }

    void SetNextPoint()
    {
        Debug.Log("heh");
        _target = (_target + 1) % _patrolPath.Count;

        _agent.destination = _patrolPath[_target];
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0)
            return; // don't update when time is paused

        Debug.Log(_agent.destination + " - " + _target + " - " + _agent.remainingDistance);

        if (!_agent.pathPending && _agent.remainingDistance < 2f)
            SetNextPoint();

        if (_patrolPath.Count < 1) return;


        /*
        Debug.Log(_target + " - " + Vector3.Distance(transform.position, _patrolPath[_target]));
        _velocity = (_patrolPath[_target] - transform.position).normalized * _speed;
        _velocity.y = 0;
        transform.position += _velocity * (Time.deltaTime * _globalSpeed);
        transform.LookAt(_patrolPath[_target]);*/

	}
}
