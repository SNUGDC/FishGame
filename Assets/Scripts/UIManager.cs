using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public GameObject popUp;

	PlayerValues PV;

	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
	}
	public void MoveScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}
	public void QuitGame(){
		Application.Quit();
	}
	public void SetPopUpActive(bool b){
		popUp.SetActive(b);
		PV.isPaused = b;
	}
}
