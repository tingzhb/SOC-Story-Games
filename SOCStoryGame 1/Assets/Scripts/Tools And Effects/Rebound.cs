using System;
using UnityEngine;

public class Rebound : MonoBehaviour{
	private Vector3 originalPosition;
	
	private void Start(){
		originalPosition = gameObject.transform.position;
	}

	private void OnDisable(){
		gameObject.transform.position = originalPosition;
	}
}
