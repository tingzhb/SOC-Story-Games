using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySpawn : MonoBehaviour{
	[SerializeField] private GameObject item;
	private float timer;

	private void Update(){
		timer += Time.deltaTime;
		if (timer >= 2.8f){
			item.SetActive(true);
		}
	}
}
