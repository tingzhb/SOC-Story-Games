using System;
using UnityEngine;
using UnityEngine.UIElements;

public class OnAwakeTransform : MonoBehaviour{
	private float currentScale;
	[SerializeField] private float scaleDuration, scaleSpeed;
	private void Update() {
		if (currentScale < scaleDuration){
			currentScale += scaleSpeed * Time.deltaTime;
			transform.localScale += Vector3.one * currentScale;
			Debug.Log(currentScale);
		}
	}
}