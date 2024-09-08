using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100.0f;
    public float damage = 30.0f;
    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] ammo ammoSlot;
    [SerializeField] AmmoType ammoType;


    [SerializeField] float timeBetweenShots;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine(Shoot());

        }
    }
    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            //PlayMuzzleEffect();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;



    }

    //void PlayMuzzleEffect()
    //{
    //    muzzleFlash.Play();
    //}

    void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) { return; }
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }





}