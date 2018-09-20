using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerManager : MonoBehaviour {
    [SerializeField]
    GameObject DeliveryPointManagerObject;
    [SerializeField]
    Image _pointer;
    [SerializeField]
    GameObject _player;

    [SerializeField]
    GameObject _compassSpawn;

    [SerializeField]
    float _pointerDistanceAmp = 10;

    [SerializeField] Sprite _objectiveImage;
    [SerializeField] Sprite _socialImage;

    DeliveryPointManager _deliveryPointManager;
    List<Image> _pointers = new List<Image>();

	// Use this for initialization
	void Start () {
        _deliveryPointManager = DeliveryPointManagerObject.GetComponent<DeliveryPointManager>();
        //Invoke("fillList", 0.0001f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= 0)
            return; // don't update when time is paused

        if (_pointers.Count < 1)
            return;

        for (int i = _deliveryPointManager._deliveryPoints.Count - 1; i > -1; i--)
        {
            GameObject obj = _deliveryPointManager._deliveryPoints[i];

            if (obj.activeSelf) {
                bool IsObjective = obj.GetComponent<PickupSwitchScript>().TypeIsObjective;
                if (IsObjective)
                {
                    _pointers[i].sprite = _objectiveImage;
                }
                else
                {
                    _pointers[i].sprite = _socialImage;
                }
                _pointers[i].gameObject.SetActive(true);
                updatePointer(obj, i);
            }
            else
            {
                _pointers[i].gameObject.SetActive(false);
            }            
        }
	}

    public void FillList()
    {
        foreach (GameObject obj in _deliveryPointManager._deliveryPoints)
        {
            _pointers.Add(Instantiate(_pointer, _compassSpawn.transform.position, Quaternion.FromToRotation(transform.forward, transform.forward), transform));
        }
    }

    void updatePointer(GameObject obj, int index)
    {
        Vector2 pointer = new Vector2(obj.transform.position.x, obj.transform.position.z);
        Vector2 player = new Vector2(_player.transform.position.x, _player.transform.position.z);
        Vector2 playerForward = new Vector2(_player.transform.forward.x, _player.transform.forward.z);
        Vector2 pointerDifferenceVector = (pointer - player);

        float distance = pointerDifferenceVector.magnitude;

        float pointerDegrees = (float)Mathf.Atan2(pointerDifferenceVector.y, pointerDifferenceVector.x) * Mathf.Rad2Deg;
        float cameraDegrees = (float)Mathf.Atan2(playerForward.y, playerForward.x) * Mathf.Rad2Deg;

        float pointerDegreesFromPlayer = cameraDegrees - pointerDegrees;
        if (pointerDegreesFromPlayer < -180) { pointerDegreesFromPlayer += 360; }
        if (pointerDegreesFromPlayer > 180) { pointerDegreesFromPlayer -= 360; }
        pointerDegreesFromPlayer += 180;

        float scale = Mathf.Clamp(1f / (distance / _pointerDistanceAmp), 0, 1);
        _pointers[index].transform.localPosition = new Vector3(Mathf.Lerp(0, 500, (1f / 360f * pointerDegreesFromPlayer)) - 250, _pointers[index].transform.localPosition.y, _pointers[index].transform.localPosition.z);
        _pointers[index].transform.localScale = new Vector3(scale, scale, scale);
    }
}
