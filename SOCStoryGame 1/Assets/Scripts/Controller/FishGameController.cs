using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FishGameController : MonoBehaviour{
	[SerializeField] private GameObject[] fishes;
	private int fishType, progress;
	private float timer;
	[SerializeField] private GameObject[] fishGoals;
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private GameObject wellDone;
	private bool canSpawn;

	private void Awake(){
		Broker.Subscribe<SingleTapMessage>(OnSingleTapMessageReceived);
		StartCoroutine(DelayNewFish());
	}
	private void OnDisable(){ 
		Broker.Unsubscribe<SingleTapMessage>(OnSingleTapMessageReceived);
	}
	
	private void Update(){
		timer += Time.deltaTime;
		if (timer > 6){
			StartCoroutine(DelayNewFish());
			timer = -1;
		}
		if (canSpawn) {
			Instantiate(fishes[fishType], gameObject.transform);
			canSpawn = false;
		}
	}	

	
	private void OnSingleTapMessageReceived(SingleTapMessage obj){
		if (obj.TappedObject == "Bubble"){
			
			StartCoroutine(DelayFishResult());
			if (progress < 4){
				timer = 0;
				StartCoroutine(DelayNewFish());
			} else {
				canSpawn = false;
				wellDone.SetActive(true);
				StartCoroutine(DelayEnd());
			}
			
		}
	}

	private IEnumerator DelayNewFish(){
		yield return new WaitForSeconds(1f);
		fishType = Random.Range(0, fishes.Length);
		canSpawn = true;
	}
	
	private IEnumerator DelayFishResult(){
		yield return new WaitForSeconds(1f);
		fishGoals[progress].GetComponent<Image>().sprite = sprites[fishType];
		progress++;
	}

	private IEnumerator DelayEnd(){
		yield return new WaitForSeconds(2);
		SuccessMessage successMessage = new();
		Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);
	}


}
