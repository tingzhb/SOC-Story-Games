using UnityEngine;

public class OnTap : MonoBehaviour {
	
	public void Tap() {
		var selfTag = gameObject.tag;
		SingleTapMessage singleTapMessage = new(){TappedObject = selfTag};
		Broker.InvokeSubscribers(typeof(SingleTapMessage), singleTapMessage);
	}
}