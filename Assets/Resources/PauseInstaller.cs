using UnityEngine;
using Zenject;

public class PauseInstaller : MonoInstaller
{
    [SerializeField] private GamePause gamePause;
    public override void InstallBindings()
    {
        Container.Bind<IGamePauseService>().To<GamePause>().FromNewComponentOnNewPrefab(gamePause).AsSingle().NonLazy();
    }
}