using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleRandomizer : MonoBehaviour{
	[SerializeField] private char[] characters;
	[SerializeField] private float movementSpeed;
	private void Awake(){
		var randomChar = Random.Range(0, characters.Length);
		GetComponentInChildren<TextMeshProUGUI>().text = characters[randomChar].ToString();
		if (randomChar == 1){
			gameObject.tag = "Bubble";
		}
	}
	private void FixedUpdate(){
		transform.Translate(Vector3.up * (Time.deltaTime * (movementSpeed + RandomDisplacement())));
		transform.Translate(Vector3.left * (Time.deltaTime * (movementSpeed + RandomDisplacement())));
	}
	private int RandomDisplacement(){
		return Random.Range(-40, 40);
	}
}
