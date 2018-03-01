using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRotator : MonoBehaviour {

	public float timePerRoll = 3;
	public bool isClockwise;
	PlayerValues PV;
	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
	}
	void Update () {
		if(PV.isPaused) return;
		
		if (isClockwise)
		{
			transform.Rotate(0, 0, -360 * Time.deltaTime / timePerRoll);
		}
		else
		{
			transform.Rotate(0, 0, 360 * Time.deltaTime / timePerRoll);
		}
	}
}
