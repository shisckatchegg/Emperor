using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour {

	public int MAX_CARRY_CAPACITY;
	[HideInInspector] public bool WantsToCollect;
	[HideInInspector] public bool MaxCapacityReached;
	public bool StartedCollecting;
	public Resource CarriedResource;

	private float _startingCollectionTime;

	private Resource _collectingResource;

	private const float COLLECTION_TOLERANCE = 0.05f;


	// Use this for initialization
	void Start () {
		CarriedResource = new Resource();
		
		StartedCollecting = false;

		MaxCapacityReached = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(StartedCollecting)
		{
			int timeCollecting = (int) _startingCollectionTime - (int) Time.time;
			int collectionRatio = timeCollecting % (int) _collectingResource.ResourceCollectionRate;
			if (Mathf.Abs(collectionRatio) < COLLECTION_TOLERANCE)
			{
				if (_collectingResource.Type != CarriedResource.Type)
				{
					CarriedResource.ResourceAmount = 0;
					CarriedResource.Type = _collectingResource.Type;
					MaxCapacityReached = false;
				}

				if (CarriedResource.ResourceAmount + 1 <= MAX_CARRY_CAPACITY)
				{
					CarriedResource.ResourceAmount++;
					_collectingResource.ResourceAmount--;
					Debug.Log("Collecting resource: " + CarriedResource.Type + " Amount: " + CarriedResource.ResourceAmount);
				}
				else
				{
					MaxCapacityReached = true;
					StartedCollecting = false;
				}
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Deposit script = collision.gameObject.GetComponent<Deposit>();
		if (script != null)
		{
			if (script.ResourcesPresent.ResourceAmount > 0)
			{
				if (StartedCollecting == false)
				{
					StartedCollecting = true;
					_startingCollectionTime = Time.time;
					_collectingResource = script.ResourcesPresent;
				}
			}
		}
	}
}
