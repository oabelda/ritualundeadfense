using UnityEngine;
using System.Collections;

public delegate void StandartEvent();

public class GameManager : MonoBehaviour {

    [System.Serializable]
    public struct MinMax
    {
        public int min;
        public int max;
    }

    [System.Serializable]
    public struct enemyspawners
    {
        public GameObject prefab;
        public int minRound;
        [Range(0,100)]
        public int percentage;
    }

	// Variables publicas
    public int EnemiesFirstRound = 20;
    public int IncreasedEnemiesPerRound = 3;
    public MinMax timeBetweenEnemies;
    public float timeBetweenRounds = 4f;
    public Transform spawnPoint;
    public enemyspawners[] enemies;
    public enemyspawners[] boses;

    public static StandartEvent OnRoundChange;

    public GameObject spellCircle;
    public GameObject endInformation;

	// Variables privadas
    static GameManager instance = null;
    static int _round = 0;
    bool _gameOver = false;
    int actualEnemies;
    GameObject activeSpell;

	// Metodos Awake, Start, Update....

    // Use this for spawn this instance
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        actualEnemies = EnemiesFirstRound;
        StartCoroutine(rounds());
	}

	// Otros métodos publicos
    public static int Round { get { return _round; } }

	// Otros metodos privados
    IEnumerator rounds()
    {
        while (!_gameOver)
        {
            // Realizar la oleada
            for (int i = 0; i < actualEnemies; ++i)
            {
                // Instanciar enemigo
                GameObject enemy = null;
                while (enemy == null)
                {
                    int rand = Random.Range(0, 100);
                    for (int j = 0; j < enemies.Length; ++j)
                    {
                        if ((rand -= enemies[j].percentage) <= 0 && enemies[j].minRound <= _round)
                        {
                            enemy = enemies[j].prefab; 
                            break;
                        }
                    }
                }
                GameObject.Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

                // Esperar
                yield return new WaitForSeconds(Random.Range(timeBetweenEnemies.min,timeBetweenEnemies.max +1));
            }
            // Boss
            GameObject boss = null;
            while (boss == null && boses.Length != 0)
            {
                int rand = Random.Range(0, 100);
                for (int j = 0; j < boses.Length; ++j)
                {
                    if ((rand -= boses[j].percentage) <= 0 && boses[j].minRound >= _round)
                    {
                        boss = boses[j].prefab;
                    }
                }
            }
            if (boss!=null)
                GameObject.Instantiate(boss, spawnPoint.position, spawnPoint.rotation);

            // Hacer cambio de oleada
            actualEnemies += IncreasedEnemiesPerRound;
            ++_round;
            if (OnRoundChange != null) OnRoundChange();

            // Esperar
            yield return new WaitForSeconds(timeBetweenRounds);
        }
    }

    //Termina el juego y salen los resultados
    public static void finishGame()
    {
        //Tiene que parpadear el castillo 3 veces cambiando el alpha
        //Luego sale el nuemero de rondas que has conseguido superar
        //Impedimos que salgan mas enemigos
        instance._gameOver = true;
        //Paramos el movimiento o las acciones de los enemigos e impedimos lanzar proyectiles o el uso del SpellCircle
        //Cojo el render del gameObject Torre
        SpriteRenderer towerRender = GameObject.FindGameObjectWithTag("Torre").GetComponent<SpriteRenderer>();
        //Llamo a la corutina donde se realiza el parpadeo de la torre
        instance.StartCoroutine(instance.Fade(towerRender));
        instance.StartCoroutine(instance.Fade2(towerRender));
    }

    IEnumerator Fade(SpriteRenderer towerRender)
    {
        spellCircle.SetActive(false);
        //Hago una animación fluida del parpadeo
        //Se desvanece
        Color color = towerRender.color;
        for (int j = 0; j < 3; ++j)
        {
            for (int i = 0; i < 5; ++i)
            {
                color.a *= 0.65f;
                towerRender.color = color;
                yield return new WaitForSeconds(0.10f);
            }
            //Aparece
            for (int i = 0; i < 5; ++i)
            {
                color.a /= 0.65f;
                towerRender.color = color;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    IEnumerator Fade2(SpriteRenderer towerRender)
    {
        yield return new WaitForSeconds(0.3f);
        //Hago una animación fluida del parpadeo
        //Se desvanece
        Color color = towerRender.color;
        for (int j = 0; j < 3; ++j)
        {
            for (int i = 0; i < 5; ++i)
            {
                color.a *= 0.65f;
                towerRender.color = color;
                yield return new WaitForSeconds(0.10f);
            }
            //Aparece
            for (int i = 0; i < 5; ++i)
            {
                color.a /= 0.65f;
                towerRender.color = color;
                yield return new WaitForSeconds(0.05f);
            }
        }
        color.a = 0;
        towerRender.color = color;

        endInformation.SetActive(true);
        for (int i = 0; i < _round; ++i)
        {
            endInformation.SendMessage("fin", SendMessageOptions.RequireReceiver);
        }
    }

    public void changeActiveSpell(GameObject newSpell)
    {
        if (activeSpell != null)
        {
            Destroy(activeSpell);
            activeSpell = newSpell;
            activeSpell.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        }
        else { 
            activeSpell = newSpell;
            activeSpell.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        }
    }
}
