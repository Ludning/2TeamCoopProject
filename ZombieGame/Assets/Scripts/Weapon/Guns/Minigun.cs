using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : AutomaticWeapon
{
    public override void AddAmmo()
    {
        invenAmmoCount += 50;
    }
}
