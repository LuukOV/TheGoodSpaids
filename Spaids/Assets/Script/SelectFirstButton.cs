using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectFirstButton : MonoBehaviour {

    GameObject _button;

    void OnEnable()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            _button = transform.GetChild(0).gameObject;
            EventSystem.current.SetSelectedGameObject(_button, null);
            _button.GetComponent<Button>().OnSelect(null);
        }
    }

    void Update()
    {
        if(((Input.GetAxis("Vertical") != 0 || Input.GetButton("Vertical")) || (Input.GetAxis("Horizontal") != 0 || Input.GetButton("Horizontal"))) && EventSystem.current.currentSelectedGameObject == null)
        {
            SelectButton();
        }
    }

    public void SelectButton()
    {
        /*
        if (Input.GetJoystickNames().Length > 0)
        {*/

        Debug.Log("hmm");
            _button = transform.GetChild(0).gameObject;
            EventSystem.current.SetSelectedGameObject(_button, null);
        //}
    }
}
