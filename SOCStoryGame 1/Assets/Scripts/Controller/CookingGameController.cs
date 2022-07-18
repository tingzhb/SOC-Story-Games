using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CookingGameController : MonoBehaviour{
	[SerializeField] private GameObject[] tasks;
	[SerializeField] private GameObject[] progression;
	[SerializeField] private GameObject marker, wellDone;
	private int currentTask, progress;
	private void Start(){
		Broker.Subscribe<CorrectMessage>(OnCorrectMessageReceived);
		GetNewTask();
	}
	private void GetNewTask(){
		currentTask = Random.Range(0, tasks.Length);
		tasks[currentTask].SetActive(true);
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		tasks[currentTask].SetActive(false);
		marker.transform.position = progression[progress].transform.position;
		GetNewTask();
		progress+= 2;
		Debug.Log(progress);
		if (progress == 20){
			StartCoroutine(DelayEnd());
		}
	}

	private IEnumerator DelayEnd(){
		yield return new WaitForSeconds(0.1f);
		wellDone.SetActive(true);
	}
	private void OnDestroy(){
		Broker.Unsubscribe<CorrectMessage>(OnCorrectMessageReceived);
	}
}
