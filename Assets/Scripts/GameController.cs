using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private PlayerValues PV;
	private VFX_Player VFXP;
	private GameObject Player;
	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
		Player = GameObject.FindGameObjectWithTag ("Player");
		VFXP = Player.GetComponent<VFX_Player> ();
	}
	void Start(){
		hidelefttime ();
	}
	void Update(){
		if (PV.gameover==true)
			gameover ();
	}
	void gameover(){
		/*
		 * Write code for stop Scenes, gameover UI
		 * 
		 * 
		 * 
		*/
		Player.transform.position = PV.Savepoint.position;
		Player.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
		PV.StartinWater ();
	}

	public void showlefttime(){

		PV.left_time_text.text = "Time Left : " + (float)PV.time_left_x100/100;
	}
	public void hidelefttime(){
		PV.left_time_text.text = "";
	}

	public void set_left_time(){
		PV.time_left_x100 =PV.time_limit*100 ;
		StartCoroutine (timecounter());
	}
	IEnumerator timecounter(){
		
		while (!PV.inwater) {
			showlefttime ();
			yield return new WaitForSeconds (0.01f);
			PV.time_left_x100 -=1;
			if (PV.time_left_x100 < 6000)
				VFXP.Activate_PlayerSprite (1);
			if (PV.time_left_x100 < 3000)
				VFXP.Activate_PlayerSprite (2);
			if (PV.time_left_x100 < 1500)
				VFXP.Activate_PlayerSprite (3);
			if (PV.time_left_x100 <= 0) {
				Debug.Log ("gameover");
				PV.gameover = true;
				break;
			}
		}
		VFXP.Activate_PlayerSprite (0);
		hidelefttime ();
	}
}