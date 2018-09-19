using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public SocialBoxCounterScript _socialBoxCounterScript;
    public ObjectiveBoxCounterScript _objectiveBoxCounterScript;
    public Text TimeCount;
    public Text ScoreCount;

    public float GameTime = 100;

    private bool gameEnded = false;
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0)
            return;

        GameTime -= Time.deltaTime;
        TimeCount.text = "" +(int)GameTime;

        if(GameTime <= 0 && !gameEnded)
        {
            GetComponent<GameManagerScript>().ActivateLoseScreen();
            gameEnded = true;
        }
	}

    public void IncreaseTime(float amount)
    {
        GameTime += amount;
    }
}
