using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValues : MonoBehaviour {
	public bool gameover;
	public bool inwater;
	public bool land;
	public bool showangle_on;

	public int touchcount;
	public int time_limit;
	public int time_left_x100;
	public int How_many_dead=0;

	public Text left_time_text;

	public float speed;


	public Vector2 grad;
	public Vector2 velocity;

	public Transform Savepoint;


	public void StartinWater(){
		resetgrad ();
		inwater = true; // start in water
		gameover=false;
		land = false;
		showangle_on = false;
	}
	public void resetgrad(){
		grad = new Vector2 (1, 0);
	}
}
