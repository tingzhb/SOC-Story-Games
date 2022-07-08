using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropVerification : MonoBehaviour{
	[SerializeField] private GameObject[] droppedItems;
	[SerializeField] private int boundsLimits;
	private Bounds bounds;
	private int totalDropped;
	private Executor executor;
	private void Awake(){
		bounds = new Bounds(transform.position, new Vector3(boundsLimits, boundsLimits, boundsLimits));
		executor = FindObjectOfType<Executor>();
	}

	public void Verify(){
		foreach (var item in droppedItems){
			if (bounds.Contains(item.transform.position)){
				totalDropped++;
			}
		}
		if (totalDropped == 6){
			executor.Enqueue(new ValidAnswerCommand());
		}
		Debug.Log(totalDropped);
		totalDropped = 0;
	}
}
