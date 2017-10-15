using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameController gamecontroller;

	public int numCollectables = 7;
	public float spawnTime;
	public float spawnTimeShadow;
	public float spawnTimeCollectable;
	public float spawnTimeStairs=0;
	public GameObject rock;
	public GameObject stalactite;
	public GameObject stalagmite;
	public GameObject shadowRock;
	public GameObject[] collectables;
	public GameObject stairs;

	private float timeSinceSpawn=6;
	private float shadowTimeSinceSpawn=6;
	private float collectableTimeSinceSpawn=0;
	private float stairsTimeSinceSpawn;
	private Vector3 v3;
	private float scale;
	// Use this for initialization
	void Start () 
	{
		//start with a rock spawned, with another coming in 3-5 seconds
		//Instantiate(rock, transform.position, transform.rotation);
		spawnTime = Random.Range(3,5);
		//Instantiate(shadowRock, shadowRock.transform.position, transform.rotation);
		spawnTimeShadow = Random.Range(3,5);
		spawnTimeCollectable = 7.0f;

	}
	
	// Update is called once per frame
	void Update ()
	{
		SpawnObstacle ();
		SpawnShadowObstacle ();
		SpawnCollectable ();
	}

	void SpawnObstacle ()
	{
		timeSinceSpawn += Time.deltaTime;
		
		if(timeSinceSpawn >= spawnTime) { //Checks every frame to see if spawnTime is less than time since last spawn
			int randObs;

			if  (gamecontroller.GetMult()>2){
				randObs=Random.Range(2,9);
				scale=Random.Range(6.0f, 7.0f);//eventually this can be up to 8.0f again if we have variable jump but not now
			} else if  (gamecontroller.GetMult()>1){
				randObs=Random.Range(0,7);
				scale=Random.Range(5.0f, 6.0f);
			} else {
				randObs=1;
				scale=Random.Range(4.0f, 5.0f);
			}
			if (randObs>7){
				v3.Set (Random.Range (-1, 2), 0.0f, 0.0f);
				stalagmite.transform.localScale = new Vector3(scale/5, scale/5, 0.0f);
				Instantiate(stalagmite, transform.position+v3, Quaternion.identity);// spawn another stalagmite
				
			} else if (randObs>4){
				v3.Set (Random.Range (-20, -18), 8.0f, 0.0f);
				stalactite.transform.localScale = new Vector3(scale/5, scale/5, 0.0f);
				Instantiate(stalactite, transform.position+v3, transform.rotation);// spawn another stalactite

			}else{
				v3.Set (Random.Range (-1, 2), 0.0f, 0.0f);
				rock.transform.localScale = new Vector3(scale, scale, 0.0f);
				Instantiate(rock, transform.position+v3, transform.rotation);// spawn another rock
			}

			timeSinceSpawn = 0.0f; //resets spawn counter
			spawnTime = Random.Range(3,5);
		}
	}

	void SpawnShadowObstacle ()
	{
		shadowTimeSinceSpawn += Time.deltaTime;
		
		if(shadowTimeSinceSpawn >= spawnTimeShadow) { //Checks every frame to see if spawnTime is less than time since last spawn
			v3.Set (Random.Range (-1, 2), 0.0f, 0.0f);
			if  (gamecontroller.GetMult()>2){
				scale=Random.Range(6.0f, 7.0f);//eventually this can be up to 8.0f again if we have variable jump but not now
			} else if  (gamecontroller.GetMult()>1){
				scale=Random.Range(5.0f, 6.0f);
			} else {
				scale=Random.Range(4.0f, 5.0f);
			}
			shadowRock.transform.localScale = new Vector3(scale, scale, 0.0f);
			Instantiate(shadowRock, shadowRock.transform.position+v3, transform.rotation);// spawn another shadow rock
			
			shadowTimeSinceSpawn = 0.0f; //resets spawn counter
			spawnTimeShadow = Random.Range(3,5);
		}
	}

	//Should only spawn once and then if the player misses it should spawn after a certain amount of time passes
	void SpawnStairs ()
	{
		stairsTimeSinceSpawn += Time.deltaTime;
		
		if(stairsTimeSinceSpawn >= spawnTimeStairs) { //Checks every frame to see if spawnTime is less than time since last spawn
			v3.Set (Random.Range (1, 5), 0.0f, 0.0f);
			Instantiate(stairs, stairs.transform.position+v3, transform.rotation);

			stairsTimeSinceSpawn = 0.0f; //resets spawn counter stairs
			collectableTimeSinceSpawn = 0.0f; //resets spawn counter collectables
			spawnTimeCollectable = Random.Range (5, 7);
			numCollectables=7;
			spawnTime=10;
			spawnTimeShadow=10;
		}
	}

	void SpawnCollectable ()
	{
		collectableTimeSinceSpawn += Time.deltaTime;//time gets closer for spawning
		if (numCollectables > 0) //if there are still collectables to collect
		{
			if (collectableTimeSinceSpawn >= spawnTimeCollectable) { //Checks every frame to see if spawnTime is less than time since last spawn
				GameObject collectable = collectables [Random.Range (0, collectables.Length)];
				v3.Set (Random.Range (-1, 5), Random.Range (-1, 4), 0.0f);
				collectable.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
				Instantiate (collectable, collectable.transform.position + v3, transform.rotation);// spawn another collectable

				collectableTimeSinceSpawn = 0.0f; //resets spawn counter
				spawnTimeCollectable = Random.Range (5, 7);
				numCollectables--;
			}
		}
		if (numCollectables == 1) { //if there is one collectable left
			stairsTimeSinceSpawn=0;
			spawnTimeStairs=7.0f;//stairs spawn after 7 seconds
		}
		if (numCollectables <= 0) {//spawn stairs after all collectables have spawned
			SpawnStairs ();
		}
	}
}



