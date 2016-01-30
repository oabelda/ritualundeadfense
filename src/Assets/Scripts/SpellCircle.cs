using UnityEngine;
using System.Collections.Generic;

public class SpellCircle : MonoBehaviour {
    // Constantes públicas
    public const float RUNE_ALPHA_ACTIVE = 1f;
    public const float RUNE_ALPHA_UNACTIVE = 0.5f;

    // Variables publicas

    // Variables privadas
    static SpellCircle instance = null;
    List<SpellRune.Rune> _spell = new List<SpellRune.Rune>();

    // Metodos Awake, Start, Update....

    // Use this for spawn this instance
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.BroadcastMessage("Available", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            gameObject.BroadcastMessage("Available", false);
            CheckSpell();
            _spell.Clear();
            gameObject.SetActive(false);

        }
    }

    // Otros métodos publicos
    public static void AddRune(SpellRune.Rune rune)
    {
        instance._spell.Add(rune);
    }

    // Otros metodos privados
    void CheckSpell()
    {
        string s = "";
        foreach (SpellRune.Rune rune in _spell)
        {
            s += rune.ToString() + " - ";
        }
        Debug.Log("Spell is: " + s);

    }
}
