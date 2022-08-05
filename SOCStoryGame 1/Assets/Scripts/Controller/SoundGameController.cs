using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundGameController : MonoBehaviour{
	[SerializeField] private Transform[] spawnPoints;
	[SerializeField] private GameObject[] soundImagePrefabs;
	[SerializeField] private GameObject[] UIs;
	[SerializeField] private GameObject wellDone;
	private GameObject[] soundImageInstances;
	private int soundOptions = 1, soundAnswer, maxSounds = 5;
	private int[] soundQuestions, soundAnswers;
	private Executor executor;
	private bool started;
	
	private void Start(){
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<EggMessage>(OnEggMessageReceived);
		StartCoroutine(GenerateNewSound());
	}
	
	private void OnEggMessageReceived(EggMessage obj){
		if (!started)
			return;
		if (!obj.Saved){
			soundAnswers[soundAnswer] = 0;
		} else {
			soundAnswers[soundAnswer] = 1;
		}

		if (CheckIndividualSound()){
			soundAnswer++;
		} else {
			executor.Enqueue(new FailureCommand());
			ClearAnswers();
			StartCoroutine(Retry());
		}
		
		if (soundAnswer == soundOptions){
			UIs[soundOptions - 1].SetActive(true);
			soundOptions++;
			DestroyPreviousImages();
			if (soundOptions > maxSounds){
				wellDone.SetActive(true);
				StartCoroutine(DelayEnd());
			} else {
				StartCoroutine(GenerateNewSound());
			}
		}
	}

	private IEnumerator DelayEnd(){
		yield return new WaitForSeconds(2);
		executor.Enqueue(new ValidAnswerCommand());

	}
	public void Replay(){
		if (started){
			StartCoroutine(Retry());
		}
	}
	private IEnumerator Retry(){
		yield return new WaitForSeconds(1f);
		StartCoroutine(ReplaySound());
		StartCoroutine(ReplayText());
	}
	private void DestroyPreviousImages(){
		StartCoroutine(DelayDestroy());
	}
	private IEnumerator DelayDestroy(){
		yield return new WaitForSeconds(1);
		foreach (var image in soundImageInstances){
			Destroy(image);
		}
	}
	
	private IEnumerator GenerateNewSound(){
		ClearAnswers();
		yield return new WaitForSeconds(2f);
		started = true;
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
			
			yield return new WaitForSeconds(1f);
		}
	}

	private IEnumerator ReplayText(){
		foreach (var gameObject in soundImageInstances){
			gameObject.GetComponent<SoundTextShower>().ShowText();
			gameObject.GetComponent<AnimateOnce>().StartAnimation();
			yield return new WaitForSeconds(1f);
		}
	}

	private IEnumerator ReplaySound() {
		foreach (var sound in soundQuestions){
			if (sound == 0){
				PlayCowSound();
			} else {
				PlayCatSound();
			}
			yield return new WaitForSeconds(1f);
		}
	}

	private void PlayCowSound(){
		SoundMessage soundMessage = new(){
			SoundType = 10
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
	}

	private void PlayCatSound(){
		SoundMessage soundMessage = new(){
			SoundType = 9
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