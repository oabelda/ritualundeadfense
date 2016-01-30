using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
	// Variables publicas
    public string nombreEscena;

	// Variables privadas

	// Metodos Awake, Start, Update....

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(nombreEscena);
	}
	
	// Otros métodos publicos

	// Otros metodos privados
}
