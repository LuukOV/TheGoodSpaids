using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour {

    [SerializeField]
    private float _timeTillDestruction = 10;
	
    void Start()
    {
        Destroy(gameObject, _timeTillDestruction);
    }
}
