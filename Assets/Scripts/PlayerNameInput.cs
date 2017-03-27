using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPlayerName()
    {
        string name = GetComponent<InputField>().text;

        if (name != "")
        {
            Debug.Log("Setting Player Name: " + name);
            GameController.control.playerName = name;
        }
    }
}
