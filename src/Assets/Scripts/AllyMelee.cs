using UnityEngine;
using System.Collections.Generic;

public class AllyMelee : Ally
{
	// Variables publicas

	// Variables privadas
    List<GameObject> _enemys = new List<GameObject>();

	// Metodos Awake, Start, Update....

    // Update is called once per frame
    void Update()
    {
        if (_enemys.Count != 0)
        {
            Debug.Log(_enemys.Count);
            _animator.SetBool("attack", true);
            if ((_attackTimer -= Time.deltaTime) <= 0)
            {
                _enemys[0].SendMessage("HitEnemy",Damage);
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
            if (!_enemys.Contains(other.gameObject))
                _enemys.Add(other.gameObject);
        }
    }

    // An enemy exits my attack area
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Collision Exit");
        if (other.CompareTag("Enemy"))
        {
            _enemys.Remove(other.gameObject);
        }
    }

    

	// Otros métodos publicos

	// Otros metodos privados
}
