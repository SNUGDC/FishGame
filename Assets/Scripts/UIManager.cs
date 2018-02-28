using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public GameObject popUp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void MoveScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}
	public void QuitGame(){
		Application.Quit();
	}
	public void SetPopUpActive(bool b){
		popUp.SetActive(b);
	}
}
