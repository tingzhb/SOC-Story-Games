using System;
using System.Collections;
using UnityEngine;

public class SingleAxisDragController : MonoBehaviour{
	[SerializeField] private GameObject[] bounds;
	[SerializeField] private GameObject drag;
	[SerializeField] private bool vertical;
	private int goal;
	private bool turn;
	private AnimateOnce animateOnce;
	private void Awake(){
		animateOnce = GetComponentInChildren<AnimateOnce>();
	}
	
	private void FixedUpdate(){
		if (vertical){
			CheckVertical();
		}
		else {
			CheckHorizontal();
		}
	}
	private void CheckHorizontal(){
		if (turn && Math.Abs(drag.transform.position.x - bounds[0].transform.position.x) < 50f){
			goal++;
			turn = true;
			DisplaySuccess();
		}
		if (!turn && Math.Abs(drag.transform.position.x - bounds[1].transform.position.x) < 50f){
			goal++;
			turn = false;
			DisplaySuccess();
		}
	}
	private void CheckVertical(){
		if (turn && Math.Abs(drag.transform.position.y - bounds[0].transform.position.y) < 50f){
			goal++;
			turn = true;
			DisplaySuccess();
		}
		if (!turn && Math.Abs(drag.transform.position.y - bounds[1].transform.position.y) < 50f){
			goal++;
			turn = false;
			DisplaySuccess();
		}
	}
	
	private void DisplaySuccess(){
		if (goal == 3){
			SoundMessage soundMessage = new(){
				SoundType = 7
			};
			Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
			turn = false;
			animateOnce.StartAnimation();
			animateOnce.canAnimate = false;
			goal = 0;
			StartCoroutine(Delay());
		}
	}

	private IEnumerator Delay(){
		yield return new WaitForSeconds(3f);
		CorrectMessage correctMessage = new();
		Broker.InvokeSubscribers(typeof(CorrectMessage), correctMessage);
	}

	private void OnDisable(){
		goal = 0;
		animateOnce.canAnimate = true;
	}
}
