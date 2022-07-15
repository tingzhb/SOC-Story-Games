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
	private void Start(){
		Broker.Subscribe<SingleTapMessage>(OnSingleTapMessageReceived);
		GetNewFish();
	}

	private void Update(){
		timer += Time.deltaTime;
		if (timer > 5f){
			GetNewFish();
		}
	}
	
	private void OnSingleTapMessageReceived(SingleTapMessage obj){
		if (obj.TappedObject == "Bubble"){
			fishGoals[progress].GetComponent<Image>().sprite = sprites[fishType];
			progress++;
			GetNewFish();
			if (progress == 5){
				
			}
		}
	}

	private void GetNewFish(){
		fishType = Random.Range(0, fishes.Length);
		Instantiate(fishes[fishType], gameObject.transform);
		timer = 0;
	}
}
