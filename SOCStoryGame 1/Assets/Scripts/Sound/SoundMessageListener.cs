using UnityEngine;

public class SoundMessageListener : MonoBehaviour{

	private SoundManager soundManager;
	private void Awake(){
		Broker.Subscribe<SoundMessage>(OnSoundMessageReceived);
		soundManager = GetComponent<SoundManager>();
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
			case 99:
				soundManager.StopMusic();
				break;
		}

	}
}
