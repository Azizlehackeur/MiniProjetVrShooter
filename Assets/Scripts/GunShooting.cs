using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Management;

public class GunShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // Préfabriqué de la balle
    public Transform bulletSpawn;     // Position de spawn de la balle
    public float shootingForce = 1000f; // Force de propulsion de la balle
    public float fireRate = 0.2f;     // Temps entre deux tirs (en secondes)
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    public ParticleSystem muzzle;   // Ancien système, devenu inutile mais je te le laisse pour éviter des erreurs si tu veux l’enlever plus tard

    private XRInputActions inputActions;
    private bool isShooting = false;
    private float nextFireTime = 0f;
    private bool isGunHeld = false;

    void Awake()
    {
        inputActions = new XRInputActions();
    }

    void OnEnable()
    {
        inputActions.XRControls.Enable();

        inputActions.XRControls.Shoot.started += OnShootStart;
        inputActions.XRControls.Shoot.canceled += OnShootStop;

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        inputActions.XRControls.Disable();

        inputActions.XRControls.Shoot.started -= OnShootStart;
        inputActions.XRControls.Shoot.canceled -= OnShootStop;

        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    void Update()
    {
        if (isGunHeld && isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void OnShootStart(InputAction.CallbackContext context)
    {
        isShooting = true;
    }

    void OnShootStop(InputAction.CallbackContext context)
    {
        isShooting = false;
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGunHeld = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGunHeld = false;
    }

    void Shoot()
    {
        // 🔥 Récupérer une balle du pool
        GameObject bullet = BulletPool.Instance.GetBullet();

        // Reset & repositionnement
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.rotation = bulletSpawn.rotation;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        bullet.SetActive(true);

        // 🔥 FX Addressables (PCVR ou Quest automatiquement)
        Instantiate(
            FXLoader.Instance.GetMuzzleFX(),
            bulletSpawn.position,
            bulletSpawn.rotation,
            bulletSpawn  // permet au FX de suivre le mouvement du canon
        );

        // Force de tir
        rb.AddForce(bulletSpawn.forward * shootingForce);
    }
}
