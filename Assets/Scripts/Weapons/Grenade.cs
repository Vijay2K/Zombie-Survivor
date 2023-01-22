using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private GameObject grenadePrefab = null;
    [SerializeField] private Transform firePoint = null;
    [SerializeField] private float throwForce = 300f;

    public int grenadenumber = 5; //ACCESSING OUTSIDE THE CLASS
    public event Action onGrenadeCountChange;

    private bool isThrowing = false;

    private void Update() {
        if(onGrenadeCountChange != null)
        {
            onGrenadeCountChange.Invoke();
        }
    }

    public void Throw() {
        if(grenadenumber <= 0) { return;  }
        StartCoroutine(StartThrowAnimation());
        GetComponent<Animator>().SetTrigger("Throw");
    }

    //ANIMATION TRIGGER EVENT
    private void ThrowGrenade() {
        if(grenadenumber <= 0) { return; }
        grenadenumber--;
        
        GameObject grenadeInstane = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
        Rigidbody grrenadeRigidbody = grenadeInstane.GetComponent<Rigidbody>();
        grrenadeRigidbody.AddForce(firePoint.forward * throwForce, ForceMode.VelocityChange);

    }

    IEnumerator StartThrowAnimation() {
        isThrowing = true;

        GetComponent<PlayerController>().enabled = false;

        yield return new WaitForSeconds(3.5f);

        GetComponent<PlayerController>().enabled = true;

        isThrowing = false;
    }

    public bool GetIsThrowing() {
        return isThrowing;
    }

}
