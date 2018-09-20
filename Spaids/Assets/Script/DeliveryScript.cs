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
    [SerializeField] GameObject _explorerPopup;

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
            if(_inventoryScript.SocializerBoxes >= 10)
            {
                return;
            }
            _audioSource.PlayOneShot(_stealClip);
            _inventoryScript.AddBox(new DeliveryBox(DeliveryBox.BOXTYPE.KILLER, _inventoryScript._deliveryPointManager));
            Destroy(collider.gameObject);
        }
        if(collider.tag == "ExplorerArea")
        {
            Destroy(collider.gameObject);
            _inventoryScript._pointSystem.ExplorerPoints += 10f;
            _explorerPopup.SetActive(true);
            Invoke("DisableExplorerPopup", 3f);
        }
    }

    void DisableExplorerPopup()
    {
        _explorerPopup.SetActive(false);
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Bystander")
        {
            if (_inventoryScript.SocializerBoxes >= 10)
            {
                return;
            }

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
