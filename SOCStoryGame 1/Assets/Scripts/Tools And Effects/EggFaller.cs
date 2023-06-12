using System;
using UnityEngine;

public class EggFaller : MonoBehaviour{
	[SerializeField] private float movementSpeed;
	[SerializeField] private GameObject killZone, safeZone, olle;
	private bool canFall = true, canAnimate = true;
	private AnimateOnce animateOnce;
	private float height;

	private void Awake(){
		height = Screen.height;
		animateOnce = gameObject.GetComponentInChildren<AnimateOnce>();
	}
	private void FixedUpdate(){
		if (canFall){
			transform.Translate(Vector3.down * (Time.deltaTime * movementSpeed * height));
		}
		TryKillEgg();
		TrySaveEgg();
	}
	private void TryKillEgg(){
		if (transform.position.y <= killZone.transform.position.y){
			canFall = false;

			if (canAnimate){
				animateOnce.StartAnimation();
				canAnimate = false;
				EggMessage eggMessage = new(){
					Saved = false
				};
				Broker.InvokeSubscribers(typeof(EggMessage), eggMessage);
			}
		}
	}
	private void TrySaveEgg(){
		if (Math.Abs(transform.position.y - safeZone.transform.position.y) < 10 && Math.Abs(transform.position.x - olle.transform.position.x) < 50){
			EggMessage eggMessage = new(){
				Saved = true
			};
			Broker.InvokeSubscribers(typeof(EggMessage), eggMessage);
			Destroy(gameObject);
		}
	}
}
