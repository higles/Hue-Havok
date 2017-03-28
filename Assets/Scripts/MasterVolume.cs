using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnChange ()
    {
        float ratio = (255f - 100f) / 255f;
        float r = (255f / 255f) - (transform.GetComponent<Slider>().value * ratio);
        float g = (100f / 255f) + (transform.GetComponent<Slider>().value * ratio);

        transform.FindChild("Fill Area").FindChild("Fill").GetComponent<Image>().color = new Color(r, g, (100f / 255f));
    }
}
