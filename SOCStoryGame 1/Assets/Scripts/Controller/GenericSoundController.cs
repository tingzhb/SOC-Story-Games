using UnityEngine;

public class GenericSoundController : MonoBehaviour{
	
	private void Awake() {
		Broker.Subscribe<SoundMessage>(OnSoundMessageReceived);
	}

	private void OnSoundMessageReceived(SoundMessage obj){
		if (obj.SoundType == 1){
			Debug.Log("Generic Error Sound");
		}
	}
}
