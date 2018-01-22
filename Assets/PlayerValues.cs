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

	void Awake(){
	}
}
