using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour {
	ScreenOrientation initial;
	void Awake () {
		initial = Screen.orientation;
		Screen.orientation = initial;
	}

}
