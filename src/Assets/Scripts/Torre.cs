using UnityEngine;
using System.Collections;




public class Torre : MonoBehaviour {
    [Header("Tower Properties")]
    [SerializeField]
    double _towerMaxHP = 100;
    [SerializeField]
    double _towerMaxMana = 100;
    [SerializeField]
    float _towerDamage;
    [SerializeField]
    float _towerCooldown;

    [SerializeField]
    double _hpPerLevel;
    [SerializeField]
    double _manaPerLevel;

    [System.Serializable]
    public class spritesTower
    {
        public Sprite m_sprite;
        public double life;
    }

    public spritesTower[] sprites;
    
    [SerializeField]
    Sprite _healthyTower;
    [SerializeField]
    double manaRegen = 1;
    public GameObject projectile;
    public Vector3 _projectileSpawnDeviation;

    private double _towerCurrentHP;
    private double _towerCurrentMana;
    private SpriteRenderer _towerSprite;
    private float _towerCooldownTimer;
    // Use this for initialization
    void Start () {
        _towerCurrentHP = _towerMaxHP;
        _towerSprite = GetComponent<SpriteRenderer>();
        if (_towerSprite.sprite == null)
            _towerSprite.sprite = _healthyTower;

        _towerCooldownTimer = _towerCooldown;
        GameManager.OnRoundChange += OnRoundChange;
    }

    void Update()
    {
        registerMouse();
        _towerCurrentMana += manaRegen;
        _towerCurrentMana = (_towerCurrentMana<_towerMaxHP)? _towerCurrentMana : _towerMaxHP;
    }

    // Función para registrar el daño que recibe la torre
    public void HitEnemy(double damageReceived)
    {
        _towerCurrentHP -= damageReceived;
        if (_towerCurrentHP > (_towerMaxHP * sprites[sprites.Length - 1].life) && _towerSprite.sprite != _healthyTower)
            _towerSprite.sprite = _healthyTower;
        else
            this.changeSprite();
        
    }


    // Función para registrar las reparaciones que hace el jugador a la torre
    public void repairTower(float percentage)
    {
        double repairAmount = _towerMaxHP * percentage;
        if (_towerCurrentHP < _towerMaxHP) // Solo se repara si la vida actual es menor que la vida máxima
        {
            _towerCurrentHP += repairAmount;

            if (_towerCurrentHP > _towerMaxHP) // Si la reparación pone la vida por encima de la vida máxima, se iguala con la vida máxima
                _towerCurrentHP = _towerMaxHP;

            if (_towerCurrentHP > (_towerMaxHP * sprites[sprites.Length - 1].life) && _towerSprite.sprite != _healthyTower)
                _towerSprite.sprite = _healthyTower;
            else
                this.changeSprite();
        }
    }

    public void restoreMana(float percentage)
    {
        double restoredMana = _towerMaxMana * percentage;
        if (_towerCurrentMana < _towerMaxMana) // Solo se repara si la vida actual es menor que la vida máxima
        {
            _towerCurrentMana += restoredMana;

            if (_towerCurrentMana > _towerMaxMana) // Si la reparación pone la vida por encima de la vida máxima, se iguala con la vida máxima
                _towerCurrentMana = _towerMaxMana;
        }
    }

    void OnRoundChange()
    {
        _towerMaxHP += _hpPerLevel;
        _towerMaxMana += _manaPerLevel;
    }

    void changeSprite()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (_towerCurrentHP <= (_towerMaxHP * sprites[i].life))
            {
                _towerSprite.sprite = sprites[i].m_sprite;
                break;
            }
        }
    }

    void registerMouse()
    {
        if (_towerCooldownTimer >= _towerCooldown)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 _throwPosition = transform.position + _projectileSpawnDeviation;
                Vector3 _directionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _throwPosition;

                GameObject newProjectile = Instantiate(projectile, _throwPosition, Quaternion.identity) as GameObject;
                newProjectile.GetComponent<ProjectileManager>().initParameters(_towerDamage, _directionVector, this.gameObject.tag);
                _towerCooldownTimer = 0.0f;
            }
        }
        _towerCooldownTimer += Time.deltaTime;
    }



}
