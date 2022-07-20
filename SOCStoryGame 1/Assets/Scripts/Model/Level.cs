using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour{
	[SerializeField] public string sceneName;

	public void GoToLevel(){
		StartCoroutine(DelayLoadLevel());
	}

	private IEnumerator DelayLoadLevel(){
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadSceneAsync(sceneName);
	}
}
