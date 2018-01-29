using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class Move : MonoBehaviour {

	private float pitchSens = 400*3.5f;
	private float rollSens = 600*3.5f;
	private float yawSens = 600*3.5f;
	public float boostAmt1;
	public float boostAmt2;
	public float boostAmt3;
	public float boostAmt4;
	public float boostAmtUI;
	private static float speedValue;
	public static float boostStart = 500f;
	private Vector3 speed;
	float ThrustMod = 40;
	float BoostMod = 30;
	public Text speedText;
	public Text boostText;
	public Text healthText;
	public static Rigidbody rb;
	public Material booster; 

	private float fireBuffer;
	private float fireRate = 0.1f;
	public Transform bulletPosition1, bulletPosition2;
	public GameObject Bullet;
	private float bulletSpeed = 3000f;

	public int playerNum;
	public GameObject playerobj;
	public Rigidbody EachplayerRigid;
	public Transform EachplayerTrans;
	public Camera SetCam1;
	public Camera SetCam2;
	public Camera SetCam3;
	public Camera SetCam4;
	public Camera AllSeeingEye;

	float intRightstickx;
	float intLeftstickx;
	float intRightsticky;
	float intLeftsticky;

	private float playerhealth = 7;
	private float player1Health;
	private float player2Health;
	private float player3Health;
	private float player4Health ;
	public int player1Hit;
	public float healthUI;


	void Start () 
	{
		EachplayerTrans = GetComponent<Transform> ();
		EachplayerRigid = GetComponent<Rigidbody> ();
		fireBuffer = 0;
		boostAmt1 = boostStart;
		boostAmt2 = boostStart;
		boostAmt3 = boostStart;
		boostAmt4 = boostStart;
		player1Health = playerhealth;
		player2Health = playerhealth;
		player3Health = playerhealth;
		player4Health = playerhealth;
		healthUI = playerhealth;
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
    }
        
    void LateUpdate()
    {
        
    	SetUIText ();
    }

	void OnCollisionEnter (Collision collisionInfo)
	{
		if (collisionInfo.collider.tag == "Bullet") {
			if (gameObject.tag == "Player 1") {
				player1Health--;
				healthUI = player1Health;
				if (player1Health == 0) {
					playerobj.SetActive (false);
				}
			} else if (playerobj.tag == "Player 2") {
				player2Health--;
				healthUI = player2Health;
				if (player2Health == 0) {
					playerobj.SetActive (false);
				}
			} else if (playerobj.tag == "Player 3") {
					player3Health--;
					healthUI = player3Health;
				if (player3Health == 0) {
					playerobj.SetActive (false);
				}
			} else if (playerobj.tag == "Player 4") {
						player4Health--;
						healthUI = player4Health;
				if (player4Health == 0) {
					playerobj.SetActive (false);
				}
			}
		}

		if (collisionInfo.collider.tag == "Sun") {
			if (gameObject.tag == "Player 1") {
					healthUI = 0;
					playerobj.SetActive (false);
			} else if (playerobj.tag == "Player 2") {
					healthUI = 0;
					playerobj.SetActive (false);
			} else if (playerobj.tag == "Player 3") {
					healthUI = 0;
					playerobj.SetActive (false);
			} else if (playerobj.tag == "Player 4") {
					healthUI = 0;
					playerobj.SetActive (false);
			}
		}
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
//		EachplayerRigid.AddRelativeTorque(Vector3.up * yawSens * Time.deltaTime * intRightstickx); //yaw right stick
		EachplayerRigid.AddRelativeTorque(Vector3.forward * rollSens * Time.deltaTime * -intLeftstickx); //roll left stick

		EachplayerRigid.AddForce(EachplayerTrans.forward * ThrustMod * inputDevice.RightTrigger); //thrust right stick 

		if (inputDevice.Action1 > 0) 
		{
			Shoot ();
		}
			
		if (inputDevice.LeftTrigger > 0) // boost
		{
			booster.SetColor ("_EmissionColor", Color.red);
				if (InputManager.Devices [0] == inputDevice) {
					if (boostAmt1 > 0) {
						EachplayerRigid.AddForce (EachplayerTrans.forward * BoostMod);
						boostAmt1 = boostAmt1 - 20f;
						boostAmtUI = boostAmt1;
						}
				} else if (InputManager.Devices [1] == inputDevice) {
					if (boostAmt2 > 0) {
						EachplayerRigid.AddForce (EachplayerTrans.forward * BoostMod);
						boostAmt2 = boostAmt2 - 20f;
						boostAmtUI = boostAmt2;
					} 
				} else if (InputManager.Devices [2] == inputDevice) {
					if (boostAmt3 > 0) {
						EachplayerRigid.AddForce (EachplayerTrans.forward * BoostMod);
						boostAmt3 = boostAmt3 - 20f;
						boostAmtUI = boostAmt3;
					} 
				} else if (InputManager.Devices [3] == inputDevice) {
					if (boostAmt4 > 0) {
						EachplayerRigid.AddForce (EachplayerTrans.forward * BoostMod);
						boostAmt4 = boostAmt4 - 20f;
						boostAmtUI = boostAmt4;
					} 
				}
			} 
			else 
			{
			booster.SetColor ("_EmissionColor", Color.black);
			if (InputManager.Devices [0] == inputDevice) {
					boostAmt1++;
					boostAmtUI = boostAmt1;
			} else if (InputManager.Devices [1] == inputDevice) {
					boostAmt2++;
					boostAmtUI = boostAmt2;
			} else if (InputManager.Devices [2] == inputDevice) {
					boostAmt3++;
					boostAmtUI = boostAmt3;
			} else if (InputManager.Devices [3] == inputDevice) {
					boostAmt4++;
					boostAmtUI = boostAmt4;
			}
		}
	}

	public void Shoot () 
	{
		if (fireBuffer == 0)
		{
			fireBuffer = 1;
			GameObject bulletclone = (GameObject)Instantiate (Bullet, bulletPosition1.position, bulletPosition1.rotation); //Spawns Bullet Clone
			Rigidbody bulletRigid = bulletclone.GetComponent<Rigidbody>();
			bulletRigid.AddForce (EachplayerTrans.transform.forward * bulletSpeed);
			Destroy (bulletclone, 1f);
			GameObject bulletclone2 = (GameObject)Instantiate (Bullet, bulletPosition2.position, bulletPosition2.rotation); //Spawns Bullet Clone
			Rigidbody bulletRigid2 = bulletclone2.GetComponent<Rigidbody>();
			bulletRigid2.AddForce (EachplayerTrans.transform.forward * bulletSpeed);
			Destroy (bulletclone2, 1f);
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
		boostText.text = "Boost: " + boostAmtUI.ToString();
		healthText.text = "Health: " + healthUI.ToString();
	}

}
//test