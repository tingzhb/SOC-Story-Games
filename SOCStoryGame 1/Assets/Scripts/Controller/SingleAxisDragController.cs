using System;
using System.Collections;
using UnityEngine;

public class SingleAxisDragController : MonoBehaviour{
	[SerializeField] private GameObject[] bounds;
	[SerializeField] private GameObject drag;
	[SerializeField] private bool vertical;
	private int goal;
	private bool turn, canAnimate;
	private AnimateOnce animateOnce;
	private void Awake(){
		animateOnce = GetComponentInChildren<AnimateOnce>();
	}
	private void Update(){
		if (vertical){
			if (turn && Math.Abs(drag.transform.position.y - bounds[0].transform.position.y) < 50f){
				goal++;
				Debug.Log(goal);
				turn = true;
			}
			if (!turn && Math.Abs(drag.transform.position.y - bounds[1].transform.position.y) < 50f){
				goal++;
				Debug.Log(goal);
				turn = false;
			}
		}
		else{
			if (turn && Math.Abs(drag.transform.position.x - bounds[0].transform.position.x) < 50f){
				goal++;
				Debug.Log(goal);
				turn = true;
			}
			if (!turn && Math.Abs(drag.transform.position.x - bounds[1].transform.position.x) < 50f){
				goal++;
				Debug.Log(goal);
				turn = false;
			}
		}
		if (goal == 5){
			Debug.Log("win");
			goal = 0;
			turn = false;
			if (canAnimate){
				animateOnce.StartAnimation();
				canAnimate = false;
			}
			StartCoroutine(Delay());
		}
	}

	private IEnumerator Delay(){
		yield return new WaitForSeconds(2f);
		CorrectMessage correctMessage = new();
		Broker.InvokeSubscribers(typeof(CorrectMessage), correctMessage);
		canAnimate = true;
	}
}
