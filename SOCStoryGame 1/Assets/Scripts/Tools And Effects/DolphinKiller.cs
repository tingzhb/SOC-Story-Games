using System.Collections;
using UnityEngine;

public class DolphinKiller : MonoBehaviour {
	private void Awake(){
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
	}
	private void OnDisable(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
		Broker.Unsubscribe<FailureMessage>(OnFailureMessageReceived);
	}
	private void OnFailureMessageReceived(FailureMessage obj){
		StartCoroutine(DelayKill());
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		StartCoroutine(DelayKill());
	}

	private IEnumerator DelayKill(){
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
