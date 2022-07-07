using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour{
	[SerializeField] public string sceneName;

	public void GoToLevel(){
		SceneManager.LoadSceneAsync(sceneName);
	}
}
