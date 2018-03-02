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
	public Image image0;
	public Text text1;
	public Text text2;
	void Start () {
		isFading = false;
		canDisappear = false;
		image.color = ChangeAlpha(image.color, 0);
		image0.color = ChangeAlpha(image0.color, 0);
		text1.color = ChangeAlpha(text1.color, 0);
		text2.color = ChangeAlpha(text2.color, 0);
		gameObject.SetActive(false);
	}
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
			//Debug.Log("startFade : "+Time.time);
			gameObject.SetActive(true);
			initTime = Time.time;
			duration = d;
			canDisappear = false;
			text1.text = "YOU ARE \n"+deadBy.ToString();
			image.color = ChangeAlpha(image.color, 0);
			image0.color = ChangeAlpha(image0.color, 0);
			text1.color = ChangeAlpha(text1.color, 0);
			text2.color = ChangeAlpha(text2.color, 0);
			isFading = true;
		}
	}
	void DoFade(){
		float time = Time.time - initTime;
		if(time < duration){
			image.color = ChangeAlpha(image.color, time/duration);
			image0.color = ChangeAlpha(image0.color, time/duration);
			text1.color = ChangeAlpha(text1.color, time/duration);
		}
		else{
			image.color = ChangeAlpha(image.color, 1);
			image0.color = ChangeAlpha(image0.color, 1);
			text1.color = ChangeAlpha(text1.color, 1);
			text2.color = ChangeAlpha(text2.color, 1);

			isFading = false;
			canDisappear = true;
		}
	}
	Color ChangeAlpha(Color color, float alpha){
		return new Vector4(color.r, color.g, color.b, alpha);
	}
}
