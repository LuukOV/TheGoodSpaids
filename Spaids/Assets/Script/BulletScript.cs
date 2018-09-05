using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [SerializeField]
    private float _damageAmount = 20f;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyScript>().Damage(_damageAmount);
        }
        Destroy(gameObject);
    }
}
