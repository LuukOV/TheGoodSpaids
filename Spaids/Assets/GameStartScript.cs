using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour {

    [SerializeField] UIManager _uIManager;
    [SerializeField] GameObject _building;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _uIManager.StartGame();
            _building.SetActive(true);
        }
    }
}
