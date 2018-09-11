using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    [SerializeField]
    GameObject _endScreen;
    [SerializeField]
    GameObject _loseScreen;

    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActivateEndScreen()
    {
        _endScreen.SetActive(true);
    }

    public void ActivateLoseScreen()
    {
        _loseScreen.SetActive(true);
    }
}
