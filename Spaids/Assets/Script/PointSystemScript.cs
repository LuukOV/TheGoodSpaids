using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystemScript : MonoBehaviour {

    private UIManager _uiManager;

    public float AchieverPoints = 0f;
    public float ExplorerPoints = 0f;
    public float SocializerPoints = 0f;
    public float KillerPoints = 0f;
    public int TotalBoxesCollected = 0;

    public float AddAchieverPoints
    {
        get { return (10 + TotalBoxesCollected) * _uiManager.GameTime; }
    }

    public float TotalPoints
    {
        get { return AchieverPoints + ExplorerPoints + SocializerPoints + KillerPoints; }
    }

    void Start()
    {
        _uiManager = GetComponent<UIManager>();
    }

    void Update()
    {
        _uiManager.ScoreCount.text = "" + (int)TotalPoints;
    }
}
