using UnityEngine;

public class OnTap : MonoBehaviour {
	
	public void Tap() {
		var selfTag = gameObject.tag;
		SingleTapMessage singleTapMessage = new(){TappedObject = selfTag};
		Broker.InvokeSubscribers(typeof(SingleTapMessage), singleTapMessage);
		
		SoundMessage soundMessage = new(){SoundType = 4};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
	}
}