using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {
    public GameObject player;

    private bool isTriggered;
    private List<GameObject> players;

	// Use this for initialization
	void Start () {
        if (GameController.control.playerList.Count > transform.GetSiblingIndex()) {
            Player p = GameController.control.playerList[transform.GetSiblingIndex()];

            SpawnPlayerObject(p);
        }

        players = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        isTriggered = true;
        players.Add(col.gameObject);
        Debug.Log(transform.GetSiblingIndex() + ": " + col.name + " ENTERED: " + isTriggered);
    }
    void OnTriggerStay(Collider col)
    {
        isTriggered = true;
    }
    void OnTriggerExit(Collider col)
    {
        isTriggered = false;
        players.Remove(col.gameObject);
        Debug.Log(transform.GetSiblingIndex() + ": " + col.name + " EXITED: " + isTriggered);
    }

    public void SpawnPlayerObject(Player p)
    {
        p.playerObject = Instantiate(player);

        p.playerObject.name = p.GetGTag();
        p.playerObject.transform.localScale = new Vector3(1, 1, 1);
        p.playerObject.transform.localPosition = transform.localPosition;

        p.SetObjectColor();
    }

    public bool IsTriggered()
    {
        return (players.Count > 0);
    }
    public void RemovePlayer(Player p)
    {
        players.Remove(p.playerObject.gameObject);
    }

    public bool CheckPlayersTriggering(Player p)
    {
        return players.Contains(p.playerObject);
    }
}
