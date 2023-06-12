using System;
using System.Collections;
using UnityEngine;

public class PairChecker : MonoBehaviour{
	[SerializeField] private GameObject wellDone;
	private string previousCardName, newCardName;
	private int progress, turn;
	[SerializeField] private float delay;
	private void Awake(){
		Broker.Subscribe<CardMessage>(OnCardMessageReceived);
	}
	void OnDisable(){
		Broker.Unsubscribe<CardMessage>(OnCardMessageReceived);
	}
	private void OnCardMessageReceived(CardMessage obj){
		switch (turn){
			case 0:
				previousCardName = obj.CardName;
				turn++;
				break;
			
			case 1:
				turn--;
				newCardName = obj.CardName;
				if (previousCardName == newCardName){
					SendExecuteOnceMessage();
					CheckForCompletion();
				} else{
					HideCards();
				}
				break;
		}
	}
	private void HideCards(){
		InvalidMessage invalidMessage = new(){
			Type = newCardName
		};
		Broker.InvokeSubscribers(typeof(InvalidMessage), invalidMessage);
		InvalidMessage invalidMessage2 = new(){
			Type = previousCardName
		};
		Broker.InvokeSubscribers(typeof(InvalidMessage), invalidMessage2);
	}
	
	private void CheckForCompletion(){
		if (progress == 6){
			StartCoroutine(DelayWellDone());
		}
	}

	private IEnumerator DelayWellDone(){
		yield return new WaitForSeconds(delay);
		wellDone.SetActive(true);
		StartCoroutine(DelayEnd());

	}

	private void SendExecuteOnceMessage(){
		progress++;
		ExecuteOnceMessage executeOnceMessage = new(){
			Name = previousCardName
		};
		Broker.InvokeSubscribers(typeof(ExecuteOnceMessage), executeOnceMessage);
	}

	private IEnumerator DelayEnd(){
		wellDone.SetActive(true);
		yield return new WaitForSeconds(3f);
		SuccessMessage successMessage = new() {};
		Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);
	}
}
