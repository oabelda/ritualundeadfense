using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public Animator _shopAnimator;
    private bool _shopOpened = false;

    int _gold;

    static ShopManager instance = null;

    // Use this for spawn this instance
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
    }

}
