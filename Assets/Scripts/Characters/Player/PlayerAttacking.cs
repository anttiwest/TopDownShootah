using UnityEngine;

public class PlayerAttacking : Player {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    float coolDown = 0;
    float fireRate = 0.3f;
    float shotSpeed = 30f;

    void FixedUpdate()
    {
        coolDown -= Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            Debug.Log("equipped: " + WeaponHandling.GetEquippedWeapon());
            switch (WeaponHandling.GetEquippedWeapon())
            {
                case Equipped.Ranged:
                    if (coolDown < 0)
                    {
                        ShootObjects();
                        coolDown = fireRate;
                    }
                    break;

                case Equipped.Melee:
                    Debug.Log("MELEE");
                    break;
            }
        }
    }

    void ShootObjects()
    {
        bulletSpawn.transform.position = new Vector3(transform.position.x, bulletSpawn.transform.position.y, transform.position.z);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * shotSpeed;
        Destroy(bullet, 2f);
    }
}
