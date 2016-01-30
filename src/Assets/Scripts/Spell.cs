using UnityEngine;

public class Spell : MonoBehaviour
{
    // Variables publicas
    public SpellRune.Rune[] runes;
    public int spellID = 0;
    public int goldCost = 5;
    public int manaCost = 10;
    public GameObject minionPrefab;
    public float _Yoffset;
    // Variables privadas
    //float duration = 5;

    // Metodos Awake, Start, Update....

    // Use this for spawn this instance
    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        castSpell();

    }
    // Otros métodos publicos
    void castSpell()
    {
        Debug.Log(gameObject.name);
        switch (gameObject.name.Replace("(Clone)",""))
        {
            case "Esqueleto-Level1":
            case "Esqueleto-Level2":
                if (Input.GetMouseButtonDown(1))
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, -1, 0), Screen.height);
                    if (hit.collider != null)
                    {
                        if (!hit.collider.gameObject.CompareTag("Ally") && !hit.collider.gameObject.CompareTag("Enemy") && !hit.collider.gameObject.CompareTag("Torre"))
                        {
                            GameObject allyInst = GameObject.Instantiate(minionPrefab, new Vector2(hit.point.x, hit.point.y + _Yoffset), minionPrefab.transform.rotation) as GameObject;
                            Destroy(this.gameObject);
                        }
                    }
                }
                break;
        }
    }


    // Otros metodos privados
}