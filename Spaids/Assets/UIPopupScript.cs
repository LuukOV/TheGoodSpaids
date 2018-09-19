using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupScript : MonoBehaviour {

    [SerializeField] GameObject UIPopup;
    bool open = false;
    [SerializeField] float _timeOpen = 10f;
    float _timer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (open)
        {
            _timer += Time.deltaTime;
            if(_timer >= _timeOpen)
            {
                UIPopup.SetActive(false);
                open = false;
                _timer = 0;
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (open)
            return;

        if (other.tag == "Player")
        {
            UIPopup.SetActive(true);
            open = true;
        }
    }
}
