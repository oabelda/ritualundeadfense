using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpellRune : MonoBehaviour {
    // Enumerado publico
    public enum Rune
    {
        Rune1,Rune2,Rune3,Rune4,Rune5
    }

    // Variables publicas
    public Rune rune;

    // Variables privadas
    Image m_sprite;
    bool avialable = false;
    bool send = false;

    // Metodos Awake, Start, Update....

    // Use this for spawn this instance
    void Awake()
    {
        m_sprite = GetComponent<Image>();
    }

    // Otros métodos publicos

    public void Available(bool available)
    {
        this.avialable = available;
        if (!available)
            Deactivate();
    }

    public void Activate()
    {
        if (avialable && !send)
        {
            Color c = m_sprite.color;
            c.a = SpellCircle.RUNE_ALPHA_ACTIVE;
            m_sprite.color = c;

            SpellCircle.AddRune(rune);

            send = true;
        }
    }

    public void Deactivate()
    {
        Color c = m_sprite.color;
        c.a = SpellCircle.RUNE_ALPHA_UNACTIVE;
        m_sprite.color = c;

        avialable = false;
        send = false;
    }

    // Otros metodos privados
}
