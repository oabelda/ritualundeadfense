using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //public Animator _shopAnimator;
    public Text _goldText;
    private bool _shopOpened = false;

    [Range(0.0f, 1.0f)]
    public float _lifeRestored = 0.05f;
    [Range(0.0f, 1.0f)]
    public float _manaRestored = 0.05f;

    public List<SpellsList> _spellList;

    public int _gold = 100;
    public int _goldMana = 5;
    public int _goldRepair = 5;

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

    //public void shopButton()
    //{
    //    if (_shopOpened)
    //    {
    //        _shopAnimator.SetTrigger("Ocultar");
    //        _shopOpened = false;
    //    }
    //    else
    //    {
    //        _shopAnimator.SetTrigger("Mostrar");
    //        _shopOpened = true;
    //    }
    //}

    public static void addGold(int amount)
    {
        instance._gold += amount;
        instance._goldText.text = instance._gold.ToString();
    }

    public void LearnSpell(string spell)
    {
        for (int i = 0; i < _spellList.Count; ++i)
        {
            if (_spellList[i].name.Contains(spell))
            {
                if (_gold >= _spellList[i]._spells[0].GetComponent<Spell>().goldCost)
                {
                    if (_spellList[i]._spells[0] != null)
                        SpellCircle.LearnSpell(_spellList[i]._spells[_spellList[i].level++]);
                    else
                        Debug.LogError("Error fatal");
                    removeGold(_spellList[i]._spells[0].GetComponent<Spell>().goldCost);
                }
            }

        }
    }

    public void RestoreMana()
    {
        GameObject.FindGameObjectWithTag("Torre").SendMessage("restoreMana", _manaRestored, SendMessageOptions.RequireReceiver);
        removeGold(_goldMana);

    }

    public void RestoreLife()
    {
        GameObject.FindGameObjectWithTag("Torre").SendMessage("repairTower", _lifeRestored,SendMessageOptions.RequireReceiver);
        removeGold(_goldRepair);
    }

    //Actualiza el texto del dinero
    void removeGold(int amount)
    {
        _gold -= amount;
        _goldText.text = _gold.ToString();
    }
}
