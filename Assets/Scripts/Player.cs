using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyComponent;
    public float radius;
    public float speed;

    private float angleOnCircle;


	private void Start ()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();

        angleOnCircle = 0.0f;
	}
	

	private void Update ()
    {
        CheckPlayerMovement();
    }

    private void FixedUpdate()
    {

    }

    private void CheckPlayerMovement()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) <= Mathf.Epsilon)
        {
            return;
        }

        float movement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        angleOnCircle = (angleOnCircle + movement) % 360;

        // also add the center of the circle. In our case we don't need to add anything
        //    the center of the circle is (0, 0)
        // we now know our angle, use radius, sin, and cos to find our new location
        float x = Mathf.Sin(angleOnCircle) * radius;
        float y = Mathf.Cos(angleOnCircle) * radius;

        transform.position = new Vector3(x, y, 0);
    }
}
