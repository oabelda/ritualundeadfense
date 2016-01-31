using UnityEngine;
using System.Collections;

public class TowerProjectileManager : MonoBehaviour
{
    // Variables publicas
    [Header("Projectile properties")]
    [SerializeField]
    float m_speed;
    [SerializeField]
    float m_lifetime;
    [SerializeField]
    float m_radius;
    [SerializeField]
    float m_aoedamage;

    // Variables privadas
    private float m_damage;
    private Vector2 direction;
    private Rigidbody2D _rigidbody;
    private GameObject _collider;
    private Vector3 _initialposition;
    private float _angle;
    void Awake()
    {
        Destroy(this.gameObject, m_lifetime);
    }
    // Metodos Awake, Start, Update....
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        moveProjectile();
    }

    // Otros métodos publicos
    public void initParameters(float damage, Vector2 dir, string tag, Transform waypoint)
    {
        m_damage = damage;
        direction = dir;
        
        _angle = Mathf.Atan2(direction.y, direction.x);
        float angleMax = Mathf.PI;
        float angleMin = Mathf.Atan2(waypoint.position.y, waypoint.position.x);

        if (_angle > angleMin)
        {
            _angle = angleMin;
            float modulo = direction.magnitude;
            direction = waypoint.position - transform.position;
            direction = direction.normalized * modulo;

        }
        else if (_angle < angleMax)
            _angle = angleMax;

        direction = new Vector2(direction.x + Random.Range(-2f, 2f), direction.y);
        transform.Rotate(new Vector3(0, 0, _angle * 90 / Mathf.PI));
    }
    // Otros metodos privados
    void moveProjectile()
    {
        
        _rigidbody.velocity = direction * m_speed * Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SendMessage("HitEnemy", m_damage, SendMessageOptions.RequireReceiver);
            explosion();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Ally") || other.CompareTag("Torre"))
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.GetComponent<Collider2D>(), true);
        else
        {
            explosion();
            Destroy(this.gameObject);
        }

    }

    

    void explosion()
    {
       Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), m_radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Enemy"))
            {
                colliders[i].gameObject.SendMessage("HitEnemy", m_aoedamage, SendMessageOptions.RequireReceiver);
            }
        }
    }
}

