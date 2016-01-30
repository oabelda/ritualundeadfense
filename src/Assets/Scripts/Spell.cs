using UnityEngine;

public class Spell : MonoBehaviour {
	// Variables publicas
    public SpellRune.Rune[] runes;

    public int goldCost = 5;
    public int manaCost = 10;

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
