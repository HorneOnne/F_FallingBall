using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FallingBall
{
    public class PlatformGenerator : MonoBehaviour
    {
        public static PlatformGenerator Instance;

        [SerializeField] private List<Platform> platforms;
        public Transform spawnPoint;
        [Space(10)]
        [Header("Limit border")]
        [Range(-5,5)]
        [SerializeField] private float leftBorder = 2.0f;
        [Range(-5, 5)]
        [SerializeField] private float rightBorder = 3.0f;

        [Header("Rot")]
        [SerializeField] private float randomRotMin = -45f;
        [SerializeField] private float randomRotMax = 45f;

        [Header("Properties")]
        public float minSpeed = 10;
        public float maxSpeed = 20;
        public float currentSpeed;
        public float timeToReachMaxSpeed = 300f;    // 5 minutes
        [Space(5)]
        public float minDistanceEachStage = 2;
        public float maxDistanceEachStage = 3;
        private float distanceEachStage;


        // Cached
        [SerializeField] private Platform lastPlatformSpawned = null;
        private int minGroup = 2;
        private int maxGroup = 5;
        private float elapsedTime;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SpawnPlatform();
            distanceEachStage = Random.Range(minDistanceEachStage, maxDistanceEachStage);
            currentSpeed = minSpeed;

            elapsedTime = 0;
        }


        private void Update()
        {
            if (GameplayManager.Instance.currentState != GameplayManager.GameState.PLAYING) return;
            elapsedTime += Time.deltaTime;

            if (lastPlatformSpawned.GetMoveDistance() <= distanceEachStage) return;
            currentSpeed = CalculateSpeedIncrease(elapsedTime);
            distanceEachStage = Random.Range(minDistanceEachStage, maxDistanceEachStage);
            SpawnPlatform();  
        }
  

        #region Spike Generate
        private void SpawnPlatform(float yOffset = 0)
        {
            Platform platform = GetRandomPlatform();
            Vector2 position = (Vector2)spawnPoint.position + GetRandomXOffset();
            position.y += yOffset;

            Vector3 rotation = Vector3.zero;
            if (Random.Range(0f, 1f) < 0.3f)
            {
                // 30% platform can rotate
                rotation = GetRandomRotation();
            }
            lastPlatformSpawned = Instantiate(platform, position, Quaternion.Euler(rotation));
        }
  
   

        #endregion


        public Platform GetRandomPlatform()
        {
            return platforms[Random.Range(0, platforms.Count)];
        }

        public Vector2 GetRandomXOffset()
        {
            return new Vector2(Random.Range(leftBorder, rightBorder), 0);
        }

        public Vector3 GetRandomRotation()
        {
            return new Vector3(0,0, Random.Range(randomRotMin, randomRotMax));
        }


        #region difficulty
        private float CalculateSpeedIncrease(float elapsedTime)
        {
            // Calculate the percentage of time elapsed
            float timePercentage = Mathf.Clamp01(elapsedTime / timeToReachMaxSpeed);

            // Use the percentage to calculate the current speed within the range
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, timePercentage);

            return currentSpeed;
        }
        #endregion
    }
}
