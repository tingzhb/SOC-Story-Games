using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WormGameController : MonoBehaviour {

	[SerializeField] private GameObject[] slots;
	[SerializeField] private GameObject wormPrefab;
	private int wormCount;
	private Executor executor;

	private void Start(){
		wormCount = 0;
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		SpawnWorm();
	}

	private void Update() {
		
	}

	private void SpawnWorm(){
		var wormLocation = slots[Random.Range(0, 9)].transform;
		var wormInstance = Instantiate(wormPrefab, wormLocation.position, Quaternion.identity, wormLocation);
		StartCoroutine(DespawnWorm(wormInstance));
	}
	
	private void OnCorrectMessageReceived(CorrectMessage obj) {
		// SoundMessage soundMessage = new(){
		// 	SoundType = 3
		// };
		// Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
		
		if (wormCount == 3) {
			StartCoroutine(DelayEnd());
		}
	}

	private IEnumerator DespawnWorm(GameObject wormInstance){
		yield return new WaitForSeconds(2.2f);
		Destroy(wormInstance);
	}
	private IEnumerator DelayEnd() {
		yield return new WaitForSeconds(1f);
		executor.Enqueue(new ValidAnswerCommand());
	}
	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
