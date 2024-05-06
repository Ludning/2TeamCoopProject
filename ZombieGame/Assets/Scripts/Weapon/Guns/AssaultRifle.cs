using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AssaultRifle : AutomaticWeapon
{
    public override void AddAmmo()
    {
        invenAmmoCount += 30;
    }
}
