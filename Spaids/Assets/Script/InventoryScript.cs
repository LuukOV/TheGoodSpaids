using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {

    [SerializeField]
    private GameObject UIManager;
    private UIManager _UIManagerScript;
    public PointSystemScript _pointSystem;

    private List<DeliveryBox> _objectiveBoxes = new List<DeliveryBox>();
    private List<DeliveryBox> _socialBoxes = new List<DeliveryBox>();
    private List<DeliveryBox> _killerBoxes = new List<DeliveryBox>();

    public GameObject _deliveryPointManager;

	// Use this for initialization
	void Start () {
        Invoke("CreateObjective", 0.1f);
        _UIManagerScript = UIManager.GetComponent<UIManager>();
        _pointSystem = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PointSystemScript>();
	}
	
    void CreateObjective()
    {
        for (int i = 0; i < 10; i++)
        {
            AddBox(new DeliveryBox(DeliveryBox.BOXTYPE.MAIN, _deliveryPointManager));
        }
    }

	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0)
            return; // don't update when time is paused

        /*
        _UIManagerScript.SetObjectiveCount(_objectiveBoxes.Count);
        _UIManagerScript.SetSocialCount(_socialBoxes.Count);
        _UIManagerScript.SetKillerCount(_killerBoxes.Count);*/
	}

    int collectedBoxes()
    {
        return _objectiveBoxes.Count + _socialBoxes.Count + _killerBoxes.Count;
    }

    public void AddBox(DeliveryBox deliveryBox)
    {
        if(_deliveryPointManager.GetComponent<DeliveryPointManager>()._deliveryPoints.Count <= collectedBoxes())
        {
            return; //if you can't carry more boxes
        }

        switch (deliveryBox.Type)
        {
            case DeliveryBox.BOXTYPE.MAIN:
                _objectiveBoxes.Add(deliveryBox);
                break;
            case DeliveryBox.BOXTYPE.KILLER:
                {
                    _pointSystem.TotalBoxesCollected++;
                    _killerBoxes.Add(deliveryBox);
                   // _UIManagerScript._socialBoxCounterScript.AddBox();
                    break;
                }
            case DeliveryBox.BOXTYPE.SOCIAL:
                {
                    _pointSystem.TotalBoxesCollected++;
                    _socialBoxes.Add(deliveryBox);
                   // _UIManagerScript._socialBoxCounterScript.AddBox();
                    break;
                }
        }
    }

    public void DeliverBox(GameObject deliveryPoint)
    {
        for(int i = _objectiveBoxes.Count - 1; i > -1; i--)
        {
            if (ValidatePoint(_objectiveBoxes[i], deliveryPoint))
            {
               // _pointSystem.AchieverPoints += 10f;
                _objectiveBoxes.Remove(_objectiveBoxes[i]);
                _UIManagerScript.IncreaseTime(10f);
               // _UIManagerScript._objectiveBoxCounterScript.RemoveBox();

                if (_objectiveBoxes.Count < 1) // finish game
                {
                    _UIManagerScript.GetComponent<GameManagerScript>().ActivateEndScreen();
                }
            }
        }

        for (int i = _killerBoxes.Count - 1; i > -1; i--)
        {
            if (ValidatePoint(_killerBoxes[i], deliveryPoint))
            {
                _pointSystem.SocializerPoints += 10f;
                _killerBoxes.Remove(_killerBoxes[i]);
               // _UIManagerScript._socialBoxCounterScript.RemoveBox();
            }
        }

        for (int i = _socialBoxes.Count - 1; i > -1; i--)
        {
            if (ValidatePoint(_socialBoxes[i], deliveryPoint))
            {
                _pointSystem.SocializerPoints += 10f;
                _socialBoxes.Remove(_socialBoxes[i]);
              //  _UIManagerScript._socialBoxCounterScript.RemoveBox();
            }
        }
    }

    bool ValidatePoint(DeliveryBox box, GameObject pointLocation)
    {
        if (box.DeliveryPoint == pointLocation)
        {
            pointLocation.SetActive(false);
            // points etc
            return true;
        }
        return false;
    }
}
