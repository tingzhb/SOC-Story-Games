using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CookingGameController : MonoBehaviour{
	[SerializeField] private GameObject[] tasks;
	[SerializeField] private GameObject wellDone;
	[SerializeField] private int steps;
	private GameObject currentTask;
	private void Awake(){
		Broker.Subscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
		GetNewTask();
	}

	void OnDisable(){
		Broker.Unsubscribe<ExecuteOnceMessage>(OnExecuteOnceMessageReceived);
	}
	private void GetNewTask(){
		var newTask = Random.Range(0, tasks.Length);
		var spawnPoint = transform;
		if (steps > 0){
			currentTask = Instantiate(tasks[newTask], spawnPoint.position, Quaternion.identity, spawnPoint);
		}
	}
	private void OnExecuteOnceMessageReceived(ExecuteOnceMessage obj){
		Destroy(currentTask);
		steps--;
		if (steps == 0){
			StartCoroutine(DelayEnd());
		}
		GetNewTask();
	}

	private IEnumerator DelayEnd(){
		yield return new WaitForSeconds(0.25f);
		wellDone.SetActive(true);
	}
}
