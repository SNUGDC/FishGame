using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private PlayerValues PV;
	private VFX_Player VFXP;
	private GameObject Player;
	public GameObject gameOverUI;
	public Coroutine TimeCounter;
	void Awake(){
		PV =GameObject.FindGameObjectWithTag("PlayerValues").GetComponent<PlayerValues> ();
		Player = GameObject.FindGameObjectWithTag ("Player");
		VFXP = Player.GetComponent<VFX_Player> ();
	}
	void Start(){
		hidelefttime ();
	}
	void Update(){
		float timenow = Time.time;





		if (PV.gameover==true && !gameOverUI.GetComponent<GameOver>().isFading){
			gameover ();
		}
		PV.Challangetime += Time.deltaTime;
	}
	void gameover(){
		Debug.Log("gameover : "+Time.time);
		StartCoroutine(RestartAfterDuration(3f));
	}
	IEnumerator RestartAfterDuration(float duration){
		PV.How_many_dead++;
		
		gameOverUI.GetComponent<GameOver>().StartFade(duration, PV.deadBy);
		yield return new WaitForSeconds(3f);
		VFXP.Activate_PlayerSprite (0);
		Player.transform.position = PV.Savepoint.position;
		Player.transform.rotation = Quaternion.identity;
		Player.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
		Player.GetComponent<Rigidbody2D> ().angularVelocity = 0;
		PV.StartinWater ();

	}

	public void showlefttime(){

		PV.left_time_text.text = "Time Left : " + PV.time_left;
	}
	public void hidelefttime(){
		PV.left_time_text.text = "";
	}

	public void set_left_time(){
		PV.time_left =PV.time_limit ;
		TimeCounter=StartCoroutine (timecounter());
	}
	IEnumerator timecounter(){
		
		while (!PV.inwater&&!PV.gameover) {
			showlefttime ();
			yield return new WaitForSeconds(1f);

			if (!PV.isPaused) PV.time_left -=1;
			
			if (PV.time_left < 40)
				VFXP.Activate_PlayerSprite (1);
			if (PV.time_left < 20)
				VFXP.Activate_PlayerSprite (2);
			if (PV.time_left < 10)
				VFXP.Activate_PlayerSprite (3);
			if (PV.time_left <= 0) {
				Debug.Log ("gameover");
				PV.gameover = true;
				PV.deadBy = DeadBy.DRIED;
				break;
			}
		}

		hidelefttime ();
	}
}