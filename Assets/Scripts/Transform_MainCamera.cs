﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform_MainCamera : MonoBehaviour {

	private GameObject player;
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position+new Vector3(0,5,-10);
	}
}
