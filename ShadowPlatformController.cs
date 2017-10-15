using UnityEngine;
using System.Collections;

public class ShadowPlatformController : MonoBehaviour {
	
	[HideInInspector] public bool jump = false;
	
	public GameController gamecontroller;

	//public AudioSource collectsound;
	public AudioSource collisionsound;
	public AudioSource jumpsound;
	
	public float jumpForce = 1000f;
	public float blinkSpeed = 0.15f;//2 blinks
	public float blinkDuration = 0.05f;
	public Transform groundCheck;
	
	public float elapsedTime = 0;
	public float maxExtraJump = 500f;
	public float extraPower = 2f;
	public Vector2 jumpSpeed;
	
	private bool canDoubleJump;
	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;
	
	
	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		if (collisionsound != null) {
			collisionsound.pitch = 0.8f;//give the shadow a different sound when hit to tell which dino got hit
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			OnJump ();
		}
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ShadowGround"));
		if (grounded) {
			anim.SetBool("jump", false);
		}
		if (gamecontroller.paused) {
			grounded=false;
		}
		//The score will increment while Mo is on screen, but Mo's Shadow doesn't need score update
	}
	
	public void OnJump(){
		if(grounded)
		{
			jump = true;
			jumpsound.Play ();
		}
		
	}
	
	void FixedUpdate()
	{
//		float h = Input.GetAxis ("Horizontal");
//		
//		anim.SetFloat ("Speed", Mathf.Abs (h));
		
//		if (Input.GetButtonDown ("Jump")) {	
			if (jump) {
				if (elapsedTime > maxExtraJump) {
					elapsedTime = maxExtraJump;
				}
			
				anim.SetBool ("jump", true);
				rb2d.AddForce (new Vector2 (0f, jumpForce + (elapsedTime * extraPower)));
				//			rb2d.AddForce(new Vector2(0f, jumpForce + (elapsedTime * extraPower)),ForceMode2D.Impulse);
				//			rb2d.AddForce(jumpSpeed(0f,(elapsedTime * extraPower)));
				//			rb2d.AddForce(new Vector2(jumpForce + (elapsedTime * extraPower)));
			
				jump = false;
			}
		}
//	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("ShadowObstacle")) {
			other.attachedRigidbody.isKinematic=false;
			gamecontroller.Damage ();
			StartCoroutine(DoBlinks(blinkSpeed, blinkDuration));//Blink the player. The first value is duration, the other is speed
			jumpSpeed = new Vector2 (0,500);
			collisionsound.Play ();
		}
		else if (other.CompareTag ("Obstacle")) {
			other.attachedRigidbody.isKinematic = false;
		}
	}

	//Make the player Blink when damage is recieved
	public IEnumerator DoBlinks(float duration, float blinkTime) {
		while (duration > 0f) {
			duration -= Time.deltaTime;
			
			//toggle renderer
			GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
			
			//wait for a bit
			yield return new WaitForSeconds(blinkTime);
		}
		
		//make sure renderer is enabled when we exit
		GetComponent<Renderer>().enabled = true;
	}
	
}