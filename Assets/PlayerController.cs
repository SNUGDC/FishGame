using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private PlayerValues PV;
	private Rigidbody2D rb;
	private GameController GC;
	public float snapBoundary = 0.2f;
	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		GC = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}// get components
	void Start(){
		PV.inwater = true; // start in pot
		PV.gameover=false;
		PV.land = false;
		PV.accY_now = Input.acceleration.y;
	}//initialize
	void Update () {
		PV.touchcount = Input.touchCount;
		GetDeltaAccY();
		Debug.Log(Input.acceleration.x + ", "+Input.acceleration.y + ", " + Input.acceleration.z);
		if (PV.inwater==true||PV.land==true) {
			if (isNotJumping())
				return;
			Vector2 tilt=new Vector2(Input.acceleration.x,-Input.acceleration.y);
			rb.velocity = tilt.normalized*40;
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Water") {
			inwater_do ();
		} 
		if (other.gameObject.tag == "Ground") {
			land_do ();
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Water") {
			inwater_not_do ();
		}
		if (other.gameObject.tag == "Ground") {
			land_not_do ();
		}
	}

	void GetDeltaAccY()
	{
		PV.accY_before = PV.accY_now;
		PV.accY_now = Input.acceleration.y;
		PV.accY_delta = PV.accY_now - PV.accY_before;
	}
	bool isNotJumping()
	{
		if(PV.touchcount == 0 && Mathf.Abs(PV.accY_delta) < snapBoundary) return true;
		else if(PV.touchcount != 0){
			Debug.Log("Jumped by touch");
			return false;
		}
		else{
			Debug.Log("Jumped by snap");
			return false;
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
