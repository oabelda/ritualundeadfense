using UnityEngine;

public class SpellBegin : MonoBehaviour {
    // Variables publicas
    public Spell spellBegin;
    public GameObject spellCircle;
    public GameObject ButtonCirculo;

    // Variables privadas
    private Vector3[] vPosiciones;
    private int Destiny;

	// Metodos Awake, Start, Update....

	// Use this for initialization
	void Start () {
        vPosiciones = new Vector3[spellBegin.runes.Length];
        SpellRune[] spRune = spellCircle.GetComponentsInChildren<SpellRune>();
        SpellCircle.LearnSpell(spellBegin.gameObject);
        for (int i = 0; i < spellBegin.runes.Length; ++i)
        {
            for (int j = 0; j < spRune.Length; j++)
            {
                if (spRune[j].rune == spellBegin.runes[i])
                {
                    vPosiciones[i] = spRune[j].transform.position + new Vector3(0.0f, -50.0f, 0.0f);
                    break;
                }
            }
        }
        Destiny = 1;
	}

    // Otros métodos publicos
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, vPosiciones[Destiny], Time.deltaTime*1.5f);
        if (Vector3.Distance(this.transform.position, vPosiciones[Destiny]) < 10.0f)
        {
            ++Destiny;
            if (Destiny == vPosiciones.Length)
            {
                this.transform.position = vPosiciones[0];
                Destiny = 1;
            }
        }
    }

    void OnDisable()
    {
        ButtonCirculo.SetActive(true);
    }

	// Otros metodos privados
}
