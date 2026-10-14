using System.Collections.Generic;
using UnityEngine;

public class AIPool
{
    private List<GameObject> aiPool = new List<GameObject>();

    private AIFactory aiFactory;

    public AIPool(AIFactory factory, int poolSize)
    {
        aiFactory = factory;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject ai = aiFactory.CreateAI(Vector3.zero);

            ai.SetActive(false);

            aiPool.Add(ai);
        }
    }

    public GameObject GetAI()
    {
        foreach (GameObject ai in aiPool)
        {
            if (!ai.activeInHierarchy)
            {
                ai.SetActive(true);
                return ai;
            }
        }

        return null;
    }
}