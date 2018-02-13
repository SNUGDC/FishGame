using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private PlayerValues PV;
	private GameObject Player;
	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	void Start(){
		hidelefttime ();
	}
	void Update(){
		if (PV.gameover)
			gameover ();
	}
	void gameover(){
		Player.transform.position = PV.Savepoint.position;
		PV.StartinWater ();
		PV.gameover = false;
	}

	public void showlefttime(){

		PV.left_time_text.text = "Time Left : " + (int)PV.lefttime_x10/10;
	}
	public void hidelefttime(){
		PV.left_time_text.text = "";
	}

	public void set_time_limit(float time){
		PV.lefttime_x10 = (int)time*10;
		StartCoroutine (timecounter());
	}
	IEnumerator timecounter(){
		while (!PV.inwater) {
			showlefttime ();
			yield return new WaitForSeconds (0.1f);
			PV.lefttime_x10 -=1;
			if (PV.lefttime_x10 <= 0) {
				PV.gameover = true;
				break;
			}
		}
		hidelefttime ();
	}
}
