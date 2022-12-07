using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerWeaponInstaller : MonoInstaller
{
    [SerializeField] private PlayerWeapon playerWeapon;
    public override void InstallBindings()
    {
        Container.Bind<IPlayerWeapon>().To<PlayerWeapon>().FromNewComponentOnNewPrefab(playerWeapon).AsSingle().NonLazy();
    }
}
