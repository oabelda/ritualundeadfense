using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    // Variables publicas

    public float InitialLife;
    public float LifeIncrease;
    public float Damage;
    public float DamageIncrease;
    public float AttackSpeed;
    public float RangeDistance;
    public float Speed;
    public float AttackAnimationTime;
    public float DieAnimationTime;
    public int Gold;
    public GameObject _projectile;
    public Vector2 _direction;
    public LayerMask _layerMask;
    // Variables used to modify the range inside the given values
    public float minVarRange;
    public float maxVarRange;

    // Variables privadas

    public float _life;
    private float _damage;
    private float _lastAtackTime;
    private enum STATE { WALK=0, WALKING, ATTACK, ATTACKING, DIE, DYING }
    private enum TYPE { MEELE = 0, RANGE }
    public enum TYPE2 { WALKER = 0, FLYER }
    private STATE _state;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private GameObject _adversary, newProyectile;
    private SpriteRenderer m_sprite;

    [SerializeField]
    private TYPE _type;
    public TYPE2 _type2;


	// Metodos Awake, Start, Update....
    

	// Use this for initialization
	void Start ()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        _life = InitialLife + GameManager.Round * LifeIncrease;
        _damage = Damage + GameManager.Round * DamageIncrease;
        _lastAtackTime = 0f;
        _state = STATE.WALK;
        // The range will be slightly different so they won't always stack in the same position
        // If Melee, ignore the range
        if (_type == TYPE.MEELE)
        {
            RangeDistance = Random.Range(0.0f, 1.0f);
        }
        else
        {
            RangeDistance = RangeDistance + Random.Range(minVarRange, maxVarRange);
        }
    }

	// Update is called once per frame
	void Update () {
        MainAction();

        if (_state != STATE.DIE && _state != STATE.DYING)
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
            _life = 0;
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
                _rigidbody.velocity = new Vector2(-1 * Speed, 0) * Time.deltaTime;
                _state = STATE.WALKING;
                break;
            case STATE.WALKING:
                break;
            case STATE.ATTACK:
                _rigidbody.velocity = Vector2.zero;
                _lastAtackTime += Time.deltaTime;
                if (_lastAtackTime >= AttackSpeed)
                {
                    _state = STATE.ATTACKING;
                    _animator.SetBool("attacking", true);
                }
                else
                {
                    _animator.SetBool("attacking", false);
                }
                break;
            case STATE.ATTACKING:
                if (_adversary==null)
                {
                    _state = STATE.WALK;
                }
                else
                {
                    switch (_type)
                    {
                        case TYPE.MEELE:
                            _adversary.SendMessage("HitEnemy", _damage);
                            break;
                        case TYPE.RANGE:
                            GameObject projectile = GameObject.Instantiate(_projectile, transform.position, _projectile.transform.rotation) as GameObject;
                            projectile.GetComponent<ProjectileManager>().initParameters(_damage, _direction, tag);
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
                _state = STATE.DYING;
                var c = m_sprite.color;
                c.g = 0f;
                c.b = 0f;
                m_sprite.color = c;
                break;
            case STATE.DYING:
                var color = m_sprite.color;
                color.a -= Time.deltaTime;
                m_sprite.color = color;
                break;
            default:
                break;
        }
    }

    private void CheckForFrontAdversary()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, RangeDistance, _layerMask);
        Debug.DrawRay(transform.position, _direction);
        if (hit.collider != null)
        {
            if (_state != STATE.ATTACKING)
            {
                _adversary = hit.collider.gameObject;
                _state = STATE.ATTACK;
            }
            _animator.SetBool("stop", true);
        }
        else
        {
            _adversary = null;
            _state = STATE.WALK;
            _animator.SetBool("stop", false);
        }
    }


    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator Die(float time)
    {
        _rigidbody.velocity = Vector2.zero;
        _animator.SetTrigger("dead");
        foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
            collider.enabled = false;

        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
