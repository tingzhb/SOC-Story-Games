using System.Collections;
using UnityEngine;

public class DragDropGameController : MonoBehaviour {

	private int itemCount;

	private void Start(){
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
		SuccessMessage successMessage = new() {};
		Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);	}
	private void OnDestroy(){
		Broker.Unsubscribe<InPlaceMessage>(OnInPlaceMessageReceived);
	}
}
