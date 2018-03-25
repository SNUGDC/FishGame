using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShuttleController : MonoBehaviour {

	public enum ValueType {Period, Speed}
	public Transform start;
	public Transform end;
	public ValueType useValueForType;
	public float value;
	public bool isLoop = true;

	Vector3 initPos, finalPos, delta;
	float timer;
	PlayerValues PV;

	void Awake() {
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
	}
	void Start () {
		initPos = start.position;
		finalPos = end.position;
		transform.position = initPos;
	}
	
	void Update () {
		if(PV.isPaused) return; 
		
		if(useValueForType == ValueType.Speed){
			float period = Vector3.Distance(finalPos, initPos)/value;
			UseValueForPeriod(period);
		}
		else if(useValueForType == ValueType.Period) UseValueForPeriod(value);
	}
	float GetTime()
	{
		return Time.time - timer;
	}
	void UseValueForPeriod(float value)
	{
		float t; 
		if(isLoop){
			if(GetTime() < value / 2) t = 2 * GetTime() / value;
			else if(GetTime() < value) t = 2 - 2 * GetTime() / value;
			else{
				timer += value;
				t = 2 * GetTime() / value;
			}
		}
		else{
			if(GetTime() > value)	timer += value;
			t = GetTime() / value;
		}
		delta = transform.position;
		transform.position = (1 - t) * initPos + t * finalPos;
		delta = transform.position - delta;
	}
	void OnCollisionStay(Collision other){
		if(other.gameObject.tag == "Player"){
			other.transform.position += delta;
		}
	}
}
