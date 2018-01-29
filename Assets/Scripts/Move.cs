using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class Move : MonoBehaviour {

	public float pitchSens = 1000f;
	public float rollSens = 1000f;
	public float yawSens = 1000f;
	[SerializeField]public static float boostAmt;
	[SerializeField]public static float speedValue;
	public static float boostStart = 500f;
	private Vector3 speed;
	private Vector3 strafeSpeed;
	float ThrustMod = 50;
	//float boostMod = 2;
	public Text speedText;
	public Text boostText;
	public static Rigidbody rb;
	public Material booster; 

	private float fireBuffer;
	private float fireRate = 0.05f;
	public Transform bulletPosition;
	public GameObject Bullet;
	private float bulletSpeed = 3000f;
	private float fire;

	public int playerNum; 
	public GameObject playerobj;
	public Rigidbody EachplayerRigid;
	public Transform EachplayerTrans;

	float intRightstickx;
	float intLeftstickx;
	float intRightsticky;
	float intLeftsticky;


	void Start () 
	{
		EachplayerTrans = GetComponent<Transform> ();
		EachplayerRigid = GetComponent<Rigidbody> ();
		fireBuffer = 0;
		boostAmt = boostStart;
		SetUIText ();
	}

	void FixedUpdate()
	{
        var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
        if (inputDevice == null)
        {
			playerobj.SetActive(false);
			Debug.Log (playerobj + "is a fag");
        }
        else
        {
        	UpdateShipWithInputDevice( inputDevice );
        }
    }
        
    void LateUpdate()
    {
        
    	SetUIText ();
    }
      
    void UpdateShipWithInputDevice( InputDevice inputDevice )
    {
		if (Mathf.Abs (inputDevice.RightStickX) > .4) {
			intRightstickx = inputDevice.RightStickX;
		} else {
			intRightstickx = 0f;
		}

		if (Mathf.Abs (inputDevice.LeftStickX) > .2) {
			intLeftstickx = inputDevice.LeftStickX;
		} else {
			intLeftstickx = 0f;
		}

		if (Mathf.Abs (inputDevice.RightStickY) > .2) {
			intRightsticky = inputDevice.RightStickY;
		} else {
			intRightsticky = 0f;
		}

		if (Mathf.Abs (inputDevice.LeftStickY) > .2) {
			intLeftsticky = inputDevice.LeftStickY;
		} else {
			intLeftsticky = 0f;
		}
			

		EachplayerRigid.AddRelativeTorque(Vector3.right * pitchSens * Time.deltaTime * intLeftsticky); //pitch left stick
		EachplayerRigid.AddRelativeTorque(Vector3.up * yawSens * Time.deltaTime * intRightstickx); //yaw right stick
		EachplayerRigid.AddRelativeTorque(Vector3.forward * rollSens * Time.deltaTime * -intLeftstickx); //roll left stick

		EachplayerRigid.AddForce(EachplayerTrans.forward * ThrustMod * intRightsticky); //thrust right stick 

		if (inputDevice.RightTrigger > 0) 
		{
			Shoot ();
		}



//		if (boostAmt <= 1000) 	
//		{
//			boostAmt = boostAmt + 2;
//		}
//
//		if (boost > 0 && boostAmt > 0) 
//		{
//			speed = transform.forward * boost * boostMod;
//			boostAmt = boostAmt - (10 * boost);
//			booster.SetColor ("_EmissionColor", Color.red);
//		} 
//		else 
//		{	
//			booster.SetColor ("_EmissionColor", Color.black);
//		}

	
	
	
	}


	public void Shoot () 
	{
		if (fireBuffer == 0)
		{
			fireBuffer = 1;
			GameObject bulletclone = (GameObject)Instantiate (Bullet, bulletPosition.position, bulletPosition.rotation); //Spawns Bullet Clone
			Rigidbody bulletRigid = bulletclone.GetComponent<Rigidbody>();
			bulletRigid.AddForce (EachplayerTrans.transform.forward * bulletSpeed);
			Destroy (bulletclone, 1f);
			Invoke ("FireBuffer",fireRate);
		}
	}
		

	void FireBuffer ()
	{
		fireBuffer = 0;
	}











	void SetUIText()
	{
		speedValue = Mathf.Pow(Mathf.Pow(EachplayerRigid.velocity.x,2F) + Mathf.Pow(EachplayerRigid.velocity.y,2F) + Mathf.Pow(EachplayerRigid.velocity.z,2F),.5F);
		speedText.text = "Speed: " + speedValue.ToString();
		boostText.text = "Boost: " + boostAmt.ToString();
	}

}
