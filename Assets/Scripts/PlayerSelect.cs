using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class PlayerSelect : MonoBehaviour {

  	Renderer screenRenderer;
	private int playerNum = 1;
  public int kbm;

  void Start()
  {
      


  }


	// Use this for initialization
  void Update()
  {




		var inputDevice = (InputManager.Devices.Count + kbm > playerNum + kbm) ? InputManager.Devices[playerNum - kbm] : null;
    if (inputDevice != null)
    {
		Debug.Log (InputManager.Devices[playerNum - kbm]);
    }
    else
    {
      return;
    }
  }
}
