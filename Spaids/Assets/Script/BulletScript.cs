using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [SerializeField]
    private float _damageAmount = 20f;

    void OnTriggerEnter(Collider other)
    {

        if (other.transform.parent != null)
        {
            if (other.transform.parent.gameObject.tag == "Enemy")
            {
                other.transform.parent.gameObject.GetComponent<EnemyScript>().Damage(_damageAmount);
            }
        }
        Destroy(gameObject);
    }
}
