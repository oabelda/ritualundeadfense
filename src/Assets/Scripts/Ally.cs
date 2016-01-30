using UnityEngine;
using System.Collections;

public class Ally : MonoBehaviour {
    // Variables publicas
    public double Life;
    public double Damage;
    public float AttackSpeed;
    public float SecondsOfDying;

    // Variables privadas
    private double _life;
    protected float _attackTimer;

    protected Animator _animator;

	// Metodos Awake, Start, Update....

	// Use this for spawn this instance
	void Awake(){
        _animator = GetComponent<Animator>();
	}

    // Otros métodos publicos

    //Function for making the enemy suffer damage
    public void HitEnemy(float damage)
    {
        //Check if damege kills enemy
        if ((_life -= damage) <= 0f)
        {
            StartCoroutine(die());
        }
    }

    // Otros metodos privados

    IEnumerator die()
    {
        _animator.SetTrigger("Die");
        foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
            collider.enabled = false;
        yield return new WaitForSeconds(SecondsOfDying);
        Destroy(gameObject);
    } 
}
