using System;
using UnityEngine;

public class SingleAxisDragController : MonoBehaviour{
	[SerializeField] private GameObject[] bounds;
	[SerializeField] private GameObject drag;
	private int goal;
	private bool turn;
	
	private void Update(){
		if (turn && Math.Abs(drag.transform.position.x - bounds[0].transform.position.x) < 7.5f){
			goal++;
			Debug.Log(goal);
			turn = true;
		}
		if (!turn && Math.Abs(drag.transform.position.x - bounds[1].transform.position.x) < 7.5f){
			goal++;
			Debug.Log(goal);
			turn = false;
		}
		if (goal == 5){
			Debug.Log("win");
			goal = 0;
			turn = false;
			CorrectMessage correctMessage = new();
			Broker.InvokeSubscribers(typeof(CorrectMessage), correctMessage);
		}
	}
}
