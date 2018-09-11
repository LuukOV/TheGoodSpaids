using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveScript : MonoBehaviour {

    [SerializeField]
    public float _time = 50f;


	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
        GameManagerScript.GAMERUNNING = false;
	}
	
	// Update is called once per frame
	void Update () {

        _time -= Time.unscaledDeltaTime;
        if(_time <= 0 || Input.GetButton("Start"))
        {
            Invoke("RunGame", 0.00001f);
            RunGame();         
        }
    }

    public void RunGame()
    {
        GameObject.Find("Canvas").GetComponent<GameManagerScript>().ResumeGame();
        GameManagerScript.GAMERUNNING = true;
        Destroy(this.gameObject, 0.00002f);
    } 
}
