using UnityEngine;
using System.Collections.Generic;

public class AllyRange : Ally
{
    // Variables publicas
    public GameObject m_projectile;
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
            _animator.SetBool("attacking", true);
            if ((_attackTimer -= Time.deltaTime) <= 0)
            {
                GameObject projectile = GameObject.Instantiate(m_projectile, transform.position, m_projectile.transform.rotation) as GameObject;
                projectile.GetComponent<ProjectileManager>().initParameters(Damage,Vector2.right,tag);
                _attackTimer = AttackSpeed;
            }
            else
            {
                _animator.SetBool("attacking", false);
            }
        }
        else
        {
            _animator.SetBool("attacking", false);
        }
    }
}
