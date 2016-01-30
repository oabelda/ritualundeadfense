using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    // Variables publicas

    public double InitialLife;
    public float Damage;
    public float AttackSpeed;
    public float RangeDistance;
    public float Speed;
    public float AttackAnimationTime;
    public float DieAnimationTime;
    public GameObject projectile;



    // Variables privadas

    private double _life;
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
        _life = InitialLife;
        _lastAtackTime = 0f;
        _state = STATE.WALK;
    }

	// Update is called once per frame
	void Update () {
        MainAction();

        if (_type == TYPE.RANGE)
        CheckForFrontAdversary();

        
    }

    //Stop Walking On TriggerEnter
    void OnTriggerEnter2D(Collider2D other)
    {
        if(_type==TYPE.MEELE)
        {
            if (other.CompareTag("Torre") || other.CompareTag("Ally"))
            {
                _adversary = other.gameObject;
                _state = STATE.ATTACK;
            }
        }
    }

    //Go on Walking On TriggerExit
    void OnTriggerExit2D(Collider2D other)
    {
        if (_type == TYPE.MEELE)
        {
            if (other.CompareTag("Torre") || other.CompareTag("Ally"))
            {
                _adversary = null;
                _state = STATE.WALK;
            }
        }
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
                _rigidbody.velocity = new Vector2(Speed, 0) * Time.deltaTime;
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
                else if (_adversary.CompareTag("Torre") || _adversary.CompareTag("Ally"))
                {
                    _animator.SetInteger("State", 2);
                    switch (_type)
                    {
                        case TYPE.MEELE:
                            _adversary.SendMessage("HitEnemy", Damage);
                            break;
                        case TYPE.RANGE:
                            //Shoot proyectile   //**ATACAR LANZAR PROYECTIL WHAT EVER (PASAR DAMAGE COMO PARAMETRO);
                            newProyectile = Instantiate(projectile, transform.position + Vector3.left, Quaternion.identity) as GameObject;
                            newProyectile.GetComponent<ProjectileManager>().initParameters(Damage,Vector2.left,this.gameObject.tag);
                            break;
                    }
                }
                StartCoroutine(Wait(AttackAnimationTime));
                _lastAtackTime = 0;
                _state = STATE.ATTACK;
                break;
            case STATE.DIE:
                _rigidbody.velocity = Vector2.zero;
                _animator.SetInteger("State", 3);
                StartCoroutine(Wait(DieAnimationTime));
                Destroy(this.gameObject);
                break;
            case STATE.DYING:
                break;
            default:
                break;
        }
    }

    private void CheckForFrontAdversary()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-1f,0,0), Vector2.left, RangeDistance);
        if (hit.collider != null)
        {
            //Debug.Log("Colision detectada "+ hit.collider.gameObject.name);
            if (_state != STATE.ATTACKING)
            {
                if (hit.collider.CompareTag("Torre") || hit.collider.CompareTag("Ally"))
                {
                    _adversary = hit.collider.gameObject;
                    _state = STATE.ATTACK;
                }
                else
                {
                    _adversary = null;
                    _state = STATE.WALK;
                }
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
}
