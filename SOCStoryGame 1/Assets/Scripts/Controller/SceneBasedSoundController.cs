using System.Collections;
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
	
	private void OnDisable(){
		StopVoiceOver();
		Broker.Unsubscribe<SoundMessage>(OnSoundMessageReceived);
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

	void FixedUpdate(){
		if (!voiceOverPlaying) return;
			voiceOverInstance.getPlaybackState(out var newState);
		if (newState == PLAYBACK_STATE.STOPPED){
			StartCoroutine(DelaySceneChange());
		}
	}

	IEnumerator DelaySceneChange(){
		voiceOverPlaying = false;
		yield return new WaitForSeconds(4);
		SuccessMessage successMessage = new();
		Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);
	}
	
	private void OnSoundMessageReceived(SoundMessage obj){
		if (obj.SoundType == 98){
			if (!voiceOverPlaying){
				PlayVoiceOver();
				VAKMessage vakMessage = new(){
					A = 1
				};
				Broker.InvokeSubscribers(typeof(VAKMessage), vakMessage);
			} else {
				StopVoiceOver();
				VAKMessage vakMessage = new(){
					V = 1
				};
				Broker.InvokeSubscribers(typeof(VAKMessage), vakMessage);
			}
		}
	}
	private void PlayVoiceOver(){
		voiceOverInstance = FMODUnity.RuntimeManager.CreateInstance(voiceOver);
		voiceOverInstance.start();
		voiceOverPlaying = true;
	}
	
	private void StopVoiceOver(){
		voiceOverInstance.setPaused(true);
		voiceOverPlaying = false;
	}
}
