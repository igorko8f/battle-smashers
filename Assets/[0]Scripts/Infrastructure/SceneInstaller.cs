using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public Transform PlayerPosition;
    public PlayerController PlayerPrefab;
    public MoneyManager MoneyManager;
    
    public override void InstallBindings()
    {
        BindPlayer();
        BindMoneyManager();
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