using System.Collections;
using UnityEngine;

public class DragDropGameController : MonoBehaviour {

	private int itemCount;
	private Executor executor;

	private void Start(){
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<InPlaceMessage>(OnInPlaceMessageReceived);
	}
	private void OnInPlaceMessageReceived(InPlaceMessage obj){
		itemCount++;
		if (itemCount == 17){
			StartCoroutine(DelayEnd());
		}
	}
	
	private IEnumerator DelayEnd() {
		yield return new WaitForSeconds(1.5f);
		executor.Enqueue(new ValidAnswerCommand());
	}
	private void OnDestroy(){
		Broker.Unsubscribe<InPlaceMessage>(OnInPlaceMessageReceived);
	}
}
