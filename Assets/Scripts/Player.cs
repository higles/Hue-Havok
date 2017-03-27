using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
    private Renderer playerRenderer;
    public GameObject playerObject;
    
    public Dictionary<string, float> playerHue;
    private string gTag;

    public Player(Vector3 hue): this("NoName", hue)
    {
    }

    public Player(string n, Vector3 hue)
    {
        playerHue = new Dictionary<string, float>();
        playerHue.Add("Red", hue.x);
        playerHue.Add("Green", hue.y);
        playerHue.Add("Blue", hue.z);

        gTag = n;

    }

    public string GetGTag()
    {
        return gTag;
    }

    public void SetObjectColor()
    {
        playerRenderer = playerObject.GetComponent<Renderer>();

        //Set player color
        float r = 55 + playerHue["Red"] * 10;
        float g = 55 + playerHue["Green"] * 10;
        float b = 55 + playerHue["Blue"] * 10;
        playerRenderer.material.color = new Color(r / 255, g / 255, b / 255, 1);
    }
}
