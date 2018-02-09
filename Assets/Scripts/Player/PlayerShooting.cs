﻿using UnityEngine;

public class PlayerShooting : Player {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    float coolDown = 0;
    float fireRate = 0.3f;
    float shotSpeed = 30f;
    ParticleSystem shootParticles;
    bool isShooting;


    void Awake()
    {
        shootParticles = GetComponentInChildren<ParticleSystem>();
        isShooting = false;
    }

    void FixedUpdate()
    {
        coolDown -= Time.deltaTime;
        Debug.Log("playershooting");
        if (Input.GetMouseButton(0))
        {
            if (coolDown < 0)
            {
                ShootObjects();
                coolDown = fireRate;
            }
        }
    }

    void ShootParticles()
    {
        if (Input.GetMouseButton(0) && !isShooting)
        {
            shootParticles.Play();
            isShooting = true;
        }
        else if (isShooting && !Input.GetMouseButton(0))
        {
            shootParticles.Stop();
            isShooting = false;
        }
        shootParticles.transform.position = new Vector3(transform.position.x, shootParticles.transform.position.y, transform.position.z);
    }

    void ShootObjects()
    {
        bulletSpawn.transform.position = new Vector3(transform.position.x, bulletSpawn.transform.position.y, transform.position.z);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * shotSpeed;
        Destroy(bullet, 2f);
    }
}
