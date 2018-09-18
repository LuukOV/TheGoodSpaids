using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBox {

    public enum BOXTYPE
    {
        MAIN,
        SOCIAL,
        KILLER
    }

    BOXTYPE _type;
    GameObject _deliveryPoint;

    public BOXTYPE Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public GameObject DeliveryPoint
    {
        get { return _deliveryPoint; }
    }

    public DeliveryBox(BOXTYPE type, GameObject _deliveryPointManager)
    {
        _type = type;
        _deliveryPoint = _deliveryPointManager.GetComponent<DeliveryPointManager>().getPoint(type == BOXTYPE.MAIN ? true : false);
    }
}
