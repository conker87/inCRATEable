using UnityEngine;
using System.Collections;

public class JumpOnTap : MonoBehaviour {

	public 	Rigidbody2D body;

	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetMouseButtonDown (0)) {
			body.AddForce (new Vector2 (0, 7.5f), ForceMode2D.Impulse);
		}
	}

}
