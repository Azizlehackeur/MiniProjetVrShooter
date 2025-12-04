using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

public class GunLoader : MonoBehaviour
{
    async void Start()
    {
        Debug.Log("🔥 GunLoader START !");

        string key = PlatformDetector.GetPlatform() == PlatformType.PCVR
            ? "Gun_PCVR"
            : "Gun_Quest";

        Debug.Log("🔫 Chargement du gun : " + key);

        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(key);
        GameObject gunPrefab = await handle.Task;

        if (gunPrefab == null)
        {
            Debug.LogError("❌ Impossible de charger le gun.");
            return;
        }

        // 🔥 Spawn au sol
        Vector3 spawnPos = new Vector3(0, 1, 0);
        GameObject gun = Instantiate(gunPrefab, spawnPos, Quaternion.identity);

        Debug.Log("✔️ Gun instancié au sol : " + gun.name);
    }
}
