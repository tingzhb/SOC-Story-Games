using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureController : MonoBehaviour{
	private float touchDistance;
	private void Update(){
		if (Input.touchCount > 0){
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Moved){
				
			}
		}
	}
}
