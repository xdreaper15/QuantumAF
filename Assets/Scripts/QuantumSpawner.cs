using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class QuantumSpawner : MonoBehaviour 
{

	public int zSpawnOffset;
	public Transform Warning; //Warning Information Reference
	public Transform Asteroid; //Asteroid Information Reference
	public GameObject WarningField; //Warning Object
	public GameObject AsteroidClone; //Asteroid Object
	public int maxAst = 15; //max # of Asteroids
	public int currentAst = 0; //current # of Asteroids
	public int currentWarning = 0; //current # of Asteroids
	public int minAstSize; //min Ast size
	public int maxAstSize; //max Ast size
	private int minAstTorq = 10000;
	private int maxAstTorq = 20000; //max Ast Torq
	public int maxSpawnRadius = 65; //max radius at which Asteroids spawn
	private Quaternion astRotation; //Starting Rotation of Asteriod
	private Vector3 astSpawn; //Starting Position of Asteroid
	private float warningSize; //Size of Warning
	private float astSize; //Size of Asteroid
	private float astWarnBuffer = 1.08F; //Buffer between Asteroid Size and Warnign Size
	private int astTorqX; //X-Axis Torque on Asteroid
	private int astTorqY; //Y-Axis Torque on Asteroid
	private int astTorqZ; //Z-Axis Torque on Asteroid
	private int spawnLocX; //X Position of Asteroid Spawn
	private int spawnLocY; //X Position of Asteroid Spawn
	private int spawnLocZ; //X Position of Asteroid Spawn
	private int startAstRotationX; //X-Axis Staritiong Rotation of Asteroid
	private int startAstRotationY; //Y-Axis Staritiong Rotation of Asteroid
	private int startAstRotationZ; //Z-Axis Staritiong Rotation of Asteroid
	private int startAstRotationW; //W-Axis Staritiong Rotation of Asteroid

	// Use this for initialization
	void Start ()
	{
		
		if (SceneManager.GetActiveScene().ToString() == "MainMenu")
		{
			zSpawnOffset = 200;
		}
	}

	void Update ()
	{
	}

	void FixedUpdate ()
	{
		if (currentAst <= maxAst) // makes sure there are never more than 20 Asteroids
		{
			if (currentWarning < 1) // makes all Warnings spawn 1 at a time
			{
				SpawnWarning(); //spawns Warning
				currentWarning++; //adds 1 to the Warning Counter (currentWarning)
			}
		}
	}

	void SpawnWarning ()
	{
		astSize = Random.Range(minAstSize, maxAstSize); //establish ASTEROID Size
		warningSize = astSize * astWarnBuffer; //establish Warning Size

		startAstRotationX = Random.Range (-10000, 10000); //set random X rotation between 0 and 360 degrees
		startAstRotationY = Random.Range (-10000, 10000); //set random Y rotation between 0 and 360 degrees
		startAstRotationZ = Random.Range (-10000, 10000); //set random Z rotation between 0 and 360 degrees
		startAstRotationW = Random.Range (-10000, 10000); //set random W rotation between 0 and 360 degrees

		astRotation.Set(startAstRotationX, startAstRotationY, startAstRotationZ, startAstRotationW); //Sets an initial Asteroid Rotation

		spawnLocX = Random.Range (-maxSpawnRadius, maxSpawnRadius); //spawn at random X position
		spawnLocY = Random.Range (-maxSpawnRadius, maxSpawnRadius); //spawn at random Y position
		spawnLocZ = Random.Range (-maxSpawnRadius, maxSpawnRadius); //spawn at random Z position

		astSpawn.Set (spawnLocX, spawnLocY, spawnLocZ); //assigns ASTEROID spawn position

		Warning.localScale = new Vector3(warningSize, warningSize, warningSize); // sets the size of the WARNING
		Asteroid.localScale = new Vector3 (astSize, astSize, astSize); //sets the size of the ASTEROID

		GameObject clone = (GameObject)Instantiate (WarningField, astSpawn, astRotation); //Spawns Warning Clone

		Destroy(clone, 1.1f); //Destroys Warning Clone
		Invoke ("SpawnAsteroid", 1.0f); //Spawns Asteroid
	}
		
	void SpawnAsteroid()
	{

		GameObject astClone = (GameObject)Instantiate (AsteroidClone, astSpawn, astRotation); // Spawn Asteroid Clone, at Warning Location, With Warning Rotation

		astTorqX = Random.Range (minAstTorq, maxAstTorq); //establish random X-axis Torque
		astTorqY = Random.Range (minAstTorq, maxAstTorq); //establish random Y-axis Torque
		astTorqZ = Random.Range (minAstTorq, maxAstTorq); //establish random Z-axis Torque

		Rigidbody astRigid = astClone.GetComponent<Rigidbody> (); //Gets the new clones rigidbody and assigns to new rigidbody astRigid

		astRigid.AddRelativeTorque (transform.forward * astTorqX); //Apply established Torques
		astRigid.AddRelativeTorque (transform.up * astTorqY); //Apply established Torques
		astRigid.AddRelativeTorque (transform.right * astTorqZ); //Apply established Torques

		astRigid.AddForce (-astTorqX / 4, 0, 0); // Adds a Movement Force in the direction of spin = to 1/4 torque (X Axis)
		astRigid.AddForce (0, -astTorqY / 4, 0); // Adds a Movement Force in the direction of spin = to 1/4 torque (Y Axis)
		astRigid.AddForce (0, 0, -astTorqZ / 4); // Adds a Movement Force in the direction of spin = to 1/4 torque (Z Axis)

		Destroy(astClone, 20f); //Destroy Asteroid Clone
		currentWarning--; //Reduces Warning # allowing another Warning to spawn
		currentAst++; //Increases Asteroid number
		Invoke ("SubtractCurrentAst", 4.2f);

	}

	void SubtractCurrentAst()
	{
		currentAst--; //Decreases Asteroid number
	}

}
//test