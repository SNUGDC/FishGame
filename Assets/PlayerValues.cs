using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValues : MonoBehaviour {
	public bool gameover;
	public bool inwater;
	public bool land;

	public int touchcount;
	public float lefttime;

	public Text left_time_text;

	public float accY_before;
	public float accY_now;
	public float accY_delta;

	void Awake(){
	}
}
