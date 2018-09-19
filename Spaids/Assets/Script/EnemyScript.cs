using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [SerializeField] private float _health = 100f;
    [SerializeField] private float _killerPoints = 10f;
    [SerializeField] private bool _dropPackage = true;
    [SerializeField] private GameObject _package;
    [SerializeField] private GameObject _explosion;

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
        GameObject.FindWithTag("Canvas").GetComponent<PointSystemScript>().KillerPoints += _killerPoints;
        Instantiate(_explosion, transform.position, Quaternion.FromToRotation(Vector3.forward, Vector3.forward));
        if (_dropPackage) {
            Instantiate(_package, transform.position, Quaternion.FromToRotation(Vector3.forward, Vector3.forward));
        }
        Destroy(gameObject);
    }
}
