using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [Header("Réglages du pool")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int initialSize = 20;
    [SerializeField] private float maxDistance = 40f; // distance max avant retour au pool

    private List<GameObject> pool = new List<GameObject>();
    private List<GameObject> activeBullets = new List<GameObject>(); //  nouveau : liste des balles actives

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitializePool();
    }

    private void Start()
    {
        //  Lancement de la coroutine de nettoyage
        StartCoroutine(CleanupRoutine());
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewBullet();
        }
    }

    private GameObject CreateNewBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        pool.Add(bullet);
        return bullet;
    }

    /// <summary>
    /// Retourne une balle prête à l’emploi.
    /// </summary>
    public GameObject GetBullet()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                activeBullets.Add(pool[i]); //  on ajoute dans la liste active
                return pool[i];
            }
        }

        GameObject newBullet = CreateNewBullet();
        activeBullets.Add(newBullet);
        return newBullet;
    }

    /// <summary>
    /// Retourne une balle dans le pool.
    /// </summary>
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        activeBullets.Remove(bullet); // 🔥 on retire de la liste active
    }

    /// <summary>
    
    /// </summary>
    IEnumerator CleanupRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f); // fréquence de nettoyage

            for (int i = activeBullets.Count - 1; i >= 0; i--)
            {
                GameObject bullet = activeBullets[i];

                if (bullet.activeInHierarchy)
                {
                    // Distance depuis l'origine de la scène (simple)
                    float distance = Vector3.Distance(bullet.transform.position, Vector3.zero);

                    if (distance > maxDistance)
                    {
                        ReturnBullet(bullet);
                    }
                }
            }
        }
    }
}
