using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WormGameController : MonoBehaviour {

	[SerializeField] private GameObject[] slots;
	[SerializeField] private GameObject wormPrefab, wellDone;
	[SerializeField] private int steps;
	private bool[] occupied;
	private int currentWorms, spawnableWorms;
	private Executor executor;
	public WormGameController(){
		occupied = new[] {false, false, false, false, false, false, false, false, false};
	}

	private void Start(){
		executor = FindObjectOfType<Executor>();
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		ChangeSpawnableWorms();
	}

	private void Update(){
		if (currentWorms < spawnableWorms && steps > 0){
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
		steps--;
		
		if (steps == 0) {
			wellDone.SetActive(true);
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
		yield return new WaitForSeconds(1.5f);
		executor.Enqueue(new ValidAnswerCommand());
	}
	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
