using UnityEngine;
using System.Collections;

public class AnimateButton : MonoBehaviour {
    public float dist;
    public float speed;
    public float offset;
    public float flashSpeed;

    public MoveMenuCam camScript;

    private Vector3 targetPos;
    private Vector3 targetScale;
    private bool expanding;
    private bool clicked;
    private float t;

    private TextMesh buttonTextMesh;
    private Renderer rend;
    private Color targetTextColor;
    private Color fadedTextColor;
    private Color targetCenterColor;
    private Color fadedCenterColor;
    int flash;

    // Use this for initialization
    void Start () {
        expanding = true;
        clicked = false;
        flash = 0;
        t = 0;
        
        //Get and Set transform starting and target positions
        targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = new Vector3(transform.localPosition.x - 125f, transform.localPosition.y, transform.localPosition.z);

        //Get and Set transform starting and target scale
        targetScale = transform.localScale;
        transform.localScale = new Vector3(0, 1.25f, 1);

        //Set button text opacity to invisible
        buttonTextMesh = transform.FindChild("Text").GetComponent<TextMesh>();
        targetTextColor = buttonTextMesh.color;
        buttonTextMesh.color = new Color(buttonTextMesh.color.r, buttonTextMesh.color.g, buttonTextMesh.color.b, 0);

        //Get flash colors
        rend = transform.FindChild("Center").GetComponent<Renderer>();
        targetCenterColor = rend.material.color;
        fadedCenterColor = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, rend.material.color.a - 0.5f);

        fadedTextColor = new Color(targetTextColor.r, targetTextColor.g, targetTextColor.b, targetTextColor.a - 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        if (expanding)
        {   //Expand the buttons
            t += Time.deltaTime;

            if (t > offset)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, (t - offset) / speed);
                transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, (t - offset) / speed / 100f);
            }
        }

        if (transform.localPosition == targetPos && transform.localScale == targetScale)
        {   //Switch expanding flag when expanding is done.
            expanding = false;
        }

        if (!expanding)
        {
            buttonTextMesh.color = Color.Lerp(buttonTextMesh.color, targetTextColor, Time.deltaTime);
        }

        //Flash when clicked
        if (clicked)
        {
            FlashSelect();
        }
    }

    public void PointerEnter()
    {
        if (!expanding)
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - dist);
    }

    public void PointerExit()
    {
        if (!expanding && transform.localPosition != targetPos)
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + dist);
    }

    public void Clicked()
    {
        if (!expanding)
        {
            clicked = true;
        }
    }
    private void FlashSelect()
    {
        if (flash <= 5)
        {
            if (flash % 2 == 0)
            {
                rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, rend.material.color.a - flashSpeed);
                buttonTextMesh.color = new Color(buttonTextMesh.color.r, buttonTextMesh.color.g, buttonTextMesh.color.b, buttonTextMesh.color.a - flashSpeed);

                if (buttonTextMesh.color.a <= fadedTextColor.a)
                {
                    rend.material.color = fadedCenterColor;
                    buttonTextMesh.color = fadedTextColor;
                    flash++;
                }
            }

            else
            {
                rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, rend.material.color.a + flashSpeed);
                buttonTextMesh.color = new Color(buttonTextMesh.color.r, buttonTextMesh.color.g, buttonTextMesh.color.b, buttonTextMesh.color.a + flashSpeed);

                if (buttonTextMesh.color.a >= targetTextColor.a)
                {
                    rend.material.color = targetCenterColor;
                    buttonTextMesh.color = targetTextColor;
                    flash++;
                }
            }
        }

        if (flash > 5)
        {
            flash = 0;
            clicked = false;

            camScript.PerformClickedAction(buttonTextMesh.text);
        }
    }
}
