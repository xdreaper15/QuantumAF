using UnityEngine;
using System.Collections;


public class QuantumSpawner : MonoBehaviour 
{

	public Transform Warning, Warning1; //Warning Information Reference
	public Transform Asteroid, Asteroid1; //Asteroid Information Reference
	public GameObject WarningField, WarningField1; //Warning Object
	public GameObject AsteroidClone, AsteroidClone1; //Asteroid Object
	public int maxAst = 20; //max # of Asteroids
	public int currentAst = 0; //current # of Asteroids
	public int currentWarning = 0; //current # of Asteroids
	public int minAstSize; //min Ast size
	public int maxAstSize; //max Ast size
	private int minAstTorq = 10000;
	private int maxAstTorq = 20000; //max Ast Torq
	private int maxSpawnRadius = 90; //max radius at which Asteroids spawn
	private Quaternion astRotation, astRotation1; //Starting Rotation of Asteriod
	private Vector3 astSpawn, astSpawn1; //Starting Position of Asteroid
	private float warningSize, warningSize1; //Size of Warning
	private float astSize, astSize1; //Size of Asteroid
	private float astWarnBuffer = 1.08F; //Buffer between Asteroid Size and Warnign Size
	private int astTorqX, astTorqX1; //X-Axis Torque on Asteroid
	private int astTorqY, astTorqY1; //Y-Axis Torque on Asteroid
	private int astTorqZ, astTorqZ1; //Z-Axis Torque on Asteroid
	private int spawnLocX, spawnLocX1; //X Position of Asteroid Spawn
	private int spawnLocY, spawnLocY1; //X Position of Asteroid Spawn
	private int spawnLocZ, spawnLocZ1; //X Position of Asteroid Spawn
	private int startAstRotationX, startAstRotationX1; //X-Axis Staritiong Rotation of Asteroid
	private int startAstRotationY, startAstRotationY1; //Y-Axis Staritiong Rotation of Asteroid
	private int startAstRotationZ, startAstRotationZ1; //Z-Axis Staritiong Rotation of Asteroid
	private int startAstRotationW, startAstRotationW1; //W-Axis Staritiong Rotation of Asteroid

