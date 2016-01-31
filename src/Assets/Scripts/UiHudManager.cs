using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiHudManager : MonoBehaviour {
    // Variables publicas

    // Variables privadas
    private Torre _torre;
    [SerializeField]
    private Slider _slider_HP;
    [SerializeField]
    private Slider _slider_MP;
    // Metodos Awake, Start, Update...
    void Start()
    {
        _torre = GameObject.FindGameObjectWithTag("Torre").GetComponent<Torre>();
    }

    // Use this for spawn this instance

    // Use this for initialization
    void Update () {
        _slider_HP.value = (float) _torre.TowerCurrentHP / (float) _torre.TowerMaxHP;

        _slider_MP.value = (float)_torre.TowerCurrentMana / (float)_torre.TowerMaxMana;


    }
}
