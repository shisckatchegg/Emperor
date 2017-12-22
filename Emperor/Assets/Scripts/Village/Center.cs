using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {

	public List<Transform> Villagers;
	private Vector2 _villageCenterPosition;

	public Transform VillageChief;
	
	public Transform VillagerPrefab;
	public Transform VillageDepotPrefab;
	public Transform VillageHousePrefab;

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
			Villagers.Add(Instantiate(VillagerPrefab, _villageCenterPosition, Quaternion.identity));
		}
	}

	private void GenerateBuildings()
	{
		GenerateVillageDepot();

		GenerateVillageHouses();
	}

	private void GenerateVillageDepot()
	{
		Instantiate(VillageDepotPrefab, MathsHelpers.ConvertMapCoordintesToWorld(_villageCenterPosition), Quaternion.identity);
	}

	private void GenerateVillageHouses()
	{
		Instantiate(VillageHousePrefab, MathsHelpers.ConvertMapCoordintesToWorld(_villageCenterPosition), Quaternion.identity);
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
