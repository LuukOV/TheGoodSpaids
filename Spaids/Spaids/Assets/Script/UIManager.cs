using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text ObjectiveBoxesCount;
    public Text SocialBoxesCount;
    public Text KillerBoxesCount;
    public Text TimeCount;

    public static float GameTime = 100;
	
	// Update is called once per frame
	void Update () {
        GameTime -= Time.deltaTime;
        TimeCount.text = "> " + (int)GameTime;
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
}
