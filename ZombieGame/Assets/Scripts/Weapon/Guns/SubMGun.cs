using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMGun : AutomaticWeapon
{
    public override void AddAmmo()
    {
        invenAmmoCount += 60;
    }
}
