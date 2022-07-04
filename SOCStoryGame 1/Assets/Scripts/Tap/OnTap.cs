using UnityEngine;

public class OnTap : MonoBehaviour {
	
	public void Tap() {
		var self = gameObject;
		SingleTapMessage singleTapMessage = new(){ TappedObject = self};
		Broker.InvokeSubscribers(typeof(SingleTapMessage), singleTapMessage);
	}
}
