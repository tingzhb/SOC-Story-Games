using System;
using UnityEngine;
using UnityEngine.UIElements;

public class OnAwakeTransform : MonoBehaviour{
	private float currentScale, scaleDuration;
	[SerializeField] private float maxScaleDuration, scaleSpeed;

	private void Start(){
		SoundMessage soundMessage = new(){
			SoundType = 2
		};
		Broker.InvokeSubscribers(typeof(SoundMessage), soundMessage);
	}
	
	private void FixedUpdate() {
		if (scaleDuration < maxScaleDuration){
			scaleDuration += Time.deltaTime;
			currentScale += scaleSpeed * Time.deltaTime;
			transform.localScale += Vector3.one * currentScale;
		}
	}
}