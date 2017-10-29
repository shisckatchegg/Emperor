using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SocialRank
{
	Invalid = -1,
	Slave,
	Farmer,
	Merchant,
	Soldier,
	Noble,
	RoyalNoble,
	Emperor
}

public enum Gender
{
	Invalid = -1,
	Female,
	Male,
}

public class Character : MonoBehaviour {

	public SocialRank Rank;

	public Gender Gender;

	public int Health = 10;

	public int Relations;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
