using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleGameController : MonoBehaviour{

	[SerializeField] private GameObject[] stars;
	private int bubbleCount;

	private void Awake(){
		Broker.Subscribe<BubbleMessage>(OnBubbleMessageReceived);
	}
	private void OnBubbleMessageReceived(BubbleMessage obj){
		stars[bubbleCount].SetActive(true);
		bubbleCount++;
	}
}
