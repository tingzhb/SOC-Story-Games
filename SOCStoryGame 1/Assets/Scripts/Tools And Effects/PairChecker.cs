using System;
using System.Collections;
using UnityEngine;

public class PairChecker : MonoBehaviour{
	private string previousCardName, newCardName;
	private int progress, turn;
	private Executor executor;
	private void Start(){
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<CardMessage>(OnCardMessageReceived);
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
					progress++;
					Debug.Log(progress);
					CorrectMessage correctMessage = new(){
						Name = previousCardName
					};
					Broker.InvokeSubscribers(typeof(CorrectMessage), correctMessage);
					if (progress == 6){
						executor.Enqueue(new ValidAnswerCommand());
					}
				} else {
					StartCoroutine(DelayHide());
				}
				break;
		}
	}
	
	private IEnumerator DelayHide(){
		yield return new WaitForSeconds(0.75f);
		InvalidMessage invalidMessage = new(){
			Type = newCardName
		};
		Broker.InvokeSubscribers(typeof(InvalidMessage), invalidMessage);
		InvalidMessage invalidMessage2 = new(){
			Type = previousCardName
		};
		Broker.InvokeSubscribers(typeof(InvalidMessage), invalidMessage2);
	}
}
