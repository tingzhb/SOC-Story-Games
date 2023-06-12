using System;
using System.Collections;
using UnityEngine;

public class LoopDragController : MonoBehaviour{
	[SerializeField] private GameObject[] bounds;
	[SerializeField] private GameObject drag, direction;
	private bool completed;
	private int turn;
	private AnimateOnce animateOnce;
	private void Awake(){
		animateOnce = GetComponentInChildren<AnimateOnce>();
	}
	
	private void FixedUpdate(){
		if (!completed){
			CheckLoop();
		}
	}
	private void CheckLoop(){
		if (turn == 0 && Math.Abs(drag.transform.position.y - bounds[0].transform.position.y) < 30f){
			turn++;
		}
		if (turn == 1 && Math.Abs(drag.transform.position.x - bounds[1].transform.position.x) < 30f){
			turn++;
		}
		if (turn == 2 && Math.Abs(drag.transform.position.y - bounds[2].transform.position.y) < 30f){
			turn++;
		}
		if (turn == 3 && Math.Abs(drag.transform.position.x - bounds[3].transform.position.x) < 30f){
			turn++;
		}
		if (turn == 4 && Math.Abs(drag.transform.position.y - bounds[4].transform.position.y) < 30f){
			turn++;
			CheckSuccess();
		}
		
	}

	private void CheckSuccess(){
		completed = true;
		direction.SetActive(false);
		SoundMessage soundMessage = new(){
			SoundType = 7
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		animateOnce.StartAnimation();
		animateOnce.canAnimate = false;
		StartCoroutine(Delay());
	}

	private IEnumerator Delay(){
		yield return new WaitForSeconds(2f);
		ExecuteOnceMessage executeOnceMessage = new();
		Broker.InvokeSubscribers(typeof(ExecuteOnceMessage), executeOnceMessage);
	}
}