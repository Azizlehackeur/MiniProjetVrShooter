using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GunShooting : MonoBehaviour
{
    public Transform bulletSpawn;
    public float shootingForce = 1000f;
    public float fireRate = 0.2f;
    public XRGrabInteractable grabInteractable;

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
        //  Récupérer une balle du pool (Task 2 compatible)
        GameObject bullet = BulletPool.Instance.GetBullet();

        // Reset & repositionnement
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.rotation = bulletSpawn.rotation;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        bullet.SetActive(true);

        //  FX d'apparition du tir via Addressables
        Instantiate(
            FXLoader.Instance.GetMuzzleFX(),
            bulletSpawn.position,
            bulletSpawn.rotation,
            bulletSpawn
        );

        // Force de propulsion
        rb.AddForce(bulletSpawn.forward * shootingForce, ForceMode.Impulse);
    }
}
