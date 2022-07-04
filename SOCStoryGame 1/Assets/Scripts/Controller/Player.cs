using UnityEngine;

public class Player : MonoBehaviour {
	private void Start() {
		DontDestroyOnLoad(gameObject);
	}
}
