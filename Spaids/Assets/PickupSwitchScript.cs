﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSwitchScript : MonoBehaviour {
    public GameObject _socialPickup;
    public GameObject _objectivePickup;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DestroyOffspring()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void EnableTypeObjective()
    {
        DestroyOffspring();
        Instantiate(_objectivePickup, transform);
    }

    public void EnableTypeSocial()
    {
        DestroyOffspring();
        Instantiate(_socialPickup, transform);
    }
}
