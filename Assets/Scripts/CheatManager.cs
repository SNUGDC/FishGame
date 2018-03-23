using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {

	GameObject player;
	int turnOnNum;
	void Start () {
		gameObject.SetActive(false);
		turnOnNum = 0;
		player = GameObject.FindGameObjectWithTag("Player");
	}	
	public void SwitchActive(){
		if(!gameObject.active) {
			turnOnNum++;
			if (turnOnNum >= 3){
				gameObject.SetActive(true);
			}
		} else {
			gameObject.SetActive(false);
			turnOnNum = 0;
		}
	}
	public void MovePlayer(int magicNum){
		float x, y;
		switch (magicNum){
			case 1:{x=11;y=18;break;}
			case 2:{x=6;y=33;break;}
			case 3:{x=60;y=63;break;}
			case 4:{x=-22;y=75;break;}
			case 5:{x=44;y=117;break;}
			case 6:{x=-34;y=150;break;}
			default:{x=0;y=0;break;}
		}
		player.transform.position = new Vector3(x, y, player.transform.position.z);
		SwitchActive();
	}
}
