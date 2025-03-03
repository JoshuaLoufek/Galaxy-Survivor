using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages weapons on the player. Allows player to add and upgrade weapons.
public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer; // stores all instantiated weapons in the hierarchy

    [SerializeField] WeaponData startingWeapon;

    private void Start()
    {
        AddWeapon(startingWeapon);
    }

    public void AddWeapon(WeaponData weaponData)
    {
        GameObject newWeapon = Instantiate(weaponData.weaponPrefab, weaponObjectsContainer);
        newWeapon.GetComponent<WeaponBase>().InitializeWeaponData(weaponData);
    }
}
