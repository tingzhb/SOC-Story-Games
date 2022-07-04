using UnityEngine;

public class OnTap : MonoBehaviour {
	
	public void Tap() {
		var onj = gameObject.tag;
		SingleTapMessage singleTapMessage = new(){ TappedObject = onj};
		Broker.InvokeSubscribers(typeof(SingleTapMessage), singleTapMessage);
	}
}