﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public static MenuManager Instance;

	public GameObject Startmenu;
	public GameObject GUI;
	public GameObject Gameover;
	public GameObject GameFinish;
	public GameObject GameBrexitText;
	public GameObject GameFinishBrexit;
	public GameObject GamePause;
	public GameObject LoadingScreen;
    public GameObject GameWon;

	void Awake(){
		Instance = this;
		Startmenu.SetActive (true);
		GUI.SetActive (false);
		Gameover.SetActive (false);
		GameFinish.SetActive (false);
		GamePause.SetActive (false);
		LoadingScreen.SetActive (true);
        GameWon.SetActive(false);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (StartGame (2));
	}

	public void NextLevel(){
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
		LoadingSreen.Show ();
		SceneManager.LoadSceneAsync (LevelManager.Instance.nextLevelName);
	}

	public void RestartGame(){
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
		LoadingSreen.Show ();
		SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex);
	}

	public void HomeScene(){
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
		Time.timeScale = 1;
		LoadingSreen.Show ();
		SceneManager.LoadSceneAsync ("MainMenu");

	}

	public void Gamefinish(){
            StartCoroutine(GamefinishCo(2));       
	}	
    public void Gamewon(){
            StartCoroutine(GameWonCo(2));
    }

    public void GameBexitDeal()
    {
        StartCoroutine(GameBexitDealCo(0.2f));
    }

    public void GameOver(){
		StartCoroutine (GameOverCo (1));
	}

	public void Pause(){
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
		if (Time.timeScale == 0) {
			GamePause.SetActive (false);
			GUI.SetActive (true);
			Time.timeScale = 1;
		} else {
			GamePause.SetActive (true);
			GUI.SetActive (false);
			Time.timeScale = 0;
		}
	}

	public void GotoCheckPoint(){
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
		GUI.SetActive (true);
		Gameover.SetActive (false);

		if (!LevelManager.Instance.isLastLevelOfWorld)
			GameManager.Instance.GotoCheckPoint ();
		else
			RestartGame ();
	}

	IEnumerator StartGame(float time){
		yield return new WaitForSeconds (time - 0.5f);
		Startmenu.GetComponent<Animator> ().SetTrigger ("play");

		yield return new WaitForSeconds (0.5f);
		Startmenu.SetActive (false);
		GUI.SetActive (true);

		GameManager.Instance.StartGame ();
	}

	IEnumerator GamefinishCo(float time){
		GUI.SetActive (false);

		yield return new WaitForSeconds (time);

		GameFinish.SetActive (true);
	}

    IEnumerator GameWonCo(float time)
    {
        GUI.SetActive(false);

        yield return new WaitForSeconds(time);

        GameWon.SetActive(true);
    }
       IEnumerator GameBexitDealCo(float time)
    {
        GUI.SetActive(false);
        GameBrexitText.SetActive(false);

        yield return new WaitForSeconds(time);

        GameFinishBrexit.SetActive(true);
    }

    IEnumerator GameOverCo(float time){
		GUI.SetActive (false);
        
		yield return new WaitForSeconds (time);

		Gameover.SetActive (true);
	}
}
