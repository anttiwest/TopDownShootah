using UnityEngine;

public class WeaponHandling : MonoBehaviour {

    Vector3 swordSheatedPos;
    Quaternion swordSheatedRot;
    Vector3 swordEquippedPos;
    Quaternion swordEquippedRot;

    Vector3 gunInHolsterPos;
    Quaternion gunInHolsterRot;
    Vector3 gunEquippedPos;
    Quaternion gunEquippedRot;

    GameObject gun;
    GameObject sword;
    static Equipped equipped;

    private void Awake()
    {
        gun = GameObject.Find("scifigun_mudbox_test");
        sword = GameObject.Find("sword_lowpoly_scaled");

        gunInHolsterPos = new Vector3(0.059f, 0.838f, -0.155f);
        gunInHolsterRot = Quaternion.Euler(0.27f, 176.029f, -223.147f);

        swordEquippedPos = new Vector3(0.4f, 0.777f, 0.11f);
        swordEquippedRot = Quaternion.Euler(-45f, 0f, -90f);
        
        gunEquippedPos = gun.transform.position;
        gunEquippedRot = gun.transform.rotation;

        swordSheatedPos = sword.transform.position;
        swordSheatedRot = sword.transform.rotation;
        equipped = Equipped.Ranged;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleWeapons();
        }
    }

    void ToggleWeapons()
    {
        switch (equipped)
        {
            case Equipped.Ranged:
                equipped = Equipped.Melee;

                sword.transform.localPosition = swordEquippedPos;
                sword.transform.localRotation = swordEquippedRot;

                gun.transform.localPosition = gunInHolsterPos;
                gun.transform.localRotation = gunInHolsterRot;
                break;

            case Equipped.Melee:
                equipped = Equipped.Ranged;

                gun.transform.localPosition = gunEquippedPos;
                gun.transform.localRotation = gunEquippedRot;

                sword.transform.localPosition = swordSheatedPos;
                sword.transform.localRotation = swordSheatedRot;
                break;
        }
    }

    public static Equipped GetEquippedWeapon()
    {
        return equipped;
    }
}
