using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
    private GameObject playerObject;

	// Use this for initialization
	void Start () {
        SetFocus();

        transform.position = new Vector3(playerObject.transform.position.x, transform.position.y, playerObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        if (playerObject == null)
        {
            SetFocus();
        }
        transform.position = new Vector3(playerObject.transform.position.x, transform.position.y, playerObject.transform.position.z);
    }

    public void SetFocus()
    {
        Player player = GameController.control.FindPlayer(GameController.control.playerName);
        playerObject = GameObject.Find(GameController.control.playerName);
        playerObject.AddComponent<MovePlayer>().speed = 7 + 0.18f * player.playerHue["Green"];
    }
}
