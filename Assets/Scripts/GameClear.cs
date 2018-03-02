using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour {

	float initTime;
	float duration = 3f;
	public bool isFading;
	bool canDisappear;
	public Image image;
	public Text text0;
	public Text text1;
	public Text text2;
	public Text text3;
	PlayerValues PV;
	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
	}
	void Start () {
		isFading = false;
		canDisappear = false;
		image.color = ChangeAlpha(image.color, 0);
		text0.color = ChangeAlpha(text0.color, 0);
		text1.color = ChangeAlpha(text1.color, 0);
		text2.color = ChangeAlpha(text2.color, 0);
		text3.color = ChangeAlpha(text3.color, 0);
		gameObject.SetActive(false);
	}
	
	void Update () {
		if (isFading){
			DoFade();
		}
		if(canDisappear && Input.GetMouseButtonDown(0)){
			gameObject.SetActive(false);
			FindObjectOfType<UIManager>().MoveScene("Scene_Title");
		}
	}
	public void StartFade(float d){
		if(!isFading){
			//Debug.Log("startFade : "+Time.time);
			gameObject.SetActive(true);
			initTime = Time.time;
			duration = d;
			canDisappear = false;
			text1.text = "지금까지 "+GetStringOfTime()+"초 걸렸고";
			text2.text = GetHowManyDead()+"번 죽었습니다";
			text0.color = ChangeAlpha(text0.color, 0);
			image.color = ChangeAlpha(image.color, 0);
			text1.color = ChangeAlpha(text1.color, 0);
			text2.color = ChangeAlpha(text2.color, 0);
			text3.color = ChangeAlpha(text3.color, 0);
			isFading = true;
		}
	}
	void DoFade(){
		float time = Time.time - initTime;
		if(time < duration){
			image.color = ChangeAlpha(image.color, time/duration);
			text0.color = ChangeAlpha(text0.color, time/duration);
			text1.color = ChangeAlpha(text1.color, time/duration);
			text2.color = ChangeAlpha(text2.color, time/duration);
			text3.color = ChangeAlpha(text3.color, time/duration);
		}
		else{
			image.color = ChangeAlpha(image.color, 1);
			text0.color = ChangeAlpha(text0.color, 1);
			text1.color = ChangeAlpha(text1.color, 1);
			text2.color = ChangeAlpha(text2.color, 1);
			text3.color = ChangeAlpha(text3.color, 1);

			isFading = false;
			PV.gameClear = false;
			canDisappear = true;
		}
	}
	Color ChangeAlpha(Color color, float alpha){
		return new Vector4(color.r, color.g, color.b, alpha);
	}	
	string GetStringOfTime(){
		float t = PV.Challangetime;
		int n = (int)t;
		float a = t - n;
		int b = (int)(a*100);
		return n+"."+b;
	}
	int GetHowManyDead(){
		if (PV.How_many_dead > 0) return PV.How_many_dead - 1;
		else return PV.How_many_dead;
	}
}
