using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveMenuCam : MonoBehaviour {
    private string action;
    private float rotated;
    
    protected Vector3 playVect = new Vector3(1, 1, -0.33f);
    protected Vector3 optionsVect = new Vector3(-0.5f, -1, -0.45f);
    private Vector3 backVect;

    public Transform ball;
    public float rotateSpeed;
    public float playRotateAmount;
    public float optionsRotateAmount;
    private float backRotateAmount;

	// Use this for initialization
	void Start () {
        action = "";
        rotated = 0;
	}
   
    // Update is called once per frame
    void Update () {
        switch (action)
        {
            case "Play Game":   //rotate to play menu
                RotateToMenu(playRotateAmount, playVect);
                break;
            case "Options":
                RotateToMenu(optionsRotateAmount, optionsVect);
                Debug.Log(transform.rotation);
                break;
            case "Exit":
                Debug.Log("Exitting game");
                Application.Quit();
                break;
            case "Play":    //switch to map scene and spawn player
                //add new player to GameController
                Vector3 hue = new Vector3(GameObject.Find("Red").transform.FindChild("Points").childCount, GameObject.Find("Green").transform.FindChild("Points").childCount, GameObject.Find("Blue").transform.FindChild("Points").childCount);
                GameController.control.playerList.Add(new Player(GameController.control.playerName, hue));
                //add these players for bump testing
                GameController.control.playerList.Add(new Player("enemy Red", new Vector3(20, 00, 0)));
                GameController.control.playerList.Add(new Player("enemy Yellow", new Vector3(10, 10, 0)));
                GameController.control.playerList.Add(new Player("enemy Green", new Vector3(0, 20, 0)));
                GameController.control.playerList.Add(new Player("enemy Teal", new Vector3(0, 10, 10)));
                GameController.control.playerList.Add(new Player("enemy Blue", new Vector3(0, 0, 20)));
                GameController.control.playerList.Add(new Player("enemy Pink", new Vector3(10, 0, 10)));
                GameController.control.playerList.Add(new Player("enemy Orange", new Vector3(15, 5, 0)));

                //switch to map scene
                SceneManager.LoadScene("The Arena");
                break;
            case "Back":
                RotateToMenu(backRotateAmount, backVect);
        break;
            default:
                break;
        }
    }

    public void PerformClickedAction(string selection)
    {
        Debug.Log(selection);
        action = selection;
    }

    private void RotateToMenu(float rotateAmount, Vector3 rotateVector)
    {
        if (rotated < rotateAmount)
        {   //rotate around ball
            if (rotateAmount < rotated + Time.deltaTime * rotateSpeed)
            {   //finish rotation
                rotated = rotateAmount;
                transform.RotateAround(ball.position, rotateVector, rotateAmount - rotated);
            }
            else
            {   //rotate normally
                rotated += Time.deltaTime * rotateSpeed;
                transform.RotateAround(ball.position, rotateVector, Time.deltaTime * rotateSpeed);
            }
        }
        else
        {
            //undo over-rotation
            transform.RotateAround(ball.position, rotateVector * -1, rotated - rotateAmount);

            //set back rotation settings
            backVect = rotateVector * -1;
            backRotateAmount = rotateAmount;

            //reset
            action = null;
            rotated = 0;
        }
    }
}
