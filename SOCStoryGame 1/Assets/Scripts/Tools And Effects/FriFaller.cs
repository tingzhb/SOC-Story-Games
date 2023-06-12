using System;
using UnityEngine;

public class FriFaller : MonoBehaviour{
	[SerializeField] private float movementSpeed;
	[SerializeField] private GameObject safeZone, fish;
	private bool canFall = true;
	private float height;

	private void Awake(){
		height = Screen.height;
	}
	private void FixedUpdate(){
		if (canFall){
			transform.Translate(Vector3.down * (Time.deltaTime * movementSpeed * height));
			transform.Translate(Vector3.left * (Time.deltaTime * movementSpeed * height));
		}
		TrySaveFry();
	}

	private void TrySaveFry(){
		if (Math.Abs(transform.position.x - safeZone.transform.position.x) < 10 && Math.Abs(transform.position.y - fish.transform.position.y) < 75){
			ExecuteOnceMessage executeOnceMessage = new();
			Broker.InvokeSubscribers(typeof(ExecuteOnceMessage), executeOnceMessage);
			Destroy(gameObject);
		}
	}
}
