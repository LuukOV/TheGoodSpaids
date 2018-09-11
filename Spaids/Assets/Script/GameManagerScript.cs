using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public static bool GAMERUNNING = true;

    [SerializeField]
    GameObject _endScreen;
    [SerializeField]
    GameObject _loseScreen;
    [SerializeField]
    GameObject _pauseScreen;
    [SerializeField]
    Image _walkyTalky;

    void Update()
    {
        if (!GAMERUNNING)
            return;

        if (Input.GetButtonDown("Start"))
        {
            if (Time.timeScale != 1)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
                _pauseScreen.SetActive(true);
            }
        }
    }

    void PauseGame()
    {
        if (Input.GetJoystickNames().Length > 0) // select button if controller selected
        {
            GameObject first = _pauseScreen.transform.GetChild(0).gameObject;
            EventSystem.current.SetSelectedGameObject(first, null);
            first.GetComponent<Button>().OnSelect(null);
        }


        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        if(_pauseScreen != null)
            _pauseScreen.SetActive(false);

        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public void GoToMain()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void ReloadGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActivateEndScreen()
    {
        GAMERUNNING = false;
        PauseGame();
        _endScreen.SetActive(true);
    }

    public void ActivateLoseScreen()
    {
        GAMERUNNING = false;
        PauseGame();
        _loseScreen.SetActive(true);
    }

    public void ShowWalkyTalky()
    {
        _walkyTalky.gameObject.SetActive(true);
    }
}
