using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FXLoader : MonoBehaviour
{
    public static FXLoader Instance;

    private GameObject muzzleFX;
    private GameObject impactFX;

    private AsyncOperationHandle<GameObject> muzzleHandle;
    private AsyncOperationHandle<GameObject> impactHandle;

    private PlatformType currentPlatform;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        currentPlatform = PlatformDetector.GetPlatform();
        Debug.Log("Platform detected : " + currentPlatform);
    }

    public async Task LoadFX()
    {
        string muzzleKey = currentPlatform == PlatformType.PCVR ? "Muzzle_PCVR" : "Muzzle_Quest";
        string impactKey = currentPlatform == PlatformType.PCVR ? "Impact_PCVR" : "Impact_Quest";

        muzzleHandle = Addressables.LoadAssetAsync<GameObject>(muzzleKey);
        impactHandle = Addressables.LoadAssetAsync<GameObject>(impactKey);

        muzzleFX = await muzzleHandle.Task;
        impactFX = await impactHandle.Task;
    }

    public GameObject GetMuzzleFX() => muzzleFX;
    public GameObject GetImpactFX() => impactFX;

    public void UnloadFX()
    {
        Addressables.Release(muzzleHandle);
        Addressables.Release(impactHandle);
    }
}
