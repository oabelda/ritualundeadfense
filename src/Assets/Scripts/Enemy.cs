using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    // Variables publicas

    public double InitialLife;
    public double Damage;
    public float AtackSpeed;



    // Variables privadas

    private double _life;
    private float _lastAtackTime;
    private enum STATE { IDLE=0, WALKING, ATTACKING, DYING }
    private enum TYPE { MEELE = 0, RANGE }
    private STATE _state;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private GameObject _adversary;

    [SerializeField]
    private TYPE _type;


	// Metodos Awake, Start, Update....

	// Use this for spawn this instance
	void Awake(){
	}

	// Use this for initialization
	void Start ()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _life = InitialLife;
        _lastAtackTime = 0f;
        _state = STATE.WALKING;
    }

	// Update is called once per frame
	void Update () {

        MainAction(Time.deltaTime);
    }

    //Stop Walking On TriggerEnter
    void OnTriggerEnter2D(Collider2D other)
    {
        print("He colisionado con Algo");
        if(_type==TYPE.MEELE)
        {
            if (other.CompareTag("Torre"))
            {
                print("He colisionado con la Torre");
                _adversary = other.gameObject;
                _state = STATE.ATTACKING;
            }
            //else if(other.CompareTag("Aliado/INvocado/")) //SOLDADO, ALIADO, INVOCADO como coño sea el TAG
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
            _state = STATE.DYING;
        }
    }




	// Otros metodos privados

    //Enemy Main Action
    private void MainAction(float deltaTime)
    {
        switch(_state)
        {
            case STATE.WALKING:
                switch (_type)
                {
                    case TYPE.MEELE:
                        _animator.SetTrigger("Walk");
                        _rigidbody.velocity = new Vector2(-100, 0) * Time.deltaTime;
                        break;
                    case TYPE.RANGE:
                        //IF(NO estoy en mi posicion final)
                        _rigidbody.velocity = new Vector2(-100, 0) * Time.deltaTime;
                        break;
                }
                break;
            case STATE.ATTACKING:
                _rigidbody.velocity = Vector2.zero;
                _animator.SetTrigger("Idle");
                _lastAtackTime += deltaTime;
                if (_lastAtackTime >= AtackSpeed)
                {
                    switch(_type)
                    {
                        case TYPE.MEELE:
                            if (_adversary.CompareTag("Torre"))
                            {
                                _animator.SetTrigger("Attack");
                                _adversary.GetComponent<Torre>().damageTower(Damage);
                            }
                            //else if (_adversary.CompareTag("AliadoEtc....)
                            break;
                        case TYPE.RANGE:
                            //Shoot proyectile
                            break;
                    }
                    //**ATACAR LANZAR PROYECTIL WHAT EVER (PASAR DAMAGE COMO PARAMETRO);
                    
                    _lastAtackTime = 0;
                }
                break;
            case STATE.DYING:
                _rigidbody.velocity = Vector2.zero;
                _animator.SetTrigger("Die");
                Destroy(this.gameObject);
                break;
            case STATE.IDLE:
            default:
                _rigidbody.velocity = Vector2.zero;
                _animator.SetTrigger("Idle");
                break;
        }
    }
}
