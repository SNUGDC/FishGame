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
		PV.StartinWater ();
		PV.accY_now = Input.acceleration.y;
	}//initialize
	void Update () {
		PV.touchcount = Input.touchCount;
		GetDeltaAccY();
		Debug.Log(Input.acceleration.x + ", "+Input.acceleration.y + ", " + Input.acceleration.z);
		if (PV.inwater==true||PV.land==true) {
			if (isNotJumping())
				return;
			Vector2 tilt=new Vector2(Input.acceleration.x,-Input.acceleration.y).normalized;
			rb.velocity =new Vector2(
				tilt.x*PV.grad.x-tilt.y*PV.grad.y,
				tilt.y*PV.grad.x+tilt.x*PV.grad.y
			)*5f;
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Water") {
			inwater_do (other);
		} 
		if (other.tag == "Obstacle"){
			obsatcle_do();
		}
		if (other.tag == "Ground_Top") {
			setgrad ((other.transform.rotation.z/180)*Mathf.PI);
		} 
		if (other.tag == "Ground_Bottom") {
			setgrad ((other.transform.rotation.z+180)/180*Mathf.PI);
		} 
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Ground") {
			land_do ();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Water") {
			inwater_not_do ();
		}

	}
	void OnCollisionExit2D(Collision2D other){
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



	void inwater_do(Collider2D other){
		PV.inwater = true;
		PV.Savepoint = other.transform;
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
	void obsatcle_do()
	{
		rb.velocity = new Vector2(-1 * rb.velocity.x, Input.acceleration.y * 20);
	}
	void setgrad(float degree){
		PV.grad= new Vector2 (Mathf.Sin (degree), Mathf.Cos (degree));
	}
}
