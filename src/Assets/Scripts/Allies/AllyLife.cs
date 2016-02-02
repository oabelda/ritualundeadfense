using UnityEngine;
using System.Collections;

public class AllyLife : MonoBehaviour
{

    private float maxLife;
    private Ally parent;

    // Use this for initialization
    void Start()
    {
        parent = transform.parent.GetComponent<Ally>();
        maxLife = parent.Life;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(parent._life / maxLife, transform.localScale.y);
    }
}
