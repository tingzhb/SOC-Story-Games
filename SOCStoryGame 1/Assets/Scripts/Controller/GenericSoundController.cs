// using UnityEngine;
//
// public class GenericSoundController : MonoBehaviour{
// 	private SoundManager soundManager;
// 	private void Awake() {
// 		Broker.Subscribe<SoundMessage>(OnSoundMessageReceived);
// 		soundManager = GetComponent<SoundManager>();
// 	}
//
// 	private void OnSoundMessageReceived(SoundMessage obj){
// 		switch (obj.SoundType){
// 			case 1:
// 				soundManager.PlayErrorSound();
// 				break;
// 			case 2:
// 				soundManager.PlaySuccessSound();
// 				break;
// 		}
// 	}
// }
