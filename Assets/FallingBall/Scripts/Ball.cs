using UnityEngine;

namespace FallingBall
{
    public class Ball : MonoBehaviour
    {
        public float forceMagnitude = 10f; // Adjust this value to control the force applied  
        private Rigidbody2D rb2D;

        [Header("Deadth Border")]
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;

        [Header("References")]
        public LayerMask platformLayer;

        private bool isInside;
        private float timerCheckGameOver = 0.0f;

        // Cached
        private GameplayManager gameplayManager;
        private Camera mainCam;

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            gameplayManager = GameplayManager.Instance;
            mainCam = Camera.main;
        }

        private void Update()
        {           
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
                // Check if the mouse click is on the left or right side of the ball
                if (mousePosition.x < transform.position.x)
                {
                    // Apply force to the left side
                    Vector2 force = Vector2.left * forceMagnitude;
                    rb2D.AddForce(force, ForceMode2D.Force);
                }
                else
                {
                    // Apply force to the right side
                    Vector2 force = Vector2.right * forceMagnitude;
                    rb2D.AddForce(force, ForceMode2D.Force);
                }
            }

            if(Time.time - timerCheckGameOver > 0.7f)
            {
                timerCheckGameOver = Time.time;
                isInside = CheckBallOutOfBorder();
                if (isInside == false)
                {
                    gameplayManager.ChangeGameState(GameplayManager.GameState.GAMEOVER);
                    SoundManager.Instance.PlaySound(SoundType.OutOfBound, false);
                }
                    
            }
            
        }

        private void FixedUpdate()
        {
            ClamVelocity(15);         
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & platformLayer.value) != 0)
            {
                if(rb2D.velocity.y > 0.21f)
                    SoundManager.Instance.PlaySound(SoundType.BallCollided, false);
            }
        }

        private void ClamVelocity(float maxVelocity)
        {
            // Get the current velocity
            Vector2 velocity = rb2D.velocity;

            // Clamp the x and y components of the velocity to the maximum velocity
            velocity.x = Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity);
            velocity.y = Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity);

            // Update the velocity with the clamped values
            rb2D.velocity = velocity;
        }

        private bool CheckBallOutOfBorder()
        {
            return(transform.position.x >= minX && transform.position.x <= maxX 
                && transform.position.y >= minY && transform.position.y <= maxY);
        }
    }
}
