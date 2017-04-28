using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
    public float defenseBounciness;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter (Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                //get the corresponding players
                Player player = GameController.control.FindPlayer(gameObject.name);
                Player colPlayer = GameController.control.FindPlayer(col.gameObject.name);

                //calculate loser
                Player loser = CalculateLoser(player, colPlayer);

                //apply bounce force from enemy's blue attribute
                player.playerObject.GetComponent<Rigidbody>().AddExplosionForce(
                    defenseBounciness * colPlayer.playerHue["Blue"], colPlayer.playerObject.transform.position, defenseBounciness * colPlayer.playerHue["Blue"]
                );

                if (loser != null)
                {   //destroy loser 
                    GameController.control.destroyedPlayers.Add(loser);
                }
                break;
            default:
                break;
        }
    }

    

    private Player CalculateLoser(Player p1, Player p2)
    {
        //RPS combat style
        //float p1Attk = CalculateAttack(p1, p2);
        //float p2Attk = CalculateAttack(p2, p1);
        //Debug.Log(p1Attk + " : " + p2Attk);
        //if (p1Attk < p2Attk) return p1;
        //else if (p1Attk > p2Attk) return p2;
        //else return null;

        //RPG combat style
        float p1Attk = CalculateAttack(p1, p2);
        if (p1Attk > 0)
        {
            return p2;
        }
        else return null;
    }
    private float CalculateAttack(Player attacker, Player defender)
    {
        //RPS combat style
        //float rRatio = attacker.playerHue["Red"] / 20;
        //float gRatio = attacker.playerHue["Green"] / 20;
        //float bRatio = attacker.playerHue["Blue"] / 20;

        //float rScore = rRatio * (attacker.playerHue["Red"] + defender.playerHue["Green"] - defender.playerHue["Blue"]);
        //float gScore = gRatio * (attacker.playerHue["Green"] + defender.playerHue["Blue"] - defender.playerHue["Red"]);
        //float bScore = bRatio * (attacker.playerHue["Blue"] + defender.playerHue["Red"] - defender.playerHue["Green"]);

        //return (rScore + gScore + bScore);

        //RPG combat style
        float attk = attacker.playerHue["Red"] - defender.playerHue["Blue"];
        return attk;
    }
}
