using UnityEngine;
using System.Collections.Generic;

public class AllyMelee : Ally
{
	// Variables publicas
    public float RangeDistance;
    public LayerMask m_LayerMask;

	// Variables privadas

	// Metodos Awake, Start, Update....

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, RangeDistance, m_LayerMask);
        if (hit.collider != null)
        {
            _animator.SetBool("attack", true);
            if ((_attackTimer -= Time.deltaTime) <= 0)
            {
                hit.collider.gameObject.SendMessage("HitEnemy",Damage);
                _attackTimer = AttackSpeed;
            }
        }
        else
        {
            _animator.SetBool("attack", false);
        }
    }

	// Otros métodos publicos

	// Otros metodos privados
}
