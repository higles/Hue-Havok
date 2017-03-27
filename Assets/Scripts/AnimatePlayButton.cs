using UnityEngine;
using System.Collections;

public class AnimatePlayButton : MonoBehaviour {
    private Color applyTextColor;
    private Color playTextColor;
    private TextMesh buttonTextMesh;
    private Renderer textRenderer;
    private Renderer centerRenderer;
    private Renderer leftRenderer;
    private Renderer rightRenderer;

    private Vector3 fullScale;
    private Vector3 shrunkScale;

    private bool ready;

    private bool shrinking;
    private bool growing;

    public float speed;

    public Material whiteMatOuter;
    public Material whiteMatCenter;
    public Material whiteMatText;
    public Material greenMatOuter;
    public Material greenMatCenter;
    public Material greenMatText;

    //unallocated Points
    public Transform unassigned;

    // Use this for initialization
    void Start() {
        buttonTextMesh = transform.FindChild("Text").GetComponent<TextMesh>();
        textRenderer = transform.FindChild("Text").GetComponent<Renderer>();
        centerRenderer = transform.FindChild("Center").GetComponent<Renderer>();
        leftRenderer = transform.FindChild("Left Brace").GetComponent<Renderer>();
        rightRenderer = transform.FindChild("Right Brace").GetComponent<Renderer>();

        fullScale = new Vector3(1.5f, 1.5f, 1.5f);
        shrunkScale = new Vector3(0.1f, transform.localScale.y, transform.localScale.z);

        applyTextColor = new Color(1, 1, 1, 1);
        playTextColor = new Color(0.7f, 1, 0.7f);

        ready = false;
    }

    // Update is called once per frame
    void Update() {
        //Get ready status
        if (buttonTextMesh.text == "Play")
        {
            ready = true;
        }
        else
        {
            ready = false;
        }

        //If the ammount of unassigned points is no longer empty
        if (unassigned.childCount > 0)
        {
            SwitchButton(whiteMatOuter, whiteMatCenter, whiteMatText, "Apply Points");
        }

        //If the ammount of unassigned points becomes empty
        else if (unassigned.childCount == 0)
        {
            SwitchButton(greenMatOuter, greenMatCenter, greenMatText, "Play");
        }
    }

    // Animate the switching of the button
    private void SwitchButton(Material matOuter, Material matCenter, Material matText, string str)
    {
        //collapse button
        if (transform.localScale != shrunkScale && ((!ready && str == "Play") || (ready && str == "Apply Points")))
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, shrunkScale, Time.deltaTime * speed);
        }
        else
        {
            if ((ready && str == "Apply Points") || (!ready && str == "Play"))
            {   //change materials, colors, and text
                centerRenderer.material = matCenter;
                leftRenderer.material = matOuter;
                rightRenderer.material = matOuter;
                textRenderer.material = matText;
                buttonTextMesh.text = str;
            }
        }
        //expand button
        if (transform.localScale != fullScale && ((!ready && str == "Apply Points") || (ready && str == "Play")))
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, fullScale, Time.deltaTime * speed);
        }
    }
}
