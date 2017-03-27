using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {
    private Rigidbody rb;

    public float speed;


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
    	
	// Update is called once per physics update
	void FixedUpdate ()
    {
        float radialRatio = Mathf.PI / 4 - 1;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float movementHorizontal = horizontal + Mathf.Abs(radialRatio * vertical) * -horizontal;
        float movementVertical = vertical + Mathf.Abs(radialRatio * horizontal) * -vertical;
        
        Vector3 movement = new Vector3(movementHorizontal, 0, movementVertical);
        rb.AddForce(movement * speed);
	}
}
