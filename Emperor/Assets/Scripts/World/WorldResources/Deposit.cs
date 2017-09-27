using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deposit : MonoBehaviour {

	public Resource ResourcesPresent;

	public int StartingAmount;
	public ResourceType Type;
	public float ResourceCollectionRate;

	// Use this for initialization
	void Start () {
		ResourcesPresent = new Resource()
		{
			ResourceAmount = StartingAmount,
			Type = Type,
			ResourceCollectionRate = ResourceCollectionRate
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
