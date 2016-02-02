using UnityEngine;
using System.Collections;




public class Torre : MonoBehaviour {
    [Header("Tower Properties")]
    [SerializeField]
    float _towerDamage;
    [SerializeField]
    float _towerCooldown;

    [SerializeField]
    Transform _waypointInicio;
    [SerializeField]
    Transform _waypointFinal;

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


    public double TowerMaxHP = 100;
    public double TowerMaxMana = 100;
    public double TowerCurrentHP;
    public double TowerCurrentMana;

    public GameObject projectile;
    public Vector3 _projectileSpawnDeviation;
    

    private SpriteRenderer _towerSprite;
    private float _towerCooldownTimer;
    private Transform _nigromante;
    private Animator _animatorNigromante;
    private Animator _animatorRehen;
    private bool _animationRehenDone;
    // Use this for initialization
    void Start () {
        TowerCurrentHP = TowerMaxHP;
        TowerCurrentMana = TowerMaxMana;
        _towerSprite = GetComponent<SpriteRenderer>();
        if (_towerSprite.sprite == null)
            _towerSprite.sprite = _healthyTower;

        _towerCooldownTimer = _towerCooldown;
        GameManager.OnRoundChange += OnRoundChange;
        _nigromante = transform.FindChild("Nigromante").GetComponent<Transform>();
        _animatorNigromante = _nigromante.gameObject.GetComponent<Animator>();
        _animatorRehen = transform.FindChild("Rehen").GetComponent<Animator>();
    }

    void Update()
    {
        _towerCooldownTimer += Time.deltaTime;
        TowerCurrentMana += manaRegen * Time.deltaTime;
        TowerCurrentMana = (TowerCurrentMana<TowerMaxMana)? TowerCurrentMana : TowerMaxMana;

        int num = Random.Range(0, 100);
        if(num < 25 && !_animationRehenDone)
        {
            animationRehen();
        }
    }

    // Función para registrar el daño que recibe la torre
    public void HitEnemy(double damageReceived)
    {
        TowerCurrentHP -= damageReceived;
        if (TowerCurrentHP > (TowerMaxHP * sprites[sprites.Length - 1].life) && _towerSprite.sprite != _healthyTower)
            _towerSprite.sprite = _healthyTower;
        else
            this.changeSprite();
        if (TowerCurrentHP <= 0)
            GameManager.finishGame();
        
    }


    // Función para registrar las reparaciones que hace el jugador a la torre
    public void repairTower(float percentage)
    {
        double repairAmount = TowerMaxHP * percentage;
        if (TowerCurrentHP < TowerMaxHP) // Solo se repara si la vida actual es menor que la vida máxima
        {
            TowerCurrentHP += repairAmount;

            if (TowerCurrentHP > TowerMaxHP) // Si la reparación pone la vida por encima de la vida máxima, se iguala con la vida máxima
                TowerCurrentHP = TowerMaxHP;

            if (TowerCurrentHP > (TowerMaxHP * sprites[sprites.Length - 1].life) && _towerSprite.sprite != _healthyTower)
                _towerSprite.sprite = _healthyTower;
            else
                this.changeSprite();
        }
    }

    public void restoreMana(float percentage)
    {
        double restoredMana = TowerMaxMana * percentage;
        if (TowerCurrentMana < TowerMaxMana) // Solo se repara si la vida actual es menor que la vida máxima
        {
            TowerCurrentMana += restoredMana;

            if (TowerCurrentMana > TowerMaxMana) // Si la reparación pone la vida por encima de la vida máxima, se iguala con la vida máxima
                TowerCurrentMana = TowerMaxMana;
        }
    }

    void OnRoundChange()
    {
        TowerMaxHP += _hpPerLevel;
        TowerMaxMana += _manaPerLevel;
    }

    void changeSprite()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (TowerCurrentHP <= (TowerMaxHP * sprites[i].life))
            {
                _towerSprite.sprite = sprites[i].m_sprite;
                break;
            }
        }
    }

    public void registerMouse()
    {

        if (_towerCooldownTimer >= _towerCooldown)
        {
            _animatorNigromante.SetBool("attacking", true);
            Vector3 _directionVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _nigromante.position;

            GameObject newProjectile = Instantiate(projectile, _nigromante.position, Quaternion.identity) as GameObject;
            newProjectile.GetComponent<TowerProjectileManager>().initParameters(_towerDamage, _directionVector, this.gameObject.tag, _waypointInicio);
            _towerCooldownTimer = 0.0f;
            StartCoroutine(stopAnimationNigromante());
        }
        
    }

    void animationRehen()
    {
        _animationRehenDone = true;
        _animatorRehen.SetBool("sos", true);
        StartCoroutine(stopAnimationNigromante());
    }

    IEnumerator stopAnimationNigromante()
    {
        yield return new WaitForSeconds(0.5f);
        _animatorNigromante.SetBool("attacking", false);
    }

    IEnumerator stopAnimationRehem()
    {
        yield return new WaitForSeconds(0.5f);
        _animatorRehen.SetBool("sos", false);
        _animationRehenDone = false;
    }

    

}
