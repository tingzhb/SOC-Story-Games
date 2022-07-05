using System;
using UnityEngine;
using UnityEngine.UIElements;

public class OnAwakeTransform : MonoBehaviour{
	private float currentScale, scaleDuration;
	[SerializeField] private float maxScaleDuration, scaleSpeed;
	private void FixedUpdate() {
		if (scaleDuration < maxScaleDuration){
			scaleDuration += Time.deltaTime;
			currentScale += scaleSpeed * Time.deltaTime;
			transform.localScale += Vector3.one * currentScale;
			Debug.Log(maxScaleDuration);
		}
	}
}