using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class DifferentDolphinGame : MonoBehaviour{

	[SerializeField] private GameObject[] dolphinSlots;
	[SerializeField] private Sprite[] dolphinOptions;
	[SerializeField] private GameObject correctDolphin;
	[SerializeField] private GameObject[] progressSlots;
	[SerializeField] private Sprite[] progressStatus;
	
	private int randomCorrectImage, progression, corrects, wrongs;
	
	
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
		wrongs++;
		progressSlots[progression].GetComponent<Image>().sprite = progressStatus[1];
		progression++;
		CheckProgression();
		StartCoroutine(DelaySpawn());
	}
	private void OnCorrectMessageReceived(CorrectMessage obj){
		corrects++;
		progressSlots[progression].GetComponent<Image>().sprite = progressStatus[0];
		progression++;
		CheckProgression();
		SpawnDolphins();
	}

	private IEnumerator DelaySpawn(){
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(DelaySpawn());
	}

	private IEnumerator DelayRoundEnd(){
		yield return new WaitForSeconds(0.5f);
		
		if (corrects >= wrongs){
			SuccessMessage successMessage = new();
			Broker.InvokeSubscribers(typeof(SuccessMessage), successMessage);
		}
		else{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private void CheckProgression(){
		if (progression != 5)
			return;
		
		StartCoroutine(DelayRoundEnd());
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
