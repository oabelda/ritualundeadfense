using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour {

    private float maxLife;
    private Enemy parent;

	// Use this for initialization
	void Start () {
        parent = transform.parent.GetComponent<Enemy>();
        maxLife = parent.InitialLife + GameManager.Round * parent.LifeIncrease;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(parent._life / maxLife, transform.localScale.y);
	}
}
