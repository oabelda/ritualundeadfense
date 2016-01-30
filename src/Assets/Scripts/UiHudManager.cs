using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiHudManager : MonoBehaviour {
    // Variables publicas

    // Variables privadas
    [SerializeField]
    private GameObject _shopHud;
    [SerializeField]
    private Torre _torre;
    [SerializeField]
    private Slider _slider_HP;
    [SerializeField]
    private Slider _slider_MP;
    // Metodos Awake, Start, Update....

    // Use this for spawn this instance

    // Use this for initialization
    void Update () {
        _slider_HP.value = (float) _torre.TowerCurrentHP / (float) _torre.TowerMaxHP;

        _slider_MP.value = (float)_torre.TowerCurrentMana / (float)_torre.TowerMaxMana;

        UpdateMoneyOnHUD();

    }

	// Otros métodos publicos
    public void ToogleShop()
    {
        _shopHud.SetActive(!_shopHud.activeInHierarchy);
    }
	// Otros metodos privados
    private void UpdateMoneyOnHUD()
    {

    }
}
