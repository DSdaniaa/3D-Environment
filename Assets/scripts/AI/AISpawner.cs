using UnityEngine;

public class AISpawner : MonoBehaviour
{
    [SerializeField] private GameObject aiPrefab;
    [SerializeField] private int numberOfAI = 10;
    [SerializeField] private float spawnRadius = 8f;
    [SerializeField] private float respawnInterval = 2f;

    private AIFactory aiFactory;
    private AIPool aiPool;

    private float spawnTimer;

    private void Start()
    {
        // Create the Factory
        aiFactory = new AIFactory(aiPrefab);

        // Create the Pool
        aiPool = new AIPool(
            aiFactory,
            numberOfAI
        );

        // Spawn the initial AI
        for (int i = 0; i < numberOfAI; i++)
        {
            SpawnAI();
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= respawnInterval)
        {
            SpawnAI();

            spawnTimer = 0f;
        }
    }

    private void SpawnAI()
    {
        GameObject ai = aiPool.GetAI();

        // Pool is full / no inactive AI available
        if (ai == null)
            return;

        Vector2 randomPoint =
            Random.insideUnitCircle * spawnRadius;

        Vector3 spawnPosition =
            transform.position + new Vector3(
                randomPoint.x,
                0f,
                randomPoint.y
            );

        ai.transform.position = spawnPosition;
    }
}