using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController control;
    
    public List<Player> playerList;
    public List<Player> destroyedPlayers;
    public string playerName;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        playerList = new List<Player>();
        destroyedPlayers = new List<Player>();
        playerName = "Player 0";
    }

    void Update()
    {
        if (destroyedPlayers.Count > 0)
        {
            Debug.Log("Respawning Player");
            SelectRandomSpawnPoint(destroyedPlayers[0]);
            destroyedPlayers.Remove(destroyedPlayers[0]);
        }
    }

    private void SelectRandomSpawnPoint(Player p)
    {
        //randomly select a spawnpoint for respawn
        SpawnPoint sp;
        int index;

        while (0 <= (index = (int)Random.Range(0, 8)))
        {
            Debug.Log("Checking SpawnPoint " + index);
            sp = GameObject.Find("SpawnPoints").transform.GetChild(index).GetComponent<SpawnPoint>();
            if (!sp.IsTriggered())
            {
                sp.SpawnPlayerObject(p);
                break;
            }
        }
    }

    public Player FindPlayer(string gTag)
    {
        foreach(Player p in playerList)
        {
            if (p.GetGTag() == gTag)
            {
                return p;
            }
        }
        return null;
    }
}
