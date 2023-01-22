using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI bulletLeftAmmoText;
    [SerializeField] private TextMeshProUGUI grenadeCountText;
    
    private Gun gun;
    private Grenade grenade;

    private void Start()
    {
        gun = FindObjectOfType<Gun>();
        grenade = FindObjectOfType<Grenade>();

        gun.onChangeInAmmo += UpdateAmmoUI;
        grenade.onGrenadeCountChange += UpdateGrenadeUI;
    }

    private void OnDisable()
    {
        gun.onChangeInAmmo -= UpdateAmmoUI;
        grenade.onGrenadeCountChange -= UpdateGrenadeUI;
    }

    private void UpdateAmmoUI()
    {
        if(currentAmmoText == null || bulletLeftAmmoText == null)
        {
            Debug.LogWarning("Refernce Missing pls check the inspector");
            return;
        }

        currentAmmoText.text = gun.currentAmmo.ToString();
        bulletLeftAmmoText.text = gun.bulletsLeft.ToString();
    }

    private void UpdateGrenadeUI()
    {
        if(grenadeCountText == null)
        {
            Debug.LogWarning("Refernce Missing pls check the inspector");
            return;
        }

        grenadeCountText.text = grenade.grenadenumber.ToString();
    }

}
