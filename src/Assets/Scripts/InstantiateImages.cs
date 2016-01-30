using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstantiateImages : MonoBehaviour {

    public Sprite image;
	void fin()
    {
        GameObject vacio = new GameObject();
        var rend = vacio.AddComponent<Image>();
        vacio.AddComponent<RectTransform>();
        rend.sprite = image;
        vacio.transform.SetParent(this.transform);
        vacio.transform.parent = this.transform;

        //GameObject hijo = Instantiate(image) as GameObject;
        //hijo.transform.parent = this.transform;
        //Debug.Log("aqui");
    }
}
