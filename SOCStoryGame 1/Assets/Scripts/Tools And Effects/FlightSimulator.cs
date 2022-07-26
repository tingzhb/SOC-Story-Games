using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlightSimulator : MonoBehaviour{
	[SerializeField] private float movementSpeed, objectWidth, objectHeight, interval;
	[SerializeField] private GameObject plane;
	[SerializeField] private bool vertical, up, deadly, sensor;
	private float height;
	private Executor executor;

	private void Awake(){
		height = Screen.height;
	}
	private void Start(){
		executor = FindObjectOfType<Executor>();
	}

	private void FixedUpdate(){
		if (deadly){
			TryKillPlane();
		}
		transform.Translate(Vector3.left * (Time.deltaTime * movementSpeed * height));
		if (vertical){
			if (up){
				transform.Translate(Vector3.down * (Time.deltaTime * movementSpeed * height));
				StartCoroutine(MoveUp());

			} else {
				transform.Translate(Vector3.up * (Time.deltaTime * movementSpeed * height));
				StartCoroutine(MoveDown());
			}
		}
		if (sensor && Math.Abs(transform.position.x - plane.transform.position.x) < objectHeight && Math.Abs(transform.position.x - plane.transform.position.x) < objectWidth){
			sensor = false;
			executor.Enqueue(new CorrectCommand());
		}
	}

	private void TryKillPlane(){
		if (Math.Abs(transform.position.y - plane.transform.position.y) < objectHeight && Math.Abs(transform.position.x - plane.transform.position.x) < objectWidth){
			executor.Enqueue(new FailureCommand());
			deadly = false;
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