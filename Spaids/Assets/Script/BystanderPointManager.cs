using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderPointManager : MonoBehaviour {

    List<GameObject> _bystanders = new List<GameObject>();
    [SerializeField]
    float _maxBystanders = 3f;
    float _activeBystanders = 0f;
    [SerializeField]
    float _bystanderTimeoffset = 10f;
    float _bystanderTimer;

	// Use this for initialization
	void Start () {
        _bystanderTimer = _bystanderTimeoffset;
        for(int i = 0; i < transform.childCount; i++)
        {
            _bystanders.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        _bystanderTimer -= Time.deltaTime;
        if(_bystanderTimer <= 0)
        {
            _bystanderTimer = _bystanderTimeoffset;
            int amountOfActives = checkActives();
            if(amountOfActives < _maxBystanders) { 
                getPoint();
            }
        }
	}

    private int checkActives()
    {
        int actives = 0;
        foreach (GameObject gObject in _bystanders)
        {
            if (gObject.activeSelf)
            {
                actives++;
            }
        }
        return actives;
    }

    public GameObject getPoint()
    {
        if (_bystanders.Count <= 0)
            Debug.Log("No Bystanders?");

        for (int i = _bystanders.Count - 1; i > -1; i--)
        {
            int random = Random.Range(0, _bystanders.Count);
            if (!
                _bystanders[random].activeSelf)
            {
                _bystanders[random].SetActive(true);
                return _bystanders[random];
            }
        }
        return new GameObject();
    }
}
