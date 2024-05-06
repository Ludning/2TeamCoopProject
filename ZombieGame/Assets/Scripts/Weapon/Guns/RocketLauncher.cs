using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : BoltActionWeapon
{
    public override void AddAmmo()
    {
        invenAmmoCount += 5;
    }
}
