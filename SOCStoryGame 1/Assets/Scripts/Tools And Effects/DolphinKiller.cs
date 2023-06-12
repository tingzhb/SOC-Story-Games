using System.Collections;
using UnityEngine;

public class DolphinKiller : MonoBehaviour {
	private void Awake(){
		Broker.Subscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
	}
	private void OnDisable(){
		Broker.Unsubscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
		Broker.Unsubscribe<FailureMessage>(OnFailureMessageReceived);
	}
	private void OnFailureMessageReceived(FailureMessage obj){
		StartCoroutine(DelayKill());
	}
	private void OnExecuteOnceMessageReceived(ExecuteOnceMessage obj){
		StartCoroutine(DelayKill());
	}

	private IEnumerator DelayKill(){
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
