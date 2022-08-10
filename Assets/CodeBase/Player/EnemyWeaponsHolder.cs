namespace CodeBase.Player
{
    public class EnemyWeaponsHolder : WeaponsHolder
    {
        public bool WeaponInEmpty()
        {
            return CurrentWeapon == null;
        }
    }
}