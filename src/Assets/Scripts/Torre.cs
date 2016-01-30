using UnityEngine;
using System.Collections;




public class Torre : MonoBehaviour {
    [Header("Tower Properties")]
    [SerializeField]
    double _towerMaxHP;

    [System.Serializable]
    public class spritesTower
    {
        public Sprite m_sprite;
        public double life;
    }

    public spritesTower[] sprites;
    
    [SerializeField]
    Sprite _healthyTower;


    double _towerCurrentHP;
    SpriteRenderer _towerSprite;

    // Use this for initialization
    void Start () {
        _towerCurrentHP = _towerMaxHP;
        _towerSprite = GetComponent<SpriteRenderer>();
        if (_towerSprite.sprite == null)
            _towerSprite.sprite = _healthyTower; 
	}

    // Función para registrar el daño que recibe la torre
    public void HitEnemy(double damageReceived)
    {
        _towerCurrentHP -= damageReceived;
        Debug.Log("Damage received, current HP: " + _towerCurrentHP);
        if (_towerCurrentHP > (_towerMaxHP * sprites[sprites.Length - 1].life) && _towerSprite.sprite != _healthyTower)
            _towerSprite.sprite = _healthyTower;
        else
            this.changeSprite();
        
    }


    // Función para registrar las reparaciones que hace el jugador a la torre
    public void repairTower(double repairAmount)
    {
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



}
