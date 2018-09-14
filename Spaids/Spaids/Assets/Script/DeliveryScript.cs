using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryScript : MonoBehaviour {

    InventoryScript _inventoryScript;

    void Start()
    {
        _inventoryScript = GetComponent<InventoryScript>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "DeliveryPoint")
        {
            _inventoryScript.DeliverBox(collider.gameObject);
        }
        if(collider.tag == "Bystander")
        {
            _inventoryScript.AddBox(new DeliveryBox(DeliveryBox.BOXTYPE.SOCIAL, _inventoryScript._deliveryPointManager));
            collider.gameObject.SetActive(false);
        }
        if(collider.tag == "EnemyPackage")
        {
            _inventoryScript.AddBox(new DeliveryBox(DeliveryBox.BOXTYPE.KILLER, _inventoryScript._deliveryPointManager));
            Destroy(collider.gameObject);
        }
    }
}
