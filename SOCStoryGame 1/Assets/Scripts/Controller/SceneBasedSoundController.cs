using FMOD.Studio;
using UnityEngine;

public class SceneBasedSoundController : MonoBehaviour{
	[SerializeField] private bool playMusic, playVO = true;
	[SerializeField] private int levelNumber;
	[SerializeField] private FMODUnity.EventReference voiceOver;
	private EventInstance voiceOverInstance;
	private bool voiceOverPlaying;
	private void Awake() {
		if (playVO){
			Broker.Subscribe<SoundMessage>(OnSoundMessageReceived);
			PlayVoiceOver();
		}
	}

	private void Start(){
		if (playMusic){
			voiceOverPlaying = true;
			SoundMessage soundMessage = new(){
				SoundType = 0,
				CurrentLevel = levelNumber
			};
			Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		}
	}

	private void OnSoundMessageReceived(SoundMessage obj){
		if (obj.SoundType == 98){
			PlayVoiceOver();
		}
	}
	private void PlayVoiceOver(){
		if (!voiceOverPlaying){
			voiceOverInstance = FMODUnity.RuntimeManager.CreateInstance(voiceOver);
			voiceOverInstance.start();
			voiceOverPlaying = true;
		}
		else {
			StopVoiceOver();
		}
	}
	private void StopVoiceOver(){
		voiceOverInstance.stop(STOP_MODE.ALLOWFADEOUT);
		voiceOverPlaying = false;
	}
	
	private void OnDestroy(){
		StopVoiceOver();
		Broker.Unsubscribe<SoundMessage>(OnSoundMessageReceived);
	}
}
