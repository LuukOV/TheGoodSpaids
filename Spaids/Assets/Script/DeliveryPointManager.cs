using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPointManager : MonoBehaviour {

    public List<GameObject> _deliveryPoints = new List<GameObject>();

	// Use this for initialization
	void Start () {
        if (_deliveryPoints.Count <= 0)
        {
            checkPoints();
        }
	}

    void checkPoints()
    {
        foreach (Transform gObject in GetComponentInChildren<Transform>())
        {
            if (gObject != transform)
            {
                _deliveryPoints.Add(gObject.gameObject);
                gObject.gameObject.SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject getPoint()
    {
        if (_deliveryPoints.Count <= 0)
            Debug.Log("No Delivery points?");

        for (int i = _deliveryPoints.Count - 1; i > -1; i--)
        {
            int random = Random.Range(0, _deliveryPoints.Count);
            if (!_deliveryPoints[random].activeSelf)
            {
                _deliveryPoints[random].SetActive(true);
                return _deliveryPoints[random];
            }
        }

        return new GameObject();
    }
}
