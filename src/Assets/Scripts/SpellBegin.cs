using UnityEngine;

public class SpellBegin : MonoBehaviour {
    // Variables publicas
    public GameObject spellBegin;
    public GameObject Image;
    public GameObject Flecha;
	// Metodos Awake, Start, Update....

	// Use this for initialization
	void Start () {
        SpellCircle.LearnSpell(spellBegin);
	}

    // Otros métodos publicos
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Image.SetActive(false);
            Flecha.SetActive(true);
        }
    }

	// Otros metodos privados
}
