using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundGameController : MonoBehaviour{
	[SerializeField] private Transform[] spawnPoints;
	[SerializeField] private GameObject[] soundImagePrefabs;
	private GameObject[] soundImageInstances;
	private int soundOptions = 1, soundAnswer, maxSounds = 5;
	private int[] soundQuestions, soundAnswers;
	private Executor executor;
	
	private void Start() {
		Broker.Subscribe<EggMessage>(OnEggMessageReceived);
		StartCoroutine(GenerateNewSound());
	}
	
	private void OnEggMessageReceived(EggMessage obj) {
		if (!obj.Saved){
			soundAnswers[soundAnswer] = 0;
		} else {
			soundAnswers[soundAnswer] = 1;
		}

		if (CheckIndividualSound()){
			soundAnswer++;
		} else {
			ClearAnswers();
			StartCoroutine(Retry());
		}
		
		if (soundAnswer == soundOptions){
			soundOptions++;
			Debug.Log("Win");
			DestroyPreviousImages();
			if (soundOptions > maxSounds){
				Debug.Log("LevelEnd");
			} else {
				StartCoroutine(GenerateNewSound());
			}
		}
	}
	private IEnumerator Retry(){
		yield return new WaitForSeconds(1f);
		StartCoroutine(ReplaySound());
		StartCoroutine(ReplayText());
	}
	private void DestroyPreviousImages(){
		foreach (var image in soundImageInstances){
			Destroy(image);
		}
	}
	
	private IEnumerator GenerateNewSound(){
		ClearAnswers();
		yield return new WaitForSeconds(1f);
		soundQuestions = new int[soundOptions];
		soundImageInstances = new GameObject[soundOptions];
		for (int i = 0; i < soundQuestions.Length; i++){
			var randomNumber = Random.Range(0, 2);
			soundQuestions[i] = randomNumber;
			
			var spawnTransform = spawnPoints[i].transform;
			soundImageInstances[i] = Instantiate(soundImagePrefabs[randomNumber], spawnTransform.position, Quaternion.identity, spawnTransform);
			
			if (randomNumber == 0){
				PlayCowSound();
			} else {
				PlayCatSound();
				
			}
			soundImageInstances[i].GetComponent<SoundTextShower>().ShowText();
			
			yield return new WaitForSeconds(0.75f);
		}
	}

	private IEnumerator ReplayText(){
		foreach (var gameObject in soundImageInstances){
			gameObject.GetComponent<SoundTextShower>().ShowText();
			yield return new WaitForSeconds(0.75f);
		}
	}

	private IEnumerator ReplaySound() {
		foreach (var sound in soundQuestions){
			if (sound == 0){
				PlayCowSound();
			} else {
				PlayCatSound();
			}
			yield return new WaitForSeconds(0.75f);
		}
	}

	private void PlayCowSound(){
		Debug.Log("Moo");
		SoundMessage soundMessage = new(){
			SoundType = 5
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
	}

	private void PlayCatSound(){
		Debug.Log("Meow");
		SoundMessage soundMessage = new(){
			SoundType = 6
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
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