using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardPositionRandomizer : MonoBehaviour{
	[SerializeField] private GameObject[] cards;
	private void Awake(){
		foreach (var card in cards){
			var firstSelection = Random.Range(0, 5);
			var secondSelection = Random.Range(6, 12);
			(cards[firstSelection].transform.position, cards[secondSelection].transform.position) = (cards[secondSelection].transform.position, cards[firstSelection].transform.position);
		}
	}
}
