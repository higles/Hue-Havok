using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveMenuCam : MonoBehaviour {
    private string action;
    private float rotated;
    
    protected Vector3 playVect = new Vector3(1, 1, -0.33f);
    protected Vector3 optionsVect = new Vector3(-0.5f, -1, -0.45f);
    private Vector3 backVector;

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
                if (rotated <= playRotateAmount)
                {   //rotate around ball
                    rotated += Time.deltaTime * rotateSpeed;
                    transform.RotateAround(ball.position, playVect, Time.deltaTime * rotateSpeed);
                }
                else
                {
                    //undo over-rotation
                    transform.RotateAround(ball.position, playVect * -1, rotated - playRotateAmount);

                    //set back rotation settings
                    backVector = playVect * -1;
                    backRotateAmount = playRotateAmount;

                    //reset
                    action = null;
                    rotated = 0;
                }
                break;
            case "Options":
                if (rotated <= optionsRotateAmount)
                {   //rotate around ball
                    rotated += Time.deltaTime * rotateSpeed;
                    transform.RotateAround(ball.position, optionsVect, Time.deltaTime * rotateSpeed);
                }
                else
                {
                    //undo over-rotation
                    transform.RotateAround(ball.position, optionsVect * -1, rotated - optionsRotateAmount);

                    //set back rotation settings
                    backVector = optionsVect * -1;
                    backRotateAmount = optionsRotateAmount;

                    //reset
                    action = null;
                    rotated = 0;
                }
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
                if (rotated <= backRotateAmount)
                {   //rotate around ball
                    rotated += Time.deltaTime * rotateSpeed;
                    transform.RotateAround(ball.position, backVector, Time.deltaTime * rotateSpeed);
                }
                else
                {
                    //undo over-rotation
                    transform.RotateAround(ball.position, backVector * -1, rotated - backRotateAmount);

                    //reset
                    action = null;
                    rotated = 0;
                }
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
}
