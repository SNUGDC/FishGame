using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private PlayerValues PV;
	private Rigidbody2D rb;
	private GameController GC;
	public GameObject SA;
	public VFX_Player VFXP;
	public float snapBoundary = 0.2f;
	float coeffOfV = 1;
	Vector2 savedVelocity;
	float savedAngularVelocity;
	bool isPaused;
	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		GC = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		VFXP = gameObject.GetComponent<VFX_Player>();
		PV.StartinWater ();
	}// get components
	void Start(){
	}//initialize
	void Update () {
		if (PV.isPaused && !isPaused) {
			isPaused = true;
			savedVelocity = rb.velocity;
			savedAngularVelocity = rb.angularVelocity;
			Time.timeScale = 0.0f;
			rb.simulated = false;
			Debug.Log("Paused");
			return;
		}
		else if (!PV.isPaused && isPaused){
			Time.timeScale = 1.0f;
			rb.simulated = true;
			rb.velocity = savedVelocity;
			rb.angularVelocity = savedAngularVelocity;
			isPaused = false;
			Debug.Log("Unpaused");
		}
		Vector2 tilt=new Vector2(Input.acceleration.x,-Input.acceleration.y).normalized;
		tilt.y=tilt.y>0?tilt.y:-tilt.y;
		PV.velocity=new Vector2(
			tilt.x*PV.grad.x-tilt.y*PV.grad.y,
			tilt.y*PV.grad.x+tilt.x*PV.grad.y
		)*PV.speed;

		PV.touchcount = Input.touchCount;
		if (PV.land&&PV.showangle_on==false) {
			Instantiate(SA,transform.position, transform.rotation);
			PV.showangle_on = true;
		}
		if (PV.inwater==true||PV.land==true) {
			
			if(PV.touchcount!=0 && !PV.gameover){
				rb.velocity = coeffOfV * PV.velocity;
				coeffOfV = 1;
			}
		}
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
			}
			else if (other.transform.parent.tag == "Ground_Super"){
				coeffOfV = Mathf.Sqrt(2);
				land_do ();
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.transform.parent != null) {
			if (other.transform.parent.tag == "Ground")
				land_do ();
			if (other.tag == "Ground_Top") {
				setgrad ((other.transform.parent.transform.eulerAngles.z / 180) * Mathf.PI);
			} 
			if (other.tag == "Ground_Bottom") {
				setgrad ((other.transform.parent.transform.eulerAngles.z + 180) / 180 * Mathf.PI);
			} 
			if (other.tag == "Ground_60") {
				setgrad ((other.transform.parent.transform.eulerAngles.z + 60) / 180 * Mathf.PI);
			} 
			if (other.tag == "Ground_-60") {
				setgrad ((other.transform.parent.transform.eulerAngles.z - 60) / 180 * Mathf.PI);
			} 
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Water") {
			inwater_not_do ();
		}
	    if (other.transform.parent!=null) {
			if(other.transform.parent.tag=="Ground")
			land_not_do ();
			else if(other.transform.parent.tag=="Ground_Super")
			land_not_do ();
		}

	}
		
	void inwater_do(Collider2D other){
		PV.inwater = true;
		PV.Savepoint = other.transform;
		PV.resetgrad ();
		StopCoroutine (GC.TimeCounter);
		GC.hidelefttime();
		VFXP.Activate_PlayerSprite (0);
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
		Debug.Log ("Setgrad called"+PV.grad.x+","+PV.grad.y);
	}
	void Dead_do(){
		PV.gameover = true;
		PV.deadBy = DeadBy.COOKED;
	}
}
