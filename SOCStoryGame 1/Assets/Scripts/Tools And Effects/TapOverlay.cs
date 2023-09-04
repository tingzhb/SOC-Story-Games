using System.Collections;
using UnityEngine;

public class TapOverlay : MonoBehaviour{
	[SerializeField] private GameObject highlight;

	public void Highlight(){
		highlight.SetActive(true);
		StartCoroutine(DisableHighlight());
	}

	private IEnumerator DisableHighlight(){
		yield return new WaitForSeconds(1f);
		highlight.SetActive(false);
	}
}
