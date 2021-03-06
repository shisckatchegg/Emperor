﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDepot : MonoBehaviour {

	public int [] StoredResources;

	// Use this for initialization
	void Start () {
		StoredResources = new int[(int)ResourceType.Count];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision != null)
		{
			ResourceCollection script = collision.gameObject.GetComponent<ResourceCollection>();
			if (script != null)
			{
				Resource resource = script.CarriedResource;
				if (resource != null)
				{
					if (resource.Type != ResourceType.Invalid)
					{
						StoredResources[(int)resource.Type] += resource.ResourceAmount;
						resource.ResourceAmount = 0;
						script.MaxCapacityReached = false;
					}
				}
			}
		}
	}
}
