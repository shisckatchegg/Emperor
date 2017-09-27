using UnityEngine;
using System.Collections;

public class ApproachPlayer : MonoBehaviour
{

    private GameObject _player = null;
    private Dialogue _dialogue = null;
	private AILerp _movement;

    private bool shouldApproachPlayer = false;

    public float Speed = 1.5f;    
    public float DistanceAroundPLayer = 3.5f;

	// Use this for initialization
	void Start ()
    {
		_player = GameObject.Find("Player");
		_movement = GetComponent<AILerp>();
        _dialogue = GetComponent<Dialogue>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(shouldApproachPlayer)
        {
            Vector3 distance = transform.position - _player.transform.position;
            transform.position -= distance.normalized * Speed * Time.deltaTime;
            if(distance.sqrMagnitude < DistanceAroundPLayer)
            {
                shouldApproachPlayer = false;
				_movement.target = null;

				if (_dialogue != null)
                {
                    _dialogue.SetCanTalk(true);
                }
                else
                {
                    Debug.Log("Dialogue script handler is null.");
                }
            }
        }
	}

    void OnMouseUp()
    {
        if (_player == null)
        {
            _player = GameObject.Find("Player");
        }

        shouldApproachPlayer = true;
		_movement.target = _player.transform;

		Vector3 distance = transform.position - _player.transform.position;
        if (distance.sqrMagnitude < DistanceAroundPLayer)
        {
            shouldApproachPlayer = false;
			_movement.target = null;

			if (_dialogue != null)
            {
                _dialogue.SetCanTalk(true);
            }
            else
            {
                Debug.Log("Dialogue script handler is null.");
            }
        }
    }
}
