using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBarCounterScript : MonoBehaviour {

    [SerializeField] RectTransform _bar;
    [SerializeField] float _time = 3f;
    float _initialWidth = 1f;
    

    void Start()
    {
        gameObject.SetActive(false);
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void DecreaseBar()
    {
        _bar.localScale = new Vector3(_bar.localScale.x - _initialWidth / 3 * Time.deltaTime, 1, 1);
    }

    public void ResetBar()
    {
        _bar.localScale = new Vector3(1, 1, 1);
    }
}
