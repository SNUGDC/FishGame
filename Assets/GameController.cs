using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private PlayerValues PV;
	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
	}
	void Start(){
		hidelefttime ();
	}


	public void showlefttime(){

		PV.left_time_text.text = "Time Left : " + PV.lefttime;
	}
	public void hidelefttime(){
		PV.left_time_text.text = "";
	}

	public void set_time_limit(float time){
		PV.lefttime = time;
		StartCoroutine (timecounter());
	}
	IEnumerator timecounter(){
		while (!PV.inwater) {
			showlefttime ();
			yield return new WaitForSeconds (0.1f);
			PV.lefttime -= 0.1f;
			if (PV.lefttime <= 0) {
				PV.gameover = true;
				break;
			}
		}
		hidelefttime ();
	}
}
