using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Animator _shopAnimator;
    public Text _goldText;
    private bool _shopOpened = false;

    [Range(0.0f, 1.0f)]
    public float _lifeRestored = 0.05f;
    [Range(0.0f, 1.0f)]
    public float _manaRestored = 0.05f;

    public List<GameObject> _spells = new List<GameObject>();

    public int _gold = 100;

    static ShopManager instance = null;

    // Use this for spawn this instance
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        _goldText.text = _gold.ToString();
    }

    public void shopButton()
    {
        if (_shopOpened)
        {
            _shopAnimator.SetTrigger("Ocultar");
            _shopOpened = false;
        }
        else
        {
            _shopAnimator.SetTrigger("Mostrar");
            _shopOpened = true;
        }
    }

    public static void addGold(int amount)
    {
        instance._gold += amount;
        instance._goldText.text = instance._gold.ToString();
    }

    public void LearnSpell(GameObject spell)
    {
        if (_gold >= spell.GetComponent<Spell>().goldCost)
        {
            for (int i = 0; i < _spells.Count; ++i )
            {
                if (_spells[i].name == spell.name)
                {
                    SpellCircle.LearnSpell(spell);
                    _gold -= spell.GetComponent<Spell>().goldCost;
                    _goldText.text = _gold.ToString();
                    _spells.Remove(_spells[i]);
                }
            }
            
        }
    }

    public void RestoreMana()
    {
        GameObject.FindGameObjectWithTag("Torre").SendMessage("restoreMana", _manaRestored, SendMessageOptions.RequireReceiver);
    }

    public void RestoreLife()
    {
        GameObject.FindGameObjectWithTag("Torre").SendMessage("repairTower", _lifeRestored,SendMessageOptions.RequireReceiver);
    }


}
