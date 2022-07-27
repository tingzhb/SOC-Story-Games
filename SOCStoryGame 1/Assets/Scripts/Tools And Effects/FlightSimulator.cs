using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlightSimulator : MonoBehaviour{
	[SerializeField] private float movementSpeed, objectWidth, objectHeight, interval;
	[SerializeField] private GameObject plane;
	[SerializeField] private bool vertical, up, deadly, sensor;
	private float height, width;
	private Executor executor;
	private BoxCollider2D boxCollider;

	private void Awake(){
		executor = FindObjectOfType<Executor>();
		boxCollider = GetComponent<BoxCollider2D>();
		height = Screen.height;
		width = Screen.width;
	}
	
	private void FixedUpdate(){
		if (deadly){
			TryKillPlane();
		}
		transform.Translate(Vector3.left * (Time.deltaTime * movementSpeed * width));
		if (vertical){
			if (up){
				transform.Translate(Vector3.down * (Time.deltaTime * movementSpeed * height));
				StartCoroutine(MoveUp());

			} else {
				transform.Translate(Vector3.up * (Time.deltaTime * movementSpeed * height));
				StartCoroutine(MoveDown());
			}
		}
		if (sensor && Math.Abs(transform.position.x - plane.transform.position.x) < objectWidth){
			sensor = false;
			executor.Enqueue(new CorrectCommand());
		}
	}

	private void TryKillPlane(){
		if (boxCollider.bounds.Contains(plane.transform.position)){
			deadly = false;
			executor.Enqueue(new FailureCommand());
		}
	}

	private IEnumerator MoveUp(){
		yield return new WaitForSeconds(interval);
		up = false;
	}
	private IEnumerator MoveDown(){
		yield return new WaitForSeconds(interval);
		up = true;
	}
}