using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rebound : MonoBehaviour{
	private Vector3 originalPosition;

	private void Start(){
		originalPosition = gameObject.transform.position;
	}
	private void Update(){
		gameObject.transform.position = originalPosition;
	}
}
