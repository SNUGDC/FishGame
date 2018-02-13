using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValues : MonoBehaviour {
	public bool gameover;
	public bool inwater;
	public bool land;

	public int touchcount;
	public int lefttime_x10;

	public Text left_time_text;

	public float accY_before;
	public float accY_now;
	public float accY_delta;

	public Vector2 grad;
	public Transform Savepoint;

	void Awake(){
		grad = new Vector2 (0, 0);
	}
	public void StartinWater(){
		inwater = true; // start in water
		gameover=false;
		land = false;
	}
}
