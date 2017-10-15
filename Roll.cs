using UnityEngine;
using System.Collections;

public class Roll : MonoBehaviour {
	
	private Rigidbody2D rigidbody1;
	public int force = 2;
	public int lifetime = 4;

	// Use this for initialization
	void Start () {
		rigidbody1 = gameObject.GetComponent<Rigidbody2D>();
		//rock is gone after 4 seconds
		Destroy(gameObject, lifetime);
		//rigidbody1.velocity = rigidbody1.velocity + (Vector2.left * force);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//rigidbody1.AddForce (Vector3.left * force);
		rigidbody1.velocity = new Vector2(-1*force, rigidbody1.velocity.y);
	}
}