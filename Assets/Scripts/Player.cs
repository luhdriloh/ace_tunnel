using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyComponent;

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

    private void CheckPlayerMovement()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) <= Mathf.Epsilon)
        {
            return;
        }

        float movement = (Input.GetAxis("Horizontal") * Time.deltaTime * GameConstants.anglesPerSecond);
        angleOnCircle = (angleOnCircle + movement) % 360;

        // also add the center of the circle. In our case we don't need to add anything
        //    the center of the circle is (0, 0)
        // we now know our angle, use radius, sin, and cos to find our new location

        // uses radians so the mod should be 2*pi
        float x = Mathf.Sin(angleOnCircle * Mathf.Deg2Rad) * GameConstants.playerDistanceFromCenter;
        float y = Mathf.Cos(angleOnCircle * Mathf.Deg2Rad) * GameConstants.playerDistanceFromCenter;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("tunnel"))
        {
            Debug.Log("hit the fucking wall!");
        }
    }
}
