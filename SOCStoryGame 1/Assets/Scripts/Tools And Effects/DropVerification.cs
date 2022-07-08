using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropVerification : MonoBehaviour{
	[SerializeField] private GameObject[] droppedItems;
	[SerializeField] private int boundsCalibration;
	private Bounds bounds;
	private int totalDropped;
	private void Awake(){
		bounds = new Bounds(transform.position, Vector3.one * boundsCalibration);
	}

	private void Update(){
		foreach (var item in droppedItems){
			if (bounds.Contains(item.transform.position)){
				totalDropped++;
			}
		}
		Debug.Log(totalDropped);
	}
}
