using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Checkbox : MonoBehaviour {
    private Color red = new Color(1, 0.706f, 0.706f);
    private Color green = new Color(0.706f, 1, 0.706f);

    private Image bgImage;
    private Image checkImage;

	// Use this for initialization
	void Start () {
        bgImage = transform.FindChild("Background").GetComponent<Image>();
        checkImage = transform.FindChild("Background").FindChild("Checkmark").GetComponent<Image>();

        OnChange();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnChange ()
    {
        if (transform.GetComponent<Toggle>().isOn)
        {
            bgImage.color = green;
            checkImage.color = green;
        }
        else
        {
            bgImage.color = red;
            checkImage.color = red;
        }
    }
}
