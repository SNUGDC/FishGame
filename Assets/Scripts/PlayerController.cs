using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private PlayerValues PV;
	private Rigidbody2D rb;
	private GameController GC;
	public GameObject SA;
	public float snapBoundary = 0.2f;
	int coeffOfV = 1;
	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		GC = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		PV.StartinWater ();
	}// get components
	void Start(){
	   	PV.accY_now = Input.acceleration.y;
	}//initialize
	void Update () {
		Vector2 tilt=new Vector2(Input.acceleration.x,-Input.acceleration.y).normalized;
		PV.velocity=new Vector2(
			tilt.x*PV.grad.x-tilt.y*PV.grad.y,
			tilt.y*PV.grad.x+tilt.x*PV.grad.y
		)*PV.speed;

		PV.touchcount = Input.touchCount;
		Debug.Log(Input.acceleration.x + ", "+Input.acceleration.y + ", " + Input.acceleration.z);


		if (PV.land&&PV.showangle_on==false) {
			Instantiate(SA,transform.position, transform.rotation);
			PV.showangle_on = true;
		}
		if (PV.inwater==true||PV.land==true) {
			
			if(PV.touchcount!=0)
			rb.velocity = coeffOfV * PV.velocity;
		}

		/*
		if (PV.inwater == true || PV.land == true) {
			if (Input.GetKeyDown ("d")) {
				Debug.Log ("d pressed");
				Vector2 tilt = new Vector2 (1, 1).normalized;
				rb.velocity = new Vector2 (
					tilt.x * PV.grad.x - tilt.y * PV.grad.y,
					tilt.y * PV.grad.x + tilt.x * PV.grad.y
				) * 10f;
			}
			if (Input.GetKeyDown ("a")) {
				Vector2 tilt = new Vector2 (-1, 1).normalized;
				rb.velocity = new Vector2 (
					tilt.x * PV.grad.x - tilt.y * PV.grad.y,
					tilt.y * PV.grad.x + tilt.x * PV.grad.y
				) * 10f;
			}
		}
		*/
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Water") {
			inwater_do (other);
		} 
		if (other.tag == "Obstacle"){
			obstacle_do();
		}
		if (other.tag == "Dead") {
			Dead_do ();
		}
		if (other.transform.parent != null) {
			if (other.transform.parent.tag == "Ground") {
				land_do ();
				if(other.tag == "Ground_Super"){
					coeffOfV = 2;
				}
				else {
					coeffOfV = 1;
				}

				if (other.tag == "Ground_Top") {
					setgrad ((other.transform.eulerAngles.z / 180) * Mathf.PI);
				} 
				if (other.tag == "Ground_Bottom") {
					setgrad ((other.transform.eulerAngles.z + 180) / 180 * Mathf.PI);
				} 
				if (other.tag == "Ground_60") {
					setgrad ((other.transform.eulerAngles.z + 60) / 180 * Mathf.PI);
				} 
				if (other.tag == "Ground_-60") {
					setgrad ((other.transform.eulerAngles.z - 60) / 180 * Mathf.PI);
				} 
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.transform.parent != null) {
			if (other.transform.parent.tag == "Ground")
				land_do ();
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Water") {
			inwater_not_do ();
		}
	    if (other.transform.parent!=null) {
			if(other.transform.parent.tag=="Ground")
			land_not_do ();
		}

	}
		
	void inwater_do(Collider2D other){
		PV.inwater = true;
		PV.Savepoint = other.transform;
		PV.resetgrad ();
	}
	void inwater_not_do(){
		PV.inwater = false;
 		GC.set_left_time ();
	}
	void land_do (){
		Debug.Log ("land_do called");
		PV.land = true;
	}
	void land_not_do(){
		Debug.Log ("land_not_do called");
		PV.land = false;
	}
	void obstacle_do()
	{
		Debug.Log ("obstacle_do called");
		rb.velocity = new Vector2(-1 * rb.velocity.x, Input.acceleration.y * 20);
		Debug.Log ("obstacle_do called2");
	}
	void setgrad(float degree){
		PV.grad= new Vector2 (Mathf.Cos (degree), Mathf.Sin (degree));
		PV.angle_ground = degree;
	}
	void Dead_do(){
	}
}
