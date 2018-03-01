using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Player : MonoBehaviour {
	public GameObject healthy0;
	public GameObject healthy1;
	public GameObject healthy2;
	public GameObject healthy3;

	public void Activate_PlayerSprite(int num){
		healthy0.SetActive(false);
		healthy1.SetActive(false);
		healthy2.SetActive(false);
		healthy3.SetActive(false);
		switch(num){
		case 0:
			healthy0.SetActive(true);
			break;
		case 1:
			healthy1.SetActive(true);
			break;
		case 2:
			healthy2.SetActive(true);
			break;
		case 3:
			healthy3.SetActive(true);
			break;
		}
	}

}
