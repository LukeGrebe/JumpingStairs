using UnityEngine;
using System.Collections;

public class LifeCounter : MonoBehaviour {
	
	public GameController controller;
	public GameObject heart;
	public GameObject[] hearts;
	public Quaternion heartRot;
	public Vector3 heartPos;
	public float heartLength = 2.5f;
	private int heartcount=0;
	
	// Use this for initialization
	void Start () {
		HeartUpdate ();	
		hearts = GameObject.FindGameObjectsWithTag("Heart");
	}
	
	// Update is called once per frame
	void Update () {

		if (heartcount > controller.GetLives()||heartcount < controller.GetLives()) {
			HeartUpdate ();
			for(int i = 0; i < hearts.Length; i++)
			{
				Destroy(hearts[i].gameObject);
			}
			hearts = GameObject.FindGameObjectsWithTag("Heart");
		}
	}

	// Updates the lives that appear on screen based on the current amount of lives the player has left
	void HeartUpdate(){
		heartcount = controller.GetLives();
		heartRot = heart.transform.rotation;
		for (int i = 0; i < heartcount; i++) {
			heartPos = new Vector3 (heart.transform.position.x + heartLength * i, heart.transform.position.y, 0.0f);
			Instantiate (heart, heartPos, heartRot);
		}
	}
}