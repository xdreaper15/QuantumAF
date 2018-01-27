using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerSelect : MonoBehaviour {

  int playerNum;
  Renderer screenRenderer;


  void Start()
  {
      screenRenderer = GetComponent<Renderer>();
  }


	// Use this for initialization
  void Update()
    {


      var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
      if (inputDevice == null)
        {
          // If no controller exists for this cube, just make it translucent.
          //cubeRenderer.material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );
          //Component.material.color = new Color( 0f, 0f, 0f, 81f);

        }
      else
        {
                playerNum = 1;
        }




      //(transform.parent.inputDevice.Action1);
      //PlayerAction.InputDevice.ActiveDevice);
    }
}
