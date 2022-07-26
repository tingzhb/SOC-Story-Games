using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WormKiller : MonoBehaviour{
	[SerializeField] private GameObject deadWorm;

	public void KillWorm(){
		GetComponent<Image>().enabled = false;
		deadWorm.SetActive(true);
	}
}
