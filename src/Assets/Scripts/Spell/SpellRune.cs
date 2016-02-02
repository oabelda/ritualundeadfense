using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpellRune : MonoBehaviour {
    // Enumerado publico
    public enum Rune
    {
        Fuego,Agua,Viento,Luz,Oscuridad,Tierra,Roca
    }

    // Variables publicas
    public Rune rune;

    public Sprite activeRune;
    public Sprite deactiveRune;

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

    public void Available()
    {
        this.avialable = true;
    }

    public void Activate(bool byClick)
    {
        if ((avialable || byClick) && !send)
        {
            m_sprite.sprite = activeRune;

            SpellCircle.AddRune(rune);

            send = true;
        }
    }

    public void Deactivate()
    {
        m_sprite.sprite = deactiveRune;

        avialable = false;
        send = false;
    }

    // Otros metodos privados
}
