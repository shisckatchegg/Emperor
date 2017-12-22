using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {

	public List<Transform> Villagers;
	private Vector2 _villageCenterPosition;
	public Transform VillageChief;

	public Transform VillageDepot;
	public Transform VillageHouses;
	public Transform VillageFarms;

	public float ResourceRadius = 20.0f;

	public int InitialPopulation;

	// Use this for initialization
	void Start ()
	{
		GenerateVillagers();

		GenerateBuildings();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void GenerateVillagers()
	{
		Villagers = new List<Transform>();

		for (int i = 0; i < InitialPopulation; i++)
		{
			Transform villager = null;
			villager = Instantiate(villager, _villageCenterPosition, Quaternion.identity);
			Villagers.Add(villager);
		}
	}

	private void GenerateBuildings()
	{
		GenerateWarehouse();

		GenerateHouses();
	}

	private void GenerateWarehouse()
	{
		//Instantiate()
	}

	private void GenerateHouses()
	{

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
