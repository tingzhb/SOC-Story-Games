using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundGameController : MonoBehaviour{
	private int soundsToTest = 5, soundChoice;
	private int[] soundBinary, soundBinaryAnswer;
	private Executor executor;
	private void Start() {
		Broker.Subscribe<EggMessage>(OnEggMessageReceived);
		GenerateNewSound();
		PlaySound();
	}
	private void OnEggMessageReceived(EggMessage obj){
		if (!obj.Saved){
			soundBinaryAnswer[soundChoice] = 0;
		} else {
			soundBinaryAnswer[soundChoice] = 1;
		}
		soundChoice++;
		if (soundChoice == soundsToTest){
			if (CheckAnswer()){
				Debug.Log("Win");
			} else {
				Debug.Log("Lose");
			}
		}
	}

	private void GenerateNewSound(){
		soundChoice = 0;
		soundBinary = new int[soundsToTest];
		soundBinaryAnswer = new int[soundsToTest];
		for (var i = 0; i < soundBinary.Length; i++){
			soundBinary[i] = Random.Range(0, 2);
		}
	}

	private void PlaySound(){
		foreach (var sound in soundBinary){
			if (sound == 0){
				Debug.Log("moo");
			} else {
				Debug.Log("meow");
			}
		}
	}

	private bool CheckAnswer(){
		return !soundBinaryAnswer.Where((t, i) => t != soundBinary[i]).Any();
	}
}
