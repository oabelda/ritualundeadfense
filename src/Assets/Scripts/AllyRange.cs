using UnityEngine;
using System.Collections.Generic;

public class AllyRange : Ally
{
    // Variables publicas
    public GameObject m_projectile;

    // Variables privadas
    List<GameObject> _enemys = new List<GameObject>();

    // Metodos Awake, Start, Update....

    // Update is called once per frame
    void Update()
    {
        if (_enemys.Count != 0)
        {
            if (_enemys[0] == null)
            {
                _enemys.RemoveAt(0);
            }
            _animator.SetBool("attack", true);
            if ((_attackTimer -= Time.deltaTime) <= 0)
            {
                GameObject projectile = GameObject.Instantiate(m_projectile, transform.position, m_projectile.transform.rotation) as GameObject;
                // @TODO: Gestionar la inicialización del proyectil
                _attackTimer = AttackSpeed;
            }
        }
        else
        {
            _animator.SetBool("attack", false);
        }
    }

    // An enemy enters my attack area
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log(other.gameObject.name + " : " + other);
            if (!_enemys.Contains(other.gameObject))
                _enemys.Add(other.gameObject);
        }
    }

    // An enemy exits my attack area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemys.Remove(other.gameObject);
        }
    }
}
