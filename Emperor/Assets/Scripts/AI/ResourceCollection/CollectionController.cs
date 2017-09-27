using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceCollection))]
[RequireComponent(typeof(AILerp))]
public class CollectionController : MonoBehaviour {

	private ResourceCollection _resourceCollection;
	private Transform _resourceDepositPosition;
	private ResourceDepot _resourceDepot;
	private AILerp _pathFinding;
	private Transform _depotPosition;
	

	// Use this for initialization
	void Start () {

		_resourceCollection = gameObject.GetComponent<ResourceCollection>();

		_pathFinding = GetComponent<AILerp>();

		_depotPosition = GameObject.Find("LongHouse1").transform;
		_resourceDepot = GameObject.Find("LongHouse1").GetComponent<ResourceDepot>();

		_resourceDepositPosition = GameObject.Find("Rock(Clone)").transform;

		_resourceCollection.WantsToCollect = true;
		
	}

	// Update is called once per frame
	void Update () {

		if (_resourceCollection.MaxCapacityReached)
		{
			_pathFinding.target = _depotPosition;
		}
		else
		{
			_pathFinding.target = _resourceDepositPosition;
		}
	}
}
