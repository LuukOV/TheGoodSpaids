using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [SerializeField]
    private float _damageAmount = 20f;

    [SerializeField] GameObject _hitParticle;

    void OnTriggerEnter(Collider other)
    {

        if (other.transform.parent != null)
        {
            if (other.transform.parent.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponentInParent<EnemyScript>().Damage(_damageAmount);
                GameObject hitParticleEffect = Instantiate(_hitParticle, transform.position - transform.up * 4, Quaternion.FromToRotation(Vector3.up, transform.up));
                Destroy(hitParticleEffect, 1f);
                Destroy(gameObject);
            }
        }
    }
}
