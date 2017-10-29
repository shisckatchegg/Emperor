using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {

	public List<Transform> Villagers;
	private Vector2 _villageCenterPosition;
	public Transform VillageChief;

	public Transform VillageDepot;
	public Transform VillageHouses;
	public Transform VlillageFarms;

	public float ResourceRadius = 20.0f;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void ElectVillageChief()
	{
		int highestPrestige = -1000;
		Transform leadVillager = null;
		foreach(Transform villagerTransform in Villagers)
		{
			//Stats script = villagerTransform.GetComponent<Stats>();
			//if(highestPrestige < script.Prestige)
			{
			//	highestPrestige = script.Prestige;
				leadVillager = villagerTransform;
			}
		}

		VillageChief = leadVillager;
	}

	private void FindResources()
	{

	}
	
}
