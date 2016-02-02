using UnityEngine;
using System.Collections;

public class AudioElement : MonoBehaviour {
	// Variables publicas

        public bool CheckActivity { get; set; }
	
	// Update is called once per frame
	void Update () {
	    if(CheckActivity)
        {
            if(!this.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                Destroy(this.gameObject);
            }
        }
	}

	// Otros métodos publicos

	// Otros metodos privados
}
