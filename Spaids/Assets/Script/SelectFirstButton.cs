using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectFirstButton : MonoBehaviour {

    GameObject _button;

    void Start()
    {/*
        // select first button if controller connected
        if (Input.GetJoystickNames().Length > 0)
        {
            _button = transform.GetChild(0).gameObject;
            EventSystem.current.SetSelectedGameObject(_button, null);
        }*/
    }

    void Update()
    {
        if(Input.GetAxis("Vertical") != 0 && EventSystem.current.currentSelectedGameObject == null)
        {
            SelectButton();
        }
    }

    public void SelectButton()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            _button = transform.GetChild(0).gameObject;
            EventSystem.current.SetSelectedGameObject(_button, null);
        }
    }
}
