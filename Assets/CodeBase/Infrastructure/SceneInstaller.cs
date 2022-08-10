using CodeBase.Player;
using CodeBase.Utils;
using CodeBase.Weapons;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        public Transform PlayerPosition;
        public PlayerController PlayerPrefab;
        public MoneyManager MoneyManager;
        public LootSpawner LootSpawner;

        public override void InstallBindings()
        {
            BindPlayer();
            BindMoneyManager();
            BindLootSpawner();
        }

        private void BindLootSpawner()
        {
            Container
                .Bind<LootSpawner>()
                .FromInstance(LootSpawner)
                .AsSingle();
        }

        private void BindMoneyManager()
        {
            Container
                .Bind<MoneyManager>()
                .FromInstance(MoneyManager)
                .AsSingle();
        }

        private void BindPlayer()
        {
            var playerController = Container
                .InstantiatePrefabForComponent<PlayerController>(PlayerPrefab, PlayerPosition.position, Quaternion.identity, null);

            Container
                .Bind<PlayerController>()
                .FromInstance(playerController)
                .AsSingle();
        }
    }
}