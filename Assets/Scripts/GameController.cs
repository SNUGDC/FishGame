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
		if (PV.gameover==true)
			gameover ();
	}
	void gameover(){
		Player.transform.position = PV.Savepoint.position;
		PV.StartinWater ();
	}

	public void showlefttime(){

		PV.left_time_text.text = "Time Left : " + (int)PV.time_left;
	}
	public void hidelefttime(){
		PV.left_time_text.text = "";
	}

	public void set_left_time(){
		PV.time_left =PV.time_limit ;
		StartCoroutine (timecounter());
	}
	IEnumerator timecounter(){
		while (!PV.inwater) {
			showlefttime ();
			yield return new WaitForSeconds (1f);
			PV.time_left -=1;
			if (PV.time_left <= 0) {
				Debug.Log ("gameover");
				PV.gameover = true;
				break;
			}
		}
		hidelefttime ();
	}
}