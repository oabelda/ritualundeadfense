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

    [SerializeField]
    private List<Image> _fila1;
    [SerializeField]
    private List<Image> _fila2;
    [SerializeField]
    private List<Image> _fila3;


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
                if (_gold >= _spellList[i]._spells[0].GetComponent<Spell>().goldCost && _spellList[i].level < _spellList[i]._spells.Count)
                {
                    if (_spellList[i]._spells[0] != null)
                    {
                        if(_spellList[i].level+1 <= _spellList[i]._spells.Count)
                        SpellCircle.LearnSpell(_spellList[i]._spells[_spellList[i].level++]);
                        UpdateSpellIndication(i);
                    }
                    else
                        Debug.LogError("Error fatal");
                    removeGold(_spellList[i]._spells[0].GetComponent<Spell>().goldCost);
                }
            }

        }
    }

    public void RestoreMana()
    {
        if (_gold >= _goldMana)
        {
            GameObject.FindGameObjectWithTag("Torre").SendMessage("restoreMana", _manaRestored, SendMessageOptions.RequireReceiver);
            removeGold(_goldMana);
        }

    }

    public void RestoreLife()
    {
        if (_gold >= _goldRepair)
        {
            GameObject.FindGameObjectWithTag("Torre").SendMessage("repairTower", _lifeRestored, SendMessageOptions.RequireReceiver);
            removeGold(_goldRepair);
        }
    }

    //Actualiza el texto del dinero
    void removeGold(int amount)
    {
        _gold -= amount;
        _goldText.text = _gold.ToString();
    }

    void UpdateSpellIndication(int fila)
    {
        switch(fila)
        {
            case 0:
                if (_spellList[fila].level == 1)
                    _fila1[0].color = new Color(_fila1[0].color.r, _fila1[0].color.g, _fila1[0].color.b, 1);
                _fila1[1].color = new Color(_fila1[1].color.r, _fila1[1].color.g, _fila1[1].color.b, 1);
                if (_spellList[fila].level > 1 && _spellList[fila].level <= _spellList[fila]._spells.Count)
                    _fila1[_spellList[fila].level].color = new Color(_fila1[_spellList[fila].level].color.r, _fila1[_spellList[fila].level].color.g, _fila1[_spellList[fila].level].color.b, 1);
                break;
            case 1:
                if (_spellList[fila].level == 1)
                    _fila2[0].color = new Color(_fila2[0].color.r, _fila2[0].color.g, _fila2[0].color.b, 1);
                _fila2[1].color = new Color(_fila2[1].color.r, _fila2[1].color.g, _fila2[1].color.b, 1);
                if (_spellList[fila].level > 1 && _spellList[fila].level <= _spellList[fila]._spells.Count)
                    _fila2[_spellList[fila].level].color = new Color(_fila2[_spellList[fila].level].color.r, _fila2[_spellList[fila].level].color.g, _fila2[_spellList[fila].level].color.b, 1);
                break;
            case 2:
                if (_spellList[fila].level == 1)
                    _fila3[0].color = new Color(_fila3[0].color.r, _fila3[0].color.g, _fila3[0].color.b, 1);
                _fila3[1].color = new Color(_fila3[1].color.r, _fila3[1].color.g, _fila3[1].color.b, 1);
                if (_spellList[fila].level > 1 && _spellList[fila].level <= _spellList[fila]._spells.Count)
                    _fila3[_spellList[fila].level].color = new Color(_fila3[_spellList[fila].level].color.r, _fila3[_spellList[fila].level].color.g, _fila3[_spellList[fila].level].color.b, 1);
                break;
        }
    }
}
