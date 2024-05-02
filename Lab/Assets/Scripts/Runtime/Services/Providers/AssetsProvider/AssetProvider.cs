using System;
using Cinemachine;
using Runtime.Configs.Infrastructure;
using Runtime.Logic.Gameplay;
using Runtime.Logic.UI;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Runtime.Services.Providers.AssetsProvider
{
    public sealed class AssetProvider : Provider, IAssetProvider
    {
        public void LoadAssets(ContextType contextType)
        {
            switch (contextType)
            {
                case ContextType.Boot:
                    break;
                case ContextType.Gameplay:
                    LoadGameplayAssets();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(contextType), contextType, null);
            }
        }

        public void UnloadAssets(ContextType contextType)
        {
            switch (contextType)
            {
                case ContextType.Boot:
                    break;
                case ContextType.Gameplay:
                    UnloadGameplayAssets();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(contextType), contextType, null);
            }
        }

        private void LoadGameplayAssets()
        {
            Add(AssetProviderKey.Hero, Load<GameObject>(AssetsPath.Hero));
            Add(AssetProviderKey.UIRoot, Load<Transform>(AssetsPath.UIRoot));
            Add(Load<GameplayHUDDisplay>(AssetsPath.GameplayHUD));
            Add(Load<FinishZone>(AssetsPath.FinishZone));
            Add(Load<CinemachineVirtualCamera>(AssetsPath.ForwardCamera));
        }

        private void UnloadGameplayAssets()
        {
            Remove<Transform>(AssetProviderKey.UIRoot);
            Remove<GameObject>(AssetProviderKey.Hero);
            Remove<CinemachineVirtualCamera>();
            Remove<GameplayHUDDisplay>();
        }
    }
}
