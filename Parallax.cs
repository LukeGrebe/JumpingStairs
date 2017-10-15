using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public GameController controller;
	public GameObject prefab;
	public float width;
	public bool alreadyDid;
	public float speed;
	private Camera camera1;
	public float cameraWidth;
	public float cameraPosition;
	public float xLastFrameSprite;
	public float xFirstFrameCamera;
	public Sprite lvl1;
	public Sprite lvl2;
	public Sprite lvl3;
	public static int mult;
	
	// Use this for initialization
	void Start ()
	{
		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer> ();
		width = sprite.bounds.size.x;
		alreadyDid = false;
		camera1 = Camera.main;
		
		cameraPosition = camera1.transform.position.x;
		mult = controller.GetMult();
		if (mult % 3 == 0) {
			sprite.sprite = lvl1;
		} else if (mult % 3 == 1) {
			sprite.sprite = lvl2;
		} else if (mult % 3 == 2) {
			sprite.sprite = lvl3;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		cameraWidth = GetComponent<Renderer>().bounds.size.x;
		transform.Translate (new Vector2 (-speed, 0) * Time.fixedDeltaTime);//speed of background
		if (transform.position.x < 0) {
			if (!alreadyDid) {
				Vector2 v = new Vector2 (transform.position.x + (width * 2) - 0.05f, transform.position.y);
				Object obj = Instantiate (prefab, v, Quaternion.identity);
				obj.name = this.name;//eventually removeable
				alreadyDid = true;
			}
		}
		
		xLastFrameSprite = gameObject.transform.position.x + (width / 2);
		xFirstFrameCamera = cameraPosition - (cameraWidth / 2);
		if (xLastFrameSprite < xFirstFrameCamera) {
			Destroy (gameObject);
		}
		
	}
	
}