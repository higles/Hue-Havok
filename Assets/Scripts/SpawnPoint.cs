using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
    public GameObject player;
    private bool isTriggered;

	// Use this for initialization
	void Start () {
        if (GameController.control.playerList.Count > transform.GetSiblingIndex()) {
            Player p = GameController.control.playerList[transform.GetSiblingIndex()];

            SpawnPlayerObject(p);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(transform.GetSiblingIndex() + ": " + col.name + " ENTERED");
        isTriggered = true;
    }
    void OnTriggerStay(Collider col)
    {
        isTriggered = true;
    }
    void OnTriggerExit(Collider col)
    {
        Debug.Log(transform.GetSiblingIndex() + ": " + col.name + " EXITED");
        isTriggered = false;
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
        return isTriggered;
    }
}
