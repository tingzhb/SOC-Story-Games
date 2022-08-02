using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleInflator : MonoBehaviour{
	[SerializeField] private Sprite[] sprites;
	[SerializeField] private Image image;
	private int progress;
	private Executor executor;

	private void Awake(){
		executor = FindObjectOfType<Executor>();
	}
	private void Update(){
		if (progress >= sprites.Length){
			var transformation = 500 * Time.deltaTime;
			transform.Translate(new Vector3(transformation, transformation, 0));
		}
	}
	public void Blow(){
		progress++;
		if (progress < sprites.Length){
			image.sprite = sprites[progress];
		}
		if (progress == sprites.Length){
			SoundMessage soundMessage = new(){
				SoundType = 3
			};
			Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
			StartCoroutine(WaitForFloat());
		}
	}
	private IEnumerator WaitForFloat(){
		yield return new WaitForSeconds(2);
		executor.Enqueue(new CorrectCommand());
	}
}
