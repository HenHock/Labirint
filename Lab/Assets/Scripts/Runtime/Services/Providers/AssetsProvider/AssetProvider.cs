using System;
using Cinemachine;
using Runtime.Configs;
using UnityEngine;

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
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(contextType), contextType, null);
            }
        }

        private void LoadGameplayAssets()
        {
            Add(AssetProviderKey.Hero, Load<GameObject>(AssetsPath.Hero));
            Add(Load<CinemachineVirtualCamera>(AssetsPath.ForwardCamera));
            //       Add(AssetProviderKey.UIRoot, Load<Transform>(AssetsPath.UIRoot));
        }

        private void UnloadGameplayAssets()
        {
            Remove<GameObject>(AssetProviderKey.Hero);
        }
    }
}
