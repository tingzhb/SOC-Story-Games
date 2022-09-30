using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class DifferentDolphinGame : MonoBehaviour{

	[SerializeField] private GameObject[] dolphinSlots;
	[SerializeField] private Sprite[] dolphinOptions;
	[SerializeField] private GameObject correctDolphin;
	private int randomCorrectImage;
	
	
	private void Awake(){ 
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		Broker.Subscribe<FailureMessage>(OnFailureMessageReceived);
		SpawnDolphins();
	}

	private void OnDisable(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
		Broker.Unsubscribe<FailureMessage>(OnFailureMessageReceived);

	}
	private void OnFailureMessageReceived(FailureMessage obj){
		SpawnDolphins();
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		SpawnDolphins();
	}

	private void SpawnDolphins(){
		var randomImageOption = new Random().Next(0, dolphinOptions.Length);
		foreach (var slot in dolphinSlots){
			slot.GetComponent<Image>().sprite = dolphinOptions[randomImageOption];
		}

		var randomPosition = new Random().Next(0, dolphinSlots.Length);
		var correct = Instantiate(correctDolphin, dolphinSlots[randomPosition].transform);
		GetExclusiveRandom(randomImageOption);
		correct.GetComponent<Image>().sprite = dolphinOptions[randomCorrectImage];
	}
	
	private void GetExclusiveRandom(int randomImageOption){
		randomCorrectImage = new Random().Next(0, dolphinOptions.Length);
		if (randomCorrectImage == randomImageOption){
			GetExclusiveRandom(randomImageOption);
			Debug.Log("SAME!");
		}
	}
}
