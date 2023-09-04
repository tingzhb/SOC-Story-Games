using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VAKModel")]
public class VAKModel : ScriptableObject{
	[SerializeField] float v, a, k, vakTotal, vPercentage, aPercentage, kPercentage;

	public float VValue{
		get => v;
		set => v = value;
	}

	public float AValue{
		get => a;
		set => a = value;
	}

	public float KValue{
		get => k;
		set => k = value;
	}
	
	public float VAKTotal{
		get => vakTotal;
		set => vakTotal = value;
	}
	
	public float VPercentage{
		get => vPercentage;
		set => vPercentage = value;
	}

	public float APercentage{
		get => aPercentage;
		set => aPercentage = value;
	}

	public float KPercentage{
		get => kPercentage;
		set => kPercentage = value;
	}
}
