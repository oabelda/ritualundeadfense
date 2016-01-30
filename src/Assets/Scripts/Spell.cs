using UnityEngine;

public class Spell : MonoBehaviour {
	// Variables publicas
    public SpellRune.Rune[] runes;

	// Variables privadas
    float duration = 5;

	// Metodos Awake, Start, Update....

	// Use this for spawn this instance
	void Awake(){
	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
        if (duration <= 0)
            Destroy(gameObject);
	}

	// Otros métodos publicos

	// Otros metodos privados
}
