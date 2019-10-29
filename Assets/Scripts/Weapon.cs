using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Shoot();
        }
    }

    private void Shoot()
    {
        audioSource.Play();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
