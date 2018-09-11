using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryScript : MonoBehaviour {

    InventoryScript _inventoryScript;
    [SerializeField]
    GameObject _walkyTalky;

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
            _walkyTalky.GetComponent<WalkyTalkyScript>().Enable();
            collider.gameObject.SetActive(false);

        }
        if(collider.tag == "EnemyPackage")
        {
            _inventoryScript.AddBox(new DeliveryBox(DeliveryBox.BOXTYPE.KILLER, _inventoryScript._deliveryPointManager));
            Destroy(collider.gameObject);
        }
    }
}
