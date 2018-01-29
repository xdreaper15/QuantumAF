using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using InControl;

public class MainMenu : MonoBehaviour {

  int playerNum;
  int zSpawnOffset = 0;
  int test;
  public Scene activeScene;
  public Scene Level1;
  public Animator cameraObject;
  public GameObject btn_play, btn_settings, btn_exit, btn_local, btn_online, hoverSound, sfxhoverSound, clickSound, coll;
  public Collider2D mainMenuCanvas;

  
  
  void Awake()
  {
    activeScene = SceneManager.GetActiveScene();
    Level1 = SceneManager.GetSceneByName("Level 1");
  }
  void Start()
  {
      if (activeScene.name.ToString() == "MainMenu")
      {
        
        zSpawnOffset = zSpawnOffset + 200;
      }
      btn_local.SetActive(false);
      btn_online.SetActive(false);
      btn_play.GetComponent<Rigidbody2D>().simulated = false;
      btn_settings.GetComponent<Rigidbody2D>().simulated = false;
      btn_exit.GetComponent<Rigidbody2D>().simulated = false;
      btn_local.GetComponent<Rigidbody2D>().simulated = false;
      btn_online.GetComponent<Rigidbody2D>().simulated = false;
      
  }


	// Use this for initialization
  void LateUpdate()
    {
     

     
    }










    //button sub menus
  public void Play()
  {
    btn_local.SetActive(true);
    btn_online.SetActive(true);
    btn_settings.GetComponent<Rigidbody2D>().simulated = true;
    btn_exit.GetComponent<Rigidbody2D>().simulated = true;

    Rigidbody2D rb1 = btn_settings.GetComponent<Rigidbody2D>();
    Vector2 vrb1 = new Vector2 (UnityEngine.Random.Range(-10f,10f), UnityEngine.Random.Range(-10f,10f));
    rb1.AddForce(vrb1);

    Rigidbody2D rb2 = btn_exit.GetComponent<Rigidbody2D>();
    Vector2 vrb2 = new Vector2 (UnityEngine.Random.Range(-10f,10f), UnityEngine.Random.Range(-10f,10f));
    rb2.AddForce(vrb2);


    Destroy(btn_settings, 10f);
    Destroy(btn_exit, 10f);
  }
  public void DisablePlay()
  {
    btn_settings.SetActive(true);
    btn_settings.GetComponent<BoxCollider2D>().enabled = true;
    btn_exit.SetActive(true);
    btn_exit.GetComponent<BoxCollider2D>().enabled = true;
    btn_local.SetActive(false);
    btn_online.SetActive(false);
    
  }
  public void ClickPlayLocal()
  {
    StartCoroutine("PlayLocal");
  }
  public IEnumerator PlayLocal()
  {
    
    yield return new WaitForSecondsRealtime(3f);
    SceneManager.LoadScene("Level 1");
    SceneManager.UnloadSceneAsync("MainMenu");
  }
  public void Quit()
  {
    Application.Quit();
  }

  public void MainMenuPosition()
  {
    btn_play.SetActive(true);
    btn_settings.SetActive(true);
    btn_exit.SetActive(true);
    cameraObject.SetInteger("test",0);
  }

  public void SettingsMenuPosition()
  {
    DisablePlay();
    cameraObject.SetInteger("test",2);
  }

  public void PlayHover()
  {
    hoverSound.GetComponent<AudioSource>().Play();
  }

  public void sfxPlayHover()
  {
    sfxhoverSound.GetComponent<AudioSource>().Play();
  }

  public void PlayClick()
  {
    clickSound.GetComponent<AudioSource>().Play();
  }

  public void DeleteBtn()
  {
    
  }
  
  // public void OnTriggerEnter2D(Collider2D col)
  // {
  //   print(col.gameObject.name);
  //   if (col.gameObject.tag == "Button") 
  //   {
  //       print("Destroying " + col.gameObject);
  //       Destroy(col.gameObject);
  //   }
  //}
}
