using System;
using UnityEngine;

public class SoundMessageListener : MonoBehaviour{

	private SoundManager soundManager;
	private void Awake(){
		Broker.Subscribe<SoundMessage>(OnSoundMessageReceived);
		soundManager = GetComponent<SoundManager>();
	}

	private void Start(){
		soundManager.PlayAmbientMusic();
	}
	private void OnSoundMessageReceived(SoundMessage obj){
		switch (obj.SoundType){
			case 0:
				soundManager.StopMusic();
				soundManager.PlayMusic(obj.CurrentLevel);
				break;
			case 1: 
				soundManager.PlayErrorSound();
				break;
			case 2:
				soundManager.PlaySuccessSound();
				break;
			case 3:
				soundManager.PlayBubblePopSound();
				break;
			case 4:
				soundManager.PlayTapSound();
				break;
			case 5:
				soundManager.PlayDragEndSound();
				break;
			case 6:
				soundManager.PlayPlopSound();
				break;
			case 7:
				soundManager.PlayStirSound();
				break;
			case 8:
				soundManager.PlayEatingSound();
				break;
			case 9:
				soundManager.PlayCatSound();
				break;
			case 10:
				soundManager.PlayCowSound();
				break;
			case 99:
				soundManager.StopMusic();
				break;
			default:
				Debug.Log("Ignored");
				break;
		}
	}
}
