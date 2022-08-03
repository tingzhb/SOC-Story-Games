using System.Collections;
using UnityEngine;

public class SoundTextShower : MonoBehaviour{

	[SerializeField] private GameObject animalSound;

	public void ShowText(){
		animalSound.SetActive(true);
		StartCoroutine(DelayDestroy());
	}

	private IEnumerator DelayDestroy(){
		yield return new WaitForSeconds(1);
		animalSound.SetActive(false);
	}
}
