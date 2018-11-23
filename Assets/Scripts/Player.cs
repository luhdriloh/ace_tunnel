using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyComponent;
    private bool dead;
    private float angleOnCircle;

    private void Start()
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

        float movement = 0f;
       
        #if UNITY_STANDALONE
        movement = Input.GetAxisRaw("Horizontal");

        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        if (Input.touchCount > 0)
        {
            // only get the first touch
            Touch touch = Input.touches[0];

            Vector2 touchPosition = touch.position;
            movement = (touchPosition.x < (pixelWidth / 2) ? -1 : 1);
        }

        #endif

        movement = movement * Time.deltaTime * GameConstants.anglesPerSecond * LevelSelectData._levelSelect._tunnelVelocity;
        angleOnCircle = (angleOnCircle + movement) % 360;

        float x = Mathf.Sin(angleOnCircle * Mathf.Deg2Rad) * GameConstants.playerDistanceFromCenter;
        float y = Mathf.Cos(angleOnCircle * Mathf.Deg2Rad) * GameConstants.playerDistanceFromCenter;

        rigidbodyComponent.MovePosition(new Vector2(x, y));
    }

    private void OnTriggerEnter2D()
    {
        dead = true;
        GameController._controller.GameOver();
    }
}