	// Use this for initialization
	void Start ()
	{
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
				currentWarning ++; //adds 1 to the Warning Counter (currentWarning)
			}
		}
	}

	void SpawnWarning ()
	{
		astSize = Random.Range(minAstSize, maxAstSize); //establish ASTEROID Size
		astSize1 = Random.Range(minAstSize, maxAstSize); 
		warningSize = astSize * astWarnBuffer; //establish Warning Size
		warningSize1 = astSize1 * astWarnBuffer; 

		startAstRotationX = Random.Range (-10000, 10000); //set random X rotation between 0 and 360 degrees
		startAstRotationY = Random.Range (-10000, 10000); //set random Y rotation between 0 and 360 degrees
		startAstRotationZ = Random.Range (-10000, 10000); //set random Z rotation between 0 and 360 degrees
		startAstRotationW = Random.Range (-10000, 10000); //set random W rotation between 0 and 360 degrees

		startAstRotationX1 = Random.Range (-10000, 10000); //set random X rotation between 0 and 360 degrees
		startAstRotationY1 = Random.Range (-10000, 10000); //set random Y rotation between 0 and 360 degrees
		startAstRotationZ1 = Random.Range (-10000, 10000); //set random Z rotation between 0 and 360 degrees
		startAstRotationW1 = Random.Range (-10000, 10000); //set random W rotation between 0 and 360 degrees

		astRotation.Set(startAstRotationX, startAstRotationY, startAstRotationZ, startAstRotationW); //Sets an initial Asteroid Rotation
		astRotation1.Set(startAstRotationX1, startAstRotationY1, startAstRotationZ1, startAstRotationW1);

		spawnLocX = Random.Range (-maxSpawnRadius, maxSpawnRadius); //spawn at random X position
		spawnLocY = Random.Range (-maxSpawnRadius, maxSpawnRadius); //spawn at random Y position
		spawnLocZ = Random.Range (-maxSpawnRadius, maxSpawnRadius); //spawn at random Z position

		spawnLocX1 = Random.Range (-maxSpawnRadius, maxSpawnRadius); //spawn at random X position
		spawnLocY1 = Random.Range (-maxSpawnRadius, maxSpawnRadius); //spawn at random Y position
		spawnLocZ1 = Random.Range (-maxSpawnRadius, maxSpawnRadius); //spawn at random Z position

		astSpawn.Set (spawnLocX, spawnLocY, spawnLocZ); //assigns ASTEROID spawn position
		astSpawn1.Set (spawnLocX1, spawnLocY1, spawnLocZ1);

		Warning.localScale = new Vector3(warningSize, warningSize, warningSize); // sets the size of the WARNING
		Asteroid.localScale = new Vector3 (astSize, astSize, astSize); //sets the size of the ASTEROID

		Warning1.localScale = new Vector3(warningSize1, warningSize1, warningSize1); // sets the size of the WARNING
		Asteroid1.localScale = new Vector3 (astSize1, astSize1, astSize1); //sets the size of the ASTEROID

		GameObject clone = (GameObject)Instantiate (WarningField, astSpawn, astRotation); //Spawns Warning Clone
		GameObject clone1 = (GameObject)Instantiate (WarningField1, astSpawn1, astRotation1); //Spawns Warning Clone

		Destroy(clone, 1.1f); //Destroys Warning Clone
		Destroy(clone1, 1.1f);
		Invoke ("SpawnAsteroid", 1.0f); //Spawns Asteroid
	}
		
	void SpawnAsteroid()
	{

		GameObject astClone = (GameObject)Instantiate (AsteroidClone, astSpawn, astRotation); // Spawn Asteroid Clone, at Warning Location, With Warning Rotation
		GameObject astClone1 = (GameObject)Instantiate (AsteroidClone, astSpawn1, astRotation1);

		astTorqX = Random.Range (minAstTorq, maxAstTorq); //establish random X-axis Torque
		astTorqY = Random.Range (minAstTorq, maxAstTorq); //establish random Y-axis Torque
		astTorqZ = Random.Range (minAstTorq, maxAstTorq); //establish random Z-axis Torque

		astTorqX1 = Random.Range (minAstTorq, maxAstTorq); //establish random X-axis Torque
		astTorqY1 = Random.Range (minAstTorq, maxAstTorq); //establish random Y-axis Torque
		astTorqZ1 = Random.Range (minAstTorq, maxAstTorq); //establish random Z-axis Torque

		Rigidbody astRigid = astClone.GetComponent<Rigidbody> (); //Gets the new clones rigidbody and assigns to new rigidbody astRigid
		Rigidbody astRigid1 = astClone1.GetComponent<Rigidbody> ();

		astRigid.AddRelativeTorque (transform.forward * astTorqX); //Apply established Torques
		astRigid.AddRelativeTorque (transform.up * astTorqY); //Apply established Torques
		astRigid.AddRelativeTorque (transform.right * astTorqZ); //Apply established Torques

		astRigid1.AddRelativeTorque (transform.forward * astTorqX1); //Apply established Torques
		astRigid1.AddRelativeTorque (transform.up * astTorqY1); //Apply established Torques
		astRigid1.AddRelativeTorque (transform.right * astTorqZ1); //Apply established Torques

		astRigid.AddForce (-astTorqX, 0, 0); // Adds a Movement Force in the direction of spin = to 1/4 torque (X Axis)
		astRigid.AddForce (0, -astTorqY, 0); // Adds a Movement Force in the direction of spin = to 1/4 torque (Y Axis)
		astRigid.AddForce (0, 0, -astTorqZ); // Adds a Movement Force in the direction of spin = to 1/4 torque (Z Axis)

		astRigid1.AddForce (-astTorqX1, 0, 0); // Adds a Movement Force in the direction of spin = to 1/4 torque (X Axis)
		astRigid1.AddForce (0, -astTorqY1, 0); // Adds a Movement Force in the direction of spin = to 1/4 torque (Y Axis)
		astRigid1.AddForce (0, 0, -astTorqZ1); // Adds a Movement Force in the direction of spin = to 1/4 torque (Z Axis)

		Destroy(astClone, 20f); //Destroy Asteroid Clone
		Destroy(astClone1, 20f);
		currentWarning --; //Reduces Warning # allowing another Warning to spawn
		currentAst ++; //Increases Asteroid number
		Invoke ("SubtractCurrentAst", 4.2f);

	}

	void SubtractCurrentAst()
	{
		currentAst --; //Decreases Asteroid number
	}

}
//test