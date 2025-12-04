using System.Collections.Generic;
using UnityEngine;

public class TargetPool : MonoBehaviour
{
    public static TargetPool Instance;

    public GameObject targetPrefab;
    public int initialSize = 10;

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewTarget();
        }
    }

    GameObject CreateNewTarget()
    {
        GameObject target = Instantiate(targetPrefab);
        target.SetActive(false);
        pool.Add(target);
        return target;
    }

    public GameObject GetTarget()
    {
        foreach (GameObject target in pool)
        {
            if (!target.activeInHierarchy)
            {
                return target;
            }
        }

        return CreateNewTarget();
    }

    public void ReturnTarget(GameObject target)
    {
        target.SetActive(false);
    }
}
