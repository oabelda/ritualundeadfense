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


    // Variables privadas
    private float m_damage;
    private Vector2 direction;
    private Rigidbody2D _rigidbody;
    private GameObject _collider;
    private Vector3 _initialposition;
    private string _shootertag;

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
    public void initParameters(float damage, Vector2 dir, string tag)
    {
        m_damage = damage;
        direction = dir;
        _shootertag = tag;
    }
    // Otros metodos privados
    void moveProjectile()
    {

        _rigidbody.velocity = direction * m_speed * Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (_shootertag)
        {
            case "Torre":
            case "Ally":
                if (other.CompareTag("Enemy"))
                {
                    other.gameObject.SendMessage("HitEnemy", m_damage, SendMessageOptions.RequireReceiver);

                    Destroy(this.gameObject);
                }
                else if (other.CompareTag("Ally") || other.CompareTag("Torre"))
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.GetComponent<Collider2D>(), true);



                break;
            case "Enemy":
                if (other.CompareTag("Ally"))
                {
                    other.gameObject.SendMessage("HitEnemy", m_damage, SendMessageOptions.RequireReceiver);
                    Destroy(this.gameObject);
                }
                else if (other.CompareTag("Torre"))
                {
                    other.SendMessage("HitEnemy", m_damage, SendMessageOptions.RequireReceiver);
                    Destroy(this.gameObject);
                }
                else if (other.CompareTag("Enemy"))
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.GetComponent<Collider2D>(), true);

                break;
        }
    }
}

