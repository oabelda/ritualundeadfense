using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellCircle : MonoBehaviour {
    // Constantes públicas
    public const float RUNE_ALPHA_ACTIVE = 1f;
    public const float RUNE_ALPHA_UNACTIVE = 0.5f;

    // Variables publicas

    // Variables privadas
    static SpellCircle instance = null;
    List<SpellRune.Rune> _spell = new List<SpellRune.Rune>();
    SpellTree _grimorium = new SpellTree();

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
            // Hacemos que las runas empicen a escuchar el evento MouseEnter
            gameObject.BroadcastMessage("Available");
        }
        if (Input.GetMouseButtonUp(0))
        {
            // Notificar a las runas que vuelvan a su estado desactivado
            gameObject.BroadcastMessage("Deactivate");
            
            // Buscar el hechizo
            GameObject spellPrefab = _grimorium.FindSpell(_spell);
            Debug.Log(spellPrefab);
            // Hacer la magia
            if (spellPrefab != null)
                GameObject.Instantiate(spellPrefab);

            // Desactivar el círculo de los conjuros
            _spell.Clear();
            gameObject.SetActive(false);

        }
    }

    // Otros métodos publicos

    /// <summary>
    /// Añade una runa al hechizo actual
    /// </summary>
    /// <param name="rune">Runa a añadir</param>
    public static void AddRune(SpellRune.Rune rune)
    {
        instance._spell.Add(rune);
    }

    /// <summary>
    /// Añade un hechizo al grimorio
    /// </summary>
    /// <param name="spell">Hechizo a añadir</param>
    public static void LearnSpell (GameObject spell){
        instance._grimorium.AddSpell(spell);
    }

    /// <summary>
    /// Añade un hechizo al grimorio
    /// </summary>
    /// <param name="spell">Hechizo a añadir</param>
    public void learnSpell(GameObject spell)
    {
        _grimorium.AddSpell(spell);
    }

    // Otros metodos privados
}

public class SpellTree
{
    class Nodo
    {
        public SpellRune.Rune rune;
        public GameObject spellPrefab = null;
        public Dictionary<SpellRune.Rune,Nodo> nextRunes = new Dictionary<SpellRune.Rune,Nodo>();
    }

    Nodo raiz = new Nodo();

    public void AddSpell(GameObject spellPrefab)
    {
        Debug.Log(spellPrefab);
        var runes = spellPrefab.GetComponent<Spell>().runes;

        Nodo actualNode = raiz;
        foreach (SpellRune.Rune rune in runes)
        {
            Debug.Log(rune);
            Nodo nextNode;
            if (actualNode.nextRunes.TryGetValue(rune,out nextNode))
            {
                actualNode = nextNode;
            }
            else
            {
                Nodo newNode = new Nodo();
                newNode.rune = rune;
                actualNode.nextRunes.Add(rune, newNode);
                actualNode = actualNode.nextRunes[rune];
            }
        }
        actualNode.spellPrefab = spellPrefab;
        Debug.Log(actualNode.spellPrefab);
    }

    public GameObject FindSpell(List<SpellRune.Rune> spell)
    {
        Nodo actualNode = raiz;
        foreach (SpellRune.Rune rune in spell)
        {
            Nodo nextNode;
            if (actualNode.nextRunes.TryGetValue(rune, out nextNode))
            {
                actualNode = nextNode;
            }
            else
            {
                return null;
            }
        }
        return actualNode.spellPrefab;
    }
}
