using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    // Variables publicas

    public double InitialLife;
    public double LifeIncrease;
    public float Damage;
    public float DamageIncrease;
    public float AttackSpeed;
    public float RangeDistance;
    public float Speed;
    public float AttackAnimationTime;
    public float DieAnimationTime;
    public int Gold;
    public GameObject _projectile;
    public LayerMask _layerMask;

    // Variables privadas

    private double _life;
    private float _damage;
    private float _lastAtackTime;
    private enum STATE { WALK=0, WALKING, ATTACK, ATTACKING, DIE, DYING }
    private enum TYPE { MEELE = 0, RANGE }
    private STATE _state;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private GameObject _adversary, newProyectile;

    [SerializeField]
    private TYPE _type;


	// Metodos Awake, Start, Update....
    

	// Use this for initialization
	void Start ()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _life = InitialLife + GameManager.Round * LifeIncrease;
        _damage = Damage + GameManager.Round * DamageIncrease;
        _lastAtackTime = 0f;
        _state = STATE.WALK;
        // If Melee, ignore the range
        if (_type == TYPE.MEELE)
        {
            RangeDistance = 0.01f;
        }
    }

	// Update is called once per frame
	void Update () {
        MainAction();

        if (_state != STATE.DIE)
            CheckForFrontAdversary();

        
    }
    
    // Otros métodos publicos

    //Function for making the enemy suffer damage
    public void HitEnemy(float damage)
    {
        _life -= damage;
        //Check if damege kills enemy
        if(_life<=0f)
        {
            _state = STATE.DIE;
        }
    }


	// Otros metodos privados

    //Enemy Main Action
    private void MainAction()
    {
        switch(_state)
        {
            case STATE.WALK:
                _animator.SetInteger("State", 1);
                _rigidbody.velocity = new Vector2(-1 * Speed, 0) * Time.deltaTime;
                _state = STATE.WALKING;
                break;
            case STATE.WALKING:
                break;
            case STATE.ATTACK:
                _rigidbody.velocity = Vector2.zero;
                _animator.SetInteger("State", 0);
                _lastAtackTime += Time.deltaTime;
                if (_lastAtackTime >= AttackSpeed)
                    _state = STATE.ATTACKING;
                break;
            case STATE.ATTACKING:
                if (_adversary==null)
                {
                    _state = STATE.WALK;
                }
                else
                {
                    _animator.SetInteger("State", 2);
                    switch (_type)
                    {
                        case TYPE.MEELE:
                            _adversary.SendMessage("HitEnemy", _damage);
                            break;
                        case TYPE.RANGE:
                            GameObject projectile = GameObject.Instantiate(_projectile, transform.position, _projectile.transform.rotation) as GameObject;
                            projectile.GetComponent<ProjectileManager>().initParameters(_damage, Vector2.left, tag);
                            break;
                    }
                }
                StartCoroutine(Wait(AttackAnimationTime));
                _lastAtackTime = 0;
                _state = STATE.ATTACK;
                break;
            case STATE.DIE:
                ShopManager.addGold(Gold);
                StartCoroutine(Die(DieAnimationTime));
                break;
            case STATE.DYING:
                break;
            default:
                break;
        }
    }

    private void CheckForFrontAdversary()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, RangeDistance, _layerMask);
        if (hit.collider != null)
        {
            if (_state != STATE.ATTACKING)
            {
                _adversary = hit.collider.gameObject;
                _state = STATE.ATTACK;
            }
        }
        else
        {
            _adversary = null;
            _state = STATE.WALK;
        }
    }


    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator Die(float time)
    {
        _rigidbody.velocity = Vector2.zero;
        _animator.SetInteger("State", 3);
        foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
            collider.enabled = false;
        this.enabled = false;
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
