using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleControll : MonoBehaviour {
	private PlayerValues PV;
	private GameObject player;

	private bool initiate=false;

	public void barset(){
		PV = GameObject.FindGameObjectWithTag ("PlayerValues").GetComponent<PlayerValues> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){
		if (initiate == false)
			barset ();
		transform.position = player.transform.position;
		transform.eulerAngles = new Vector3 (0, 0, Mathf.Rad2Deg * Mathf.Atan2 (PV.velocity.y, PV.velocity.x));
		if(PV.land==false) {
			PV.showangle_on = false;
			Destroy (gameObject);
		}
	}
}
