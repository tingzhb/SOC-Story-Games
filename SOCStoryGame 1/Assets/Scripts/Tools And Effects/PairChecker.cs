using System.Collections;
using UnityEngine;

public class PairChecker : MonoBehaviour{
	[SerializeField] private GameObject wellDone;
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
					SendCorrectMessage();
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
			wellDone.SetActive(true);
			StartCoroutine(DelayEnd());
		}
	}

	private void SendCorrectMessage(){
		progress++;
		CorrectMessage correctMessage = new(){
			Name = previousCardName
		};
		Broker.InvokeSubscribers(typeof(CorrectMessage), correctMessage);
	}

	private IEnumerator DelayEnd(){
		yield return new WaitForSeconds(0.5f);
		executor.Enqueue(new ValidAnswerCommand());

	}

	private void OnDestroy(){
		Broker.Unsubscribe<CardMessage>(OnCardMessageReceived);
	}
}
