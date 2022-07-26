using UnityEngine;

public class OnTap : MonoBehaviour{
	[SerializeField] private bool playClickSound = true;
	
	public void Tap() {
		var selfTag = gameObject.tag;
		SingleTapMessage singleTapMessage = new(){TappedObject = selfTag};
		Broker.InvokeSubscribers(typeof(SingleTapMessage), singleTapMessage);
		if (playClickSound){
			SoundMessage soundMessage = new(){SoundType = 4};
			Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		}
	}
}