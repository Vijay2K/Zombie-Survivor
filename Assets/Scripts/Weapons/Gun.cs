using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject muzzuleFlashPrefab = null; 
    [SerializeField] private Transform bulletSpawnPoint = null;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private CameraShake camShake;
    [SerializeField] private int maxAmmoPerMag = 30;

    public int bulletsLeft = 200;
    public int currentAmmo;
    public event Action onChangeInAmmo;

    private Animator animator;
    private float reloadingTime = 3.2f;
    private float fireTime;
    private bool isReloading = false;
    private int  bulletsToLoad, bulletsDeducted;

    private void Awake()
    {
        currentAmmo = maxAmmoPerMag;
        bulletsLeft = 200;
    }

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(isReloading) { return; }

        if(currentAmmo == 0) {
            Reload();
        }

        if(fireTime < fireRate) {
            fireTime += Time.deltaTime;
        }

        if(onChangeInAmmo != null)
        {
            onChangeInAmmo();
        }
    }

    public void Shoot() {
        if(currentAmmo <= 0) { return; }
         
        if (fireTime < fireRate) { return; }

        AudioManager.Instance.Play("Shooting");

        currentAmmo--;

        GameObject muzzuleFlashInstance = Instantiate(muzzuleFlashPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation, bulletSpawnPoint);

        camShake.CamShake();

        GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
        Destroy(bulletInstance, 2f);

        Destroy(muzzuleFlashInstance, 2f);

        fireTime = 0f;
    }


    public void Reload () {
        if(bulletsLeft <= 0) { return; }
        if(currentAmmo < maxAmmoPerMag) {
            StartCoroutine(ReloadindTimer());
            animator.SetTrigger("Reload");
            AudioManager.Instance.Play("Reloading");
        }
    }

    public void IncreaseAmmos(int ammoCount)
    {
        bulletsLeft += ammoCount;
    }

    IEnumerator ReloadindTimer() {
        isReloading = true;

        GetComponent<PlayerMovement>().enabled = false;

        yield return new WaitForSeconds(reloadingTime);

        GetComponent<PlayerMovement>().enabled = true;

        bulletsToLoad = maxAmmoPerMag - currentAmmo;
        bulletsDeducted = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsDeducted;
        currentAmmo += bulletsDeducted;
        
        isReloading = false;
    }

    public bool GetIsReloading() {
        return isReloading;
    }
}
