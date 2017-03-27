using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttributePoints : MonoBehaviour {
    public GameObject point;
    public Material RedPoint;
    public Material GreenPoint;
    public Material BluePoint;
    public Material WhitePoint;

    public Transform Player;

	// Use this for initialization
	void Start () {
        //setup points
	    for (int i=0; i < 20; i++)
        {
            GameObject p = Instantiate(point) as GameObject;
            p.name = "Point";

            p.transform.parent = transform.FindChild("Unassigned").gameObject.transform;
            p.transform.localPosition = new Vector3(p.transform.parent.localPosition.x + 10*i, p.transform.parent.localPosition.y, p.transform.parent.localPosition.z);
            p.transform.rotation = p.transform.parent.rotation;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddPoint (string hue)
    {
        Transform unassigned = transform.FindChild("Unassigned");
        
        //Check if there are any points left unassigned and assign them to the designated hue
        if (unassigned.childCount > 0)
        {
            Transform chosenHue = transform.FindChild(hue).FindChild("Points");
            int index = chosenHue.childCount;

            //Re-assign point from unassigned to chosenHue
            GameObject p = unassigned.GetChild(unassigned.childCount - 1).gameObject;
            p.transform.parent = chosenHue.transform;

            //Re-position the point to it's newly allocated location
            p.transform.localPosition = new Vector3(chosenHue.localPosition.x + 10 * index, chosenHue.localPosition.y, chosenHue.localPosition.z);
            p.transform.rotation = chosenHue.rotation;

            //Change the material on the point to match it's new designation
            ColorPoint(p, hue);

            //Adjust color of Player ball
            UpdatePlayerColor(hue, 0.039f);
        }
    }

    public void SubtractPoint (string hue)
    {
        Transform chosenHue = transform.FindChild(hue).FindChild("Points");

        //Check if there are any points left in the chosen hue and change them to unassigned
        if (chosenHue.childCount > 0)
        {
            Transform unassigned = transform.FindChild("Unassigned");
            int index = unassigned.childCount;

            //Re-assign point from chosenHue to unassigned
            GameObject p = chosenHue.GetChild(chosenHue.childCount - 1).gameObject;
            p.transform.parent = unassigned.transform;

            //Re-position the point to it's newly allocated location
            p.transform.localPosition = new Vector3(unassigned.localPosition.x + 10 * index, unassigned.localPosition.y, unassigned.localPosition.z);
            p.transform.rotation = unassigned.rotation;

            //Change the material on the point to match it's new designation
            ColorPoint(p, "white");

            //Adjust color of Player ball
            UpdatePlayerColor(hue, -0.039f);
        }
    }

    private void ColorPoint (GameObject p, string hue)
    {
        switch (hue)
        {
            case "Red":
                p.GetComponent<Renderer>().material = RedPoint;
                break;
            case "Green":
                p.GetComponent<Renderer>().material = GreenPoint;
                break;
            case "Blue":
                p.GetComponent<Renderer>().material = BluePoint;
                break;
            default:
                p.GetComponent<Renderer>().material = WhitePoint;
                break;
        }
    }

    private void UpdatePlayerColor(string hue, float value)
    {
        Material playerMat = Player.GetComponent<Renderer>().material;
        switch (hue)
        {
            case "Red":
                playerMat.color = new Color(playerMat.color.r + value, playerMat.color.g, playerMat.color.b, playerMat.color.a);
                break;
            case "Green":
                playerMat.color = new Color(playerMat.color.r, playerMat.color.g + value, playerMat.color.b, playerMat.color.a);
                break;
            case "Blue":
                playerMat.color = new Color(playerMat.color.r, playerMat.color.g, playerMat.color.b + value, playerMat.color.a);
                break;
        }
    }
}
