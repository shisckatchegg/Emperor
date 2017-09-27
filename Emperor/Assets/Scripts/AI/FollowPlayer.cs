using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	private GameObject _player;
	public float Radius = 10f;
	private AILerp _movement;
	public float Speed = 3f;

	// Use this for initialization
	void Start () {

		_movement = GetComponent<AILerp>();

		_player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if (_movement.target == null)
		{
			Vector3 distance = transform.position - _player.transform.position;
			if (distance.magnitude > Radius)
			{
				transform.position -= distance.normalized * Speed * Time.deltaTime;
			}
		}
	}
}
