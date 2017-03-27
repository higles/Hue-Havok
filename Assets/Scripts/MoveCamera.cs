using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
    private GameObject player;

	// Use this for initialization
	void Start () {
        SetFocus();

        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            SetFocus();
        }
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }

    public void SetFocus()
    {
        player = GameObject.Find(GameController.control.playerName);
        player.AddComponent<MovePlayer>().speed = 10;
    }
}
