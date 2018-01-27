using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InControl;

public class MainMenu : MonoBehaviour {

  int playerNum;
  int zSpawnOffset = 0;
  Renderer screenRenderer;
  Animator CameraObject;
  GameObject playBtn, playGrp, settingsGrp;
  
  

  void Start()
  {
      Debug.Log(SceneManager.GetActiveScene().ToString());
      if (SceneManager.GetActiveScene().ToString() == "MainMenu")
      {
        
        zSpawnOffset = zSpawnOffset + 200;
        Debug.Log(zSpawnOffset);
      }
  }


	// Use this for initialization
  void Update()
    {


     
    }










    //button sub menus
  void Play()
  {
    playGrp.SetActive(true);
    
  }
  void DisablePlay()
  {
    playBtn.SetActive(false);
    playGrp.SetActive(false);
  }

  void Position1()
  {
    CameraObject.SetFloat("Animate",0);
  }

  void Position2()
  {
    playBtn.SetActive(false);
    CameraObject.SetFloat("Animate",1);
  }


}
