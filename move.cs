using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class Move : MonoBehaviour {

	private float pitchSens = 300*3.5f;
	private float rollSens = 600*3.5f;
	private float yawSens = 600*3.5f;
	[SerializeField]public static float boostAmt;
	private static float speedValue;
	public static float boostStart = 500f;
	private Vector3 speed;
	private Vector3 strafeSpeed;
	float ThrustMod = 40;
	float BoostMod = 30;
	public Text speedText;
	public Text boostText;
	public static Rigidbody rb;
	public Material booster; 

	private float fireBuffer;
	private float fireRate = 0.1f;
	public Transform bulletPosition;
	public GameObject Bullet;
	private float bulletSpeed = 10000f;
	private float fire;

	public int playerNum; 
	public GameObject playerobj;
	public Rigidbody EachplayerRigid;
	public Transform EachplayerTrans;
	public Camera SetCam1;
	public Camera SetCam2;
	public Camera SetCam3;
	public Camera SetCam4;
	public Camera AllSeeingEye;

	public Text speedText3;
	public Text boostText3;

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

		if (InputManager.Devices.Count == 1) 
		{
			SetCam1.rect = new Rect(0, 0, 1, 1);
		}
		else if (InputManager.Devices.Count == 2) 
		{
			SetCam1.rect = new Rect(0,0,0.5f,1);
			SetCam2.rect = new Rect(0.5f,0,0.5f, 1);
		} 
		else if (InputManager.Devices.Count == 3) 
		{
			SetCam1.rect = new Rect(0,0.5f,0.5f,0.5f);
			SetCam2.rect = new Rect(0.5f,0.5f,0.5f,0.5f);
			SetCam3.rect = new Rect(0,0,0.5f,0.5f);
			AllSeeingEye.rect = new Rect(0.5f,0,0.5f,0.5f);
		} 
		else if (InputManager.Devices.Count == 4) 
		{
			SetCam1.rect = new Rect(0,0.5f,0.5f,0.5f);
			SetCam2.rect = new Rect(0.5f,0.5f,0.5f,0.5f);
			SetCam3.rect = new Rect(0,0,0.5f,0.5f);
			SetCam3.rect = new Rect(0.5f,0,0.5f,0.5f);
		}
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

		if (boostAmt <= 1000) 	
		{
		boostAmt = boostAmt + 2;
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
			
		if (inputDevice.RightBumper > 0 && boostAmt > 0) 
		{
			EachplayerRigid.AddForce(EachplayerTrans.forward * BoostMod);
			boostAmt = boostAmt - 20f;
			booster.SetColor ("_EmissionColor", Color.red);
		} 
		else 
		{	
			booster.SetColor ("_EmissionColor", Color.black);
		}
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
		Debug.Log("SetUIText()");
	}

}
