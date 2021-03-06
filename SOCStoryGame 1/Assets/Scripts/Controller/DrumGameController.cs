using System;
using System.Collections;
using UnityEngine;


public class DrumGameController : MonoBehaviour{
	[SerializeField] private GameObject stickL, stickR, dog, tapL, tapR, wellDone, UI;
	[SerializeField] private int maxProgression;
	[SerializeField] private GameObject[] steps;
	private AnimateOnce animateL, animateR, animateDog;
	private int progression;
	private bool leftTurn = true;
	
	private void Start(){
		Broker.Subscribe<StickMessage>(OnStickMessageReceived);
		animateL = stickL.GetComponent<AnimateOnce>();
		animateR = stickR.GetComponent<AnimateOnce>();
		animateDog = dog.GetComponent<AnimateOnce>();
	}
	private void OnStickMessageReceived(StickMessage obj){
		if (leftTurn && obj.IsLeft){
			animateL.StartAnimation();
			leftTurn = false;
			tapL.SetActive(false);
			tapR.SetActive(true);
			progression++;
			UpdateProgress();
		}
		if (!leftTurn && !obj.IsLeft){
			animateR.StartAnimation();
			leftTurn = true;
			tapR.SetActive(false);
			tapL.SetActive(true);
			progression++;
			UpdateProgress();
		}
	}
	
	private void UpdateProgress(){
		if (progression == maxProgression){
			StartCoroutine(DelayEnd());
		}
		if (progression <= maxProgression){
			animateDog.StartAnimation();
		}
		dog.transform.position = steps[progression].transform.position;
	}
	private IEnumerator DelayEnd() {
		yield return new WaitForSeconds(0.25f);
		UI.SetActive(false);
		wellDone.SetActive(true);
	}

	private void OnDestroy(){
		Broker.Unsubscribe<StickMessage>(OnStickMessageReceived);
	}
}
