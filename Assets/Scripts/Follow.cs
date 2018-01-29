using System.Collections;
using UnityEngine;

public class Follow : MonoBehaviour {

	public Transform Player;
	public GameObject Play;
	public Vector3 offset; 
	public Camera cameraFOV;
	public Rigidbody rb;
	private float changeFOV;
	public Vector3 sideMoveOffset;

	// Use this for initialization
	void Start () 
	{
	}
		
	// Update is called once per frame
	void LateUpdate () {
		sideMoveOffset.Set(rb.velocity.x * transform.right.x / 75f, rb.velocity.y * transform.right.y/ 75f, rb.velocity.z * transform.right.z/ 75f);

		transform.rotation = Play.transform.rotation;

		changeFOV = 70 + rb.velocity.x * transform.forward.x + rb.velocity.y * transform.forward.y + rb.velocity.z * transform.forward.z;
		cameraFOV.fieldOfView = changeFOV;
	}
}
// test	