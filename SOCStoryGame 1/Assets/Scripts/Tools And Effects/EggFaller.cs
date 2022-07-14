using System;
using UnityEngine;

public class EggFaller : MonoBehaviour{
	[SerializeField] private int movementSpeed;
	[SerializeField] private GameObject killZone;
	private bool canFall = true, canAnimate = true;
	private AnimateOnce animateOnce;
	private void Start(){
		animateOnce = gameObject.GetComponentInChildren<AnimateOnce>();
	}
	private void FixedUpdate(){
		if (canFall){
			transform.Translate(Vector3.down * (Time.deltaTime * movementSpeed));
		}
		if (transform.position.y <= killZone.transform.position.y){
			canFall = false;
			if (canAnimate){
				animateOnce.StartAnimation();
				canAnimate = false;
			}
		}
	}
}
