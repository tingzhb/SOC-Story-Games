using System;
using System.Collections;
using UnityEngine;

public class LoopDragController : MonoBehaviour{
	[SerializeField] private GameObject[] bounds;
	[SerializeField] private GameObject drag;
	private int turn;
	private AnimateOnce animateOnce;
	private void Awake(){
		animateOnce = GetComponentInChildren<AnimateOnce>();
	}
	
	private void FixedUpdate(){
		CheckLoop();
	}
	private void CheckLoop(){
		if (turn == 0 && Math.Abs(drag.transform.position.x - bounds[0].transform.position.x) < 50f){
			Debug.Log(turn);
			turn++;
			DisplaySuccess();
		}
		if (turn == 1 && Math.Abs(drag.transform.position.y - bounds[1].transform.position.y) < 50f){
			Debug.Log(turn);
			turn++;
			DisplaySuccess();
		}
		if (turn == 2 && Math.Abs(drag.transform.position.x - bounds[1].transform.position.x) < 50f){
			Debug.Log(turn);
			turn++;
			DisplaySuccess();
		}
		if (turn == 3 && Math.Abs(drag.transform.position.y - bounds[1].transform.position.y) < 50f){
			Debug.Log(turn);
			turn++;
			DisplaySuccess();
		}
		
	}

	private void DisplaySuccess(){
		if (turn == 4){
			Debug.Log("win");
			turn = 0;
			animateOnce.StartAnimation();
			animateOnce.canAnimate = false;
			StartCoroutine(Delay());
		}
	}

	private IEnumerator Delay(){
		yield return new WaitForSeconds(2f);
		CorrectMessage correctMessage = new();
		Broker.InvokeSubscribers(typeof(CorrectMessage), correctMessage);
	}

	private void OnDisable(){
		turn = 0;
		animateOnce.canAnimate = true;
	}
}