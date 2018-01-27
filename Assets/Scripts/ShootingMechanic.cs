using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;


public class ShootingMechanic : MonoBehaviour {




	public Transform Player;
	public Transform bulletPosition;
	public GameObject Bullet;
	public Transform objectpos;
	private float bulletSpeed = 3000f;
	private float fire;
	private float fireBuffer = 0;
	[SerializeField]float fireRate = 0.3f;


	// Use this for initialization	
	void Start () 
	{
		

	}


	// Update is called once per frame
	void FixedUpdate () 
    {
		InputDevice inputDevice = InputManager.ActiveDevice;
		fire = inputDevice.RightBumper;

		if (fireBuffer == 0)
		{

			if (fire == 1) 
			{
				fireBuffer = 1;

//				GameObject bulletclone = (GameObject)Instantiate (Bullet, bulletPosition.position, bulletPosition.rotation); //Spawns Bullet Clone
//
//                 Rigidbody bulletclone = bulletPosition.GetComponentInParent<Rigidbody>();
//
//				bulletRigid.AddForce (objectpos.transform.forward * bulletSpeed);
//
//				Destroy (bulletclone, 1f); //Destroys Warning Clone
//
//				Invoke ("FireBuffer",fireRate);
			}
		}
	}

	void FireBuffer ()
	{
		fireBuffer = 0;
	}
}
