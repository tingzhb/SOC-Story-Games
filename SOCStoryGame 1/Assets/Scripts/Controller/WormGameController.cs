using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WormGameController : MonoBehaviour {

	[SerializeField] private GameObject[] slots;
	[SerializeField] private GameObject wormPrefab, apple;
	[SerializeField] private GameObject[] steps;
	private bool[] occupied;
	private int wormScore, currentWorms, spawnableWorms;
	private Executor executor;
	public WormGameController(){
		occupied = new[] {false, false, false, false, false, false, false, false, false};
	}

	private void Start(){
		executor = FindObjectOfType<Executor>();
		wormScore = 0;
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		ChangeSpawnableWorms();
	}

	private void Update(){
		if (currentWorms < spawnableWorms && wormScore < steps.Length){
			SpawnWorm();
		}
	}

	private void ChangeSpawnableWorms(){
		spawnableWorms = Random.Range(1, 4);
	}

	private void SpawnWorm(){
		var slotNumber = Random.Range(0, 9);
		var wormLocation = slots[slotNumber].transform;
		if (!occupied[slotNumber]){
			currentWorms++;
			occupied[slotNumber] = true;
			var wormInstance = Instantiate(wormPrefab, wormLocation.position, Quaternion.identity, wormLocation);
			StartCoroutine(DespawnWorm(wormInstance, slotNumber));
		}
	}
	
	private void OnCorrectMessageReceived(CorrectMessage obj) {
		SoundMessage soundMessage = new(){
			SoundType = 6
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		wormScore++;

		if (wormScore < steps.Length){
			apple.transform.position = steps[wormScore].transform.position;
		}
		if (wormScore == steps.Length - 1) {
			StartCoroutine(DelayEnd());
		}
	}

	private IEnumerator DespawnWorm(GameObject wormInstance, int slotNumber){
		yield return new WaitForSeconds(2.2f);
		Destroy(wormInstance);
		occupied[slotNumber] = false;
		currentWorms--;
		ChangeSpawnableWorms();
	}
	private IEnumerator DelayEnd() {
		yield return new WaitForSeconds(1f);
		executor.Enqueue(new ValidAnswerCommand());
	}
	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
