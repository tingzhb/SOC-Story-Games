using UnityEngine;

public class SceneBasedSoundController : MonoBehaviour{
	
	private void Awake() {
		Broker.Subscribe<SoundMessage>(OnSoundMessageReceived);
	}

	private void OnSoundMessageReceived(SoundMessage obj){
		if (obj.SoundType == 2){
			Debug.Log("Replaying VO");
		}
	}
}
