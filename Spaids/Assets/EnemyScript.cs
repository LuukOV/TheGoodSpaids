using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [SerializeField]
    private float _health = 100f;

    [SerializeField]
    private GameObject _package;

    public void Damage(float damageAmount)
    {
        _health -= damageAmount;

        if(_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(_package, transform.position, Quaternion.FromToRotation(Vector3.forward, Vector3.forward));
        Debug.Log("NOOOOOOOO!");
        Destroy(gameObject);
    }
}
