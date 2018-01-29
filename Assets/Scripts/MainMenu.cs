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
  public GameObject btn_play, btn_settings, btn_exit, btn_local, btn_online, btn_understood, pnl_online, pnl_audio, pnl_controls, btn_audio, btn_controls, 
  hoverSound, sfxhoverSound, clickSound, coll;
  public Collider2D mainMenuCanvas;

  public Vector2 vr;


  
  
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
      Toggle(btn_play, "on");
      Toggle(btn_settings, "on");
      Toggle(btn_exit, "on");
      Toggle(btn_local, "off");
      Toggle(btn_online, "off");
      Toggle(btn_understood, "off");
      Toggle(btn_audio, "off");
      Toggle(btn_controls, "off");
      
  }


	// Use this for initialization
  void LateUpdate()
    {
     

     
    }


  // public object RandVector(int arg1, string arg2)
  // {

  //   float r1f = UnityEngine.Random.Range(-10f,10f);
  //   float r2f = UnityEngine.Random.Range(-10f,10f);
  //   float r3f = UnityEngine.Random.Range(-10f,10f);
  //   float r4f = UnityEngine.Random.Range(-10f,10f);
  //   int r1i = Mathf.RoundToInt(UnityEngine.Random.Range(-10,10));
  //   int r2i = Mathf.RoundToInt(UnityEngine.Random.Range(-10,10));
  //   int r3i = Mathf.RoundToInt(UnityEngine.Random.Range(-10,10));
  //   int r4i = Mathf.RoundToInt(UnityEngine.Random.Range(-10,10));
  //   if (arg1 == 2)
  //   {
  //     if ( arg2 == "float")
  //     {
  //       Vector2 v2 = new Vector2(r1f, r2f);
  //       return v2;
  //     }
  //     else if(arg2 == "string")
  //     {
  //       Vector2Int v2 = new Vector2Int(r1i, r2i);
  //       return v2;
  //     }
  //   }
  //   if (arg1 == 3)
  //   {
  //     if ( arg2 == "float")
  //     {
  //       Vector3 v3 = new Vector3(r1f, r2f, r3f);
  //       return v3;
  //     }
  //     else if(arg2 == "string")
  //     {
  //       Vector3Int v3 = new Vector3Int(r1i, r2i, r3i);
  //       return v3;
  //     }
  //   }
  //   else
  //   return;
  // }







    //button sub menus
  public void Play()
  {
    Toggle(btn_local, "on");
    Toggle(btn_online, "on");

    
    vr = new Vector2 (UnityEngine.Random.Range(-10f,10f), UnityEngine.Random.Range(-10f,10f));
    btn_settings.GetComponent<Rigidbody2D>().AddForce(vr);
    vr = new Vector2 ((UnityEngine.Random.Range(-10f,10f)), (UnityEngine.Random.Range(-10f,10f)));
    btn_exit.GetComponent<Rigidbody2D>().AddForce(vr);


    Destroy(btn_settings, 10f);
    Destroy(btn_exit, 10f);
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
    Toggle(btn_play, "on");
    Toggle(btn_settings, "on");
    Toggle(btn_exit, "on");
    cameraObject.SetInteger("test",0);
  }

  public void SettingsMenuPosition()
  {
    Toggle(btn_play, "off");
    Toggle(btn_settings, "off");
    Toggle(btn_exit, "off");
    Toggle(btn_local, "off");
    Toggle(btn_online, "off");
    Toggle(btn_audio, "on");
    Toggle(btn_controls, "on");
    Toggle(pnl_audio, "on");
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

  public void OnlineClick()
  {
    pnl_online.SetActive(true);
    btn_understood.SetActive(true);
    Toggle(btn_understood, "on");
    Toggle(pnl_online, "on");
    Toggle(btn_play, "fall");
    Toggle(btn_local, "fall");
    Toggle(btn_online, "fall");
  }
  public void PlayClick()
  {
    clickSound.GetComponent<AudioSource>().Play();
  }

  public void UnderstoodClick()
  {
    Toggle(btn_local, "off");
    Toggle(btn_online, "off");
    Toggle(btn_understood, "off");
    Toggle(btn_play, "on");
    Toggle(btn_settings, "on");
    Toggle(btn_exit, "on");
    // Instantiate(btn_play);
    // btn_play.SetActive(true);
    // btn_play.GetComponent<Rigidbody2D>().simulated = false;
    // Instantiate(btn_settings);
    // btn_settings.SetActive(true);
    // btn_settings.GetComponent<Rigidbody2D>().simulated = false;
    // Instantiate(btn_exit);
    // btn_exit.SetActive(true);
    // btn_exit.GetComponent<Rigidbody2D>().simulated = false;
  }

  public void DeleteBtn()
  {
    
  }

  public void Toggle(GameObject arg1, string arg2)
  {
    if (arg2 == "on")
    {
      
      Destroy(arg1);
      Instantiate(arg1);
      arg1.SetActive(true);
      arg1.GetComponent<Rigidbody2D>().simulated = false;
      
    }
    if (arg2 == "off")
    {
      arg1.SetActive(false);
    }
    if (arg2 == "fall")
    {
      
      vr = new Vector2 ((UnityEngine.Random.Range(-10f,10f)), (UnityEngine.Random.Range(-10f,10f)));
      arg1.GetComponent<Rigidbody2D>().simulated = true;
      arg1.GetComponent<Rigidbody2D>().AddForce(vr);
    }
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
