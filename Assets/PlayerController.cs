using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private PlayerValues PV;
	private Rigidbody2D rb;
	private GameController GC;
	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		GC = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}// get components
	void Start(){
		PV.inwater = true; // start in pot
		PV.gameover=false;
		PV.land = false;
	}//initialize
	void Update () {
		PV.touchcount = Input.touchCount;
		if (PV.inwater==true||PV.land==true) {
			if (PV.touchcount == 0)
				return;
			Vector2 tilt=new Vector2(Input.acceleration.x,-Input.acceleration.y);
			rb.velocity = tilt.normalized*40;
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Water") {
			inwater_do ();
		} 
	}
	void OnCollisionEnter2D(Collision2D other ){
		if (other.gameObject.tag == "Ground") {
			land_do ();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Water") {
			inwater_not_do ();
		}
	}
	void OnCollisionExit2D(Collision2D other ){
		if (other.gameObject.tag == "Ground") {
			land_not_do ();
		}
	}





	void inwater_do(){
		PV.inwater = true;
	}
	void inwater_not_do(){
		PV.inwater = false;
 		GC.set_time_limit (20);
	}
	void land_do (){
		PV.land = true;
	}
	void land_not_do(){
		PV.land = false;
	}
}
