using UnityEngine;
using System.Collections;

public class TargetSpawner : MonoBehaviour
{
    public float spawnInterval = 3f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Vérifier si on a le droit de spawn
            if (GameplayManager.Instance.currentTargets < GameplayManager.Instance.maxTargets)
            {
                SpawnTarget();
            }
        }
    }

    void SpawnTarget()
    {
        // Demande une cible au pool
        GameObject target = TargetPool.Instance.GetTarget();

        // Réinitialiser position (tu peux garder ton random)
        target.transform.position = new Vector3(
            Random.Range(-10f, 10f),
            1f,
            Random.Range(-10f, 10f)
        );

        target.transform.rotation = Quaternion.identity;
        target.SetActive(true);

        // Informer le GameplayManager
        GameplayManager.Instance.RegisterTargetSpawn();
    }
}
