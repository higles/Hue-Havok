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
            UnTriggerSpawnPoint(destroyedPlayers[0]);
            Destroy(destroyedPlayers[0].playerObject);
            SelectRandomSpawnPoint(destroyedPlayers[0]);
            destroyedPlayers.Remove(destroyedPlayers[0]);
        }
    }

    private void UnTriggerSpawnPoint(Player p)
    {
        foreach(SpawnPoint sp in GameObject.Find("SpawnPoints").transform.GetComponentsInChildren<SpawnPoint>())
        {
            if (sp.CheckPlayersTriggering(p))
            {
                sp.RemovePlayer(p);
            }
        }
    }

    private void SelectRandomSpawnPoint(Player p)
    {
        //get list of all available spawnpoints
        List<SpawnPoint> spList = new List<SpawnPoint>();
        foreach (SpawnPoint sp in GameObject.Find("SpawnPoints").transform.GetComponentsInChildren<SpawnPoint>())
        {
            if (!sp.IsTriggered())
            {
                spList.Add(sp);
            }
        }
        if (spList.Count == 0)
        {
            //if no available spawnpoint then don't respawn
            return;
        }

        //randomly select a spawnpoint for respawn
        int index;

        while (0 <= (index = (int)Random.Range(0, spList.Count)))
        {
            Debug.Log("Checking SpawnPoint " + index);
            SpawnPoint sp = spList[index];

            if (!sp.IsTriggered())
            {
                sp.SpawnPlayerObject(p);
                return;
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
