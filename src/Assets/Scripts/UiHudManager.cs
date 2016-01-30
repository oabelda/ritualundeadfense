using UnityEngine;
using System.Collections;

public class UiHudManager : MonoBehaviour {
    // Variables publicas

    // Variables privadas
    [SerializeField]
    private GameObject _shopHud;
    [SerializeField]
    private GameObject _torre;
	// Metodos Awake, Start, Update....

	// Use this for spawn this instance

	// Use this for initialization
	void Start () {
	}

	// Otros métodos publicos
    public void ToogleShop()
    {
        _shopHud.SetActive(!_shopHud.activeInHierarchy);
    }
	// Otros metodos privados
}
