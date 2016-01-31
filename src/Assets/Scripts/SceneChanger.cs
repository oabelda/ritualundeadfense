using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
	// Variables publicas
    public int idEscena;

	// Variables privadas

	// Metodos Awake, Start, Update....

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(idEscena);
	}
	
	// Otros métodos publicos

	// Otros metodos privados
}
