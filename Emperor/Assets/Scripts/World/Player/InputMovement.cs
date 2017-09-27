using UnityEngine;
using System.Collections;

public class InputMovement : MonoBehaviour
{

    public Vector2 Speed = new Vector2(5.0f, 5.0f);

    public Vector2 Movement;

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Movement = new Vector2(Speed.x * inputX, Speed.y  * inputY);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Movement;
    }
}
