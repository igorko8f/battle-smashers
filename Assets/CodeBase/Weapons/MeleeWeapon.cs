using UnityEngine;

namespace CodeBase.Weapons
{
    public class MeleeWeapon : Weapon
    {
        public override void Shot()
        {
            print("MELEE SHOT!");
        }
    }
}