using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyComponent;

    private bool dead;
    private float angleOnCircle;

	private void Start ()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        angleOnCircle = 0;
        dead = false;
	}

    private void FixedUpdate()
    {
        CheckPlayerMovement();
    }

    private void CheckPlayerMovement()
    {
        if (dead)
        {
            return;
        }

        float movement = (Input.GetAxisRaw("Horizontal") * Time.deltaTime * GameConstants.anglesPerSecond * LevelSelectData._levelSelect._tunnelVelocity);
        angleOnCircle = (angleOnCircle + movement) % 360;

        // uses radians so the mod should be 2*pi
        float x = Mathf.Sin(angleOnCircle * Mathf.Deg2Rad) * GameConstants.playerDistanceFromCenter;
        float y = Mathf.Cos(angleOnCircle * Mathf.Deg2Rad) * GameConstants.playerDistanceFromCenter;

        rigidbodyComponent.MovePosition(new Vector2(x, y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        dead = true;
        GameController._controller.GameOver();
    }
}
