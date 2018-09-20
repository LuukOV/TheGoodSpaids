using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    public static bool GAMERUNNING = true;

    [SerializeField] GameObject _endScreen;
    [SerializeField] GameObject _loseScreen;
    [SerializeField] GameObject _pauseScreen;
    [SerializeField] Image _walkyTalky;
    [SerializeField] Image _loseImage;
    [SerializeField] Sprite _loseSprite;

    [SerializeField] Text _achieverScore;
    [SerializeField] Text _explorerScore;
    [SerializeField] Text _socializerScore;
    [SerializeField] Text _killerScore;
    [SerializeField] Text _totalScore;

    [SerializeField] GameObject _endPoint;

    PointSystemScript _pointSystemScript;

    void Start()
    {
        _pointSystemScript = GetComponent<PointSystemScript>();
    }

    void Update()
    {
        if (!GAMERUNNING)
            return;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            ActivateEndScreen();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ActivateLoseScreen();
        }

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
        GAMERUNNING = true;
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void ReloadGame()
    {
        GAMERUNNING = true;
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActivateEndScreen()
    {
        GAMERUNNING = false;
        PauseGame();
        DisableHUD();
        _endScreen.SetActive(true);
        _pointSystemScript.AddAchieverPoints();
        _achieverScore.text = "" + (int)_pointSystemScript.AchieverPoints;
        _socializerScore.text = "" + (int)_pointSystemScript.SocializerPoints;
        _killerScore.text = "" + (int)_pointSystemScript.KillerPoints;
        _explorerScore.text = "" + (int)_pointSystemScript.ExplorerPoints;
        _totalScore.text = "" + (int)_pointSystemScript.TotalPoints;
        SetPercentages();
        _endPoint.gameObject.SetActive(true);
    }

    public void ActivateLoseScreen()
    {
        _loseImage.sprite = _loseSprite;
        ActivateEndScreen();
    }

    public void DisableHUD()
    {
        foreach(Transform gObject in GetComponentsInChildren<Transform>())
        {
            if(gObject != transform)
            {
                gObject.gameObject.SetActive(false);
            }
        }
    }

    public void SetPercentages()
    {
        float OnePercent = 100 / _pointSystemScript.TotalPoints;
        float percentageAchiever = OnePercent * _pointSystemScript.AchieverPoints;
        float percentageSocializer = OnePercent * _pointSystemScript.SocializerPoints;
        float percentageExplorer = OnePercent * _pointSystemScript.ExplorerPoints;
        float percentageKiller = OnePercent * _pointSystemScript.KillerPoints;

        _achieverScore.text +=  "  -  %" + Mathf.Round(percentageAchiever);
        _socializerScore.text += "  -  %" + Mathf.Round(percentageSocializer);
        _explorerScore.text += "  -  %" + Mathf.Round(percentageExplorer);
        _killerScore.text += "  -  %" + Mathf.Round(percentageKiller);
    }

    public void ShowWalkyTalky()
    {
        _walkyTalky.gameObject.SetActive(true);
    }
}
