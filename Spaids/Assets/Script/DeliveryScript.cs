using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryScript : MonoBehaviour {

    InventoryScript _inventoryScript;
    AudioSource _audioSource;
    [SerializeField] GameObject _walkyTalky;
    [SerializeField] PickupBarCounterScript _pickupBarCounterScript;
    [SerializeField] AudioClip _deliveryClip;
    [SerializeField] AudioClip _stealClip;

    float _bystanderTimer = 0;
    [SerializeField] float _bystanderTime = 3f;

    void Start()
    {
        _inventoryScript = GetComponent<InventoryScript>();
        _audioSource = GetComponents<AudioSource>()[1];
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "DeliveryPoint")
        {
            _inventoryScript.DeliverBox(collider.gameObject);
            _audioSource.PlayOneShot(_deliveryClip);
        }
        if(collider.tag == "EnemyPackage")
        {
            _audioSource.PlayOneShot(_stealClip);
            _inventoryScript.AddBox(new DeliveryBox(DeliveryBox.BOXTYPE.KILLER, _inventoryScript._deliveryPointManager));
            Destroy(collider.gameObject);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Bystander")
        {
            _bystanderTimer += Time.deltaTime;
            _pickupBarCounterScript.gameObject.SetActive(true);
            _pickupBarCounterScript.DecreaseBar();
            if (_bystanderTimer >= _bystanderTime)
            {
                _inventoryScript.AddBox(new DeliveryBox(DeliveryBox.BOXTYPE.SOCIAL, _inventoryScript._deliveryPointManager));
                _walkyTalky.GetComponent<WalkyTalkyScript>().Enable();
                collider.gameObject.SetActive(false);
                _bystanderTimer = 0f;
                _pickupBarCounterScript.ResetBar();
                _pickupBarCounterScript.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Bystander")
        {
            _pickupBarCounterScript.ResetBar();
            _pickupBarCounterScript.gameObject.SetActive(false);
            _bystanderTimer = 0f;
        }
    }
}
