using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundGameController : MonoBehaviour {
	private int soundOptions = 1, soundAnswer, maxSounds = 5;
	private int[] soundQuestions, soundAnswers;
	private Executor executor;
	
	private void Start() {
		Broker.Subscribe<EggMessage>(OnEggMessageReceived);
		GenerateNewSound();
		PlaySound();
	}
	
	private void OnEggMessageReceived(EggMessage obj) {
		if (!obj.Saved){
			soundAnswers[soundAnswer] = 0;
		} else {
			soundAnswers[soundAnswer] = 1;
		}

		if (CheckIndividualSound()){
			Debug.Log("Correct");
			soundAnswer++;
		} else {
			Debug.Log("Lose");
			ClearAnswers();
			PlaySound();
		}
		
		if (soundAnswer == soundOptions){
			soundOptions++;
			Debug.Log("Win");
			
			if (soundOptions > maxSounds){
				Debug.Log("LevelEnd");
			} else {
				GenerateNewSound();
				PlaySound();
			}
		}
	}

	private void GenerateNewSound() {
		ClearAnswers();
		soundQuestions = new int[soundOptions];
		for (var i = 0; i < soundQuestions.Length; i++){
			soundQuestions[i] = Random.Range(0, 2);
		}
	}

	private void PlaySound() {
		foreach (var sound in soundQuestions){
			if (sound == 0){
				Debug.Log("moo");
			} else {
				Debug.Log("meow");
			}
		}
	}

	private bool CheckIndividualSound(){
		return soundQuestions[soundAnswer] == soundAnswers[soundAnswer];
	}

	private void ClearAnswers() {
		soundAnswer = 0;
		soundAnswers = new int[soundOptions];
	}

	private void OnDestroy() {
		Broker.Unsubscribe<EggMessage>(OnEggMessageReceived);
	}
}