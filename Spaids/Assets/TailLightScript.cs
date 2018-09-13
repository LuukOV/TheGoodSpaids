using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailLightScript : MonoBehaviour {

    List<Light> _lights = new List<Light>();
	// Use this for initialization
	void Start () {
        foreach (Light light in GetComponentsInChildren<Light>())
        {
            _lights.Add(light);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("LT_TRIGGER") != 0) // Left Trigger
        {
            EnableTailLights();
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))// S/Down
        {
            EnableTailLights();
        }
        else
        {
            DisableTailLights();
        }


    }

    void EnableTailLights()
    {
        foreach (Light light in _lights)
        {
            //light.enabled = true;
            light.intensity = 2f;
        }
    }

    void DisableTailLights()
    {
        foreach (Light light in _lights)
        {
            //light.enabled = false;
            light.intensity = 0.5f;
        }
    }
}
