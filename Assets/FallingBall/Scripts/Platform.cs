using UnityEngine;


namespace FallingBall
{
    public class Platform : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] private LayerMask wallLayer;
        private PlatformGenerator platformGenerator;
        private Vector2 initPosition;



        private void Start()
        {
            platformGenerator = PlatformGenerator.Instance;
            initPosition = platformGenerator.spawnPoint.position;
            rb = GetComponent<Rigidbody2D>();

        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + Vector2.up * platformGenerator.currentSpeed * Time.fixedDeltaTime);
        }

        public float GetMoveDistance()
        {
            return Vector2.Distance(initPosition, transform.position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the collision is with the target layer
            if ((wallLayer.value & (1 << collision.gameObject.layer)) != 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
