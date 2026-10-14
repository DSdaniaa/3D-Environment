using UnityEngine;

public class AIFactory
{
    private GameObject aiPrefab;

    public AIFactory(GameObject aiPrefab)
    {
        this.aiPrefab = aiPrefab;
    }

    public GameObject CreateAI(Vector3 position)
    {
        return Object.Instantiate(
            aiPrefab,
            position,
            Quaternion.identity
        );
    }
}