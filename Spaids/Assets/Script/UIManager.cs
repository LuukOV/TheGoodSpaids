using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text ObjectiveBoxesCount;
    public Text SocialBoxesCount;
    public Text KillerBoxesCount;
    public Text TimeCount;

    public float GameTime = 100;

    private bool gameEnded = false;
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0)
            return;

        GameTime -= Time.deltaTime;
        TimeCount.text = "> " + (int)GameTime;

        if(GameTime <= 0 && !gameEnded)
        {
            GetComponent<GameManagerScript>().ActivateLoseScreen();
            gameEnded = true;
        }
	}

    public void SetObjectiveCount(int count)
    {
        ObjectiveBoxesCount.text = "Objective X " + count;
    }

    public void SetSocialCount(int count)
    {
        SocialBoxesCount.text = "Social X " + count;
    }

    public void SetKillerCount(int count)
    {
        KillerBoxesCount.text = "Killer X " + count;
    }

    public void IncreaseTime(float amount)
    {
        GameTime += amount;
    }
}
