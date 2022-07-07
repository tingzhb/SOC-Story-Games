using UnityEngine;

public class SceneBasedSoundController : MonoBehaviour{
	[SerializeField] private bool playMusic;
	[SerializeField] private int levelNumber;
	private void Awake() {
		Broker.Subscribe<SoundMessage>(OnSoundMessageReceived);
	}

	private void Start(){
		if (playMusic){
			SoundMessage soundMessage = new(){
				SoundType = 0,
				CurrentLevel = levelNumber
			};
			Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		}
	}

	private void OnSoundMessageReceived(SoundMessage obj){
		if (obj.SoundType == 2){
			Debug.Log("Replaying VO");
		}
	}
	private void OnDestroy(){
		Broker.Unsubscribe<SoundMessage>(OnSoundMessageReceived);
	}
}
