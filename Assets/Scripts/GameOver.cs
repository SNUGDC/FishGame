using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DeadBy { DRIED, COOKED }

public class GameOver : MonoBehaviour {

	float initTime;
	float duration = 3f;
	public bool isFading;
	bool canDisappear;
	public Image image;
	public Text text1;
	public Text text2;
	void Start () {
		isFading = false;
		canDisappear = false;
		image.color = new Vector4(0,0,0,0);
		text1.color = new Vector4(0.75f,0,0,0);
		text2.color = new Vector4(0.75f,0.75f,0.75f,0);
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isFading){
			DoFade();
		}
		if(canDisappear && Input.GetMouseButtonDown(0)){
			gameObject.SetActive(false);
			canDisappear = false;
		}
	}
	public void StartFade(float d, DeadBy deadBy){
		if(!isFading){
			Debug.Log("startFade : "+Time.time);
			gameObject.SetActive(true);
			initTime = Time.time;
			duration = d;
			canDisappear = false;
			text1.text = "YOU "+deadBy.ToString();
			image.color = new Vector4(0,0,0,0);
			text1.color = new Vector4(0.75f,0,0,0);
			text2.color = new Vector4(0.75f,0.75f,0.75f,0);
			isFading = true;
		}
	}
	void DoFade(){
		float time = Time.time - initTime;
		if(time < duration){
			image.color = new Vector4(0,0,0,time/duration);
			text1.color = new Vector4(0.75f,0,0,time/duration);
			text2.color = new Vector4(0.75f,0.75f,0.75f,time/duration);
		}
		else{
			isFading = false;
			canDisappear = true;
		}
	}
	
}
