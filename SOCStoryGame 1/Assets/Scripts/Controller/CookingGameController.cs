using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CookingGameController : MonoBehaviour{
	[SerializeField] private GameObject[] tasks;
	private int currentTask;
	private void Start(){
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		GetTask();
	}
	private void GetTask(){
		currentTask = Random.Range(0, tasks.Length);
		tasks[currentTask].SetActive(true);
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		tasks[currentTask].SetActive(false);
		GetTask();
	}
	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
