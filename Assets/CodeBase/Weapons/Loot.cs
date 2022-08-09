using System.Collections.ObjectModel;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.Weapons
{
    public abstract class Loot : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Collect(GameObject to);
    }
}