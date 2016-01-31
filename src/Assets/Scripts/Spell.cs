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
    private Transform greenFloor; // Para indicar dónde se pueden castear torres
    GameManager _gamemanager;
    // Metodos Awake, Start, Update....
    
    void Start()
    {
        GameObject gmanager = GameObject.FindGameObjectWithTag("GameManager");
        if (gmanager != null)
        {
            _gamemanager = gmanager.GetComponent<GameManager>();
            _gamemanager.changeActiveSpell(this.gameObject);
        }
        GameObject background = GameObject.FindGameObjectWithTag("Background");
        greenFloor = background.transform.GetChild(0);
        greenFloor.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
        castSpell();

    }
    // Otros métodos publicos
    void castSpell()
    {
        switch (gameObject.name.Replace("(Clone)",""))
        {
            case "EspirituLevel1":
            case "EspirituLevel2":
            case "GolemLevel1":
            case "GolemLevel2":
            case "MurcielagoLevel1":
            case "MurcielagoLevel2":
                greenFloor.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, -1, 0), Screen.height);
                    if (hit.collider != null)
                    {
                        if (!hit.collider.gameObject.CompareTag("Ally") && !hit.collider.gameObject.CompareTag("Enemy") && !hit.collider.gameObject.CompareTag("Torre"))
                        {
                            GameObject.Instantiate(minionPrefab, new Vector2(hit.point.x, hit.point.y + _Yoffset), minionPrefab.transform.rotation);
                            Destroy(this.gameObject);
                            greenFloor.gameObject.SetActive(false);
                        }
                    }
                }
                break;
        }
    }


    // Otros metodos privados
}