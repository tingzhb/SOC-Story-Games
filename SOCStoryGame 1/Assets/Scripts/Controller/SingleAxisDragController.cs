using System;
using System.Collections;
using UnityEngine;

public class SingleAxisDragController : MonoBehaviour{
	[SerializeField] private GameObject[] bounds;
	[SerializeField] private GameObject drag, direction;
	[SerializeField] private bool vertical;
	private int goal;
	private bool turn, completed;
	private AnimateOnce animateOnce;
	private void Awake(){
		animateOnce = GetComponentInChildren<AnimateOnce>();
	}
	
	private void FixedUpdate(){
		if (!completed && vertical){
			CheckVertical();
		}
		else if(!completed) {
			CheckHorizontal();
		}
	}
	private void CheckHorizontal(){
		if (turn && Math.Abs(drag.transform.position.x - bounds[0].transform.position.x) < 50f){
			goal++;
			turn = true;
			CheckSuccess();
		}
		if (!turn && Math.Abs(drag.transform.position.x - bounds[1].transform.position.x) < 50f){
			goal++;
			turn = false;
			CheckSuccess();
		}
	}
	private void CheckVertical(){
		if (turn && Math.Abs(drag.transform.position.y - bounds[0].transform.position.y) < 50f){
			goal++;
			turn = true;
			CheckSuccess();
		}
		if (!turn && Math.Abs(drag.transform.position.y - bounds[1].transform.position.y) < 50f){
			goal++;
			turn = false;
			CheckSuccess();
		}
	}
	
	private void CheckSuccess(){
		if (goal == 3){
			completed = true;
			direction.SetActive(false);
			SoundMessage soundMessage = new(){
				SoundType = 7
			};
			Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
			turn = false;
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
}
