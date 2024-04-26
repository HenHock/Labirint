using Runtime.Configs;
using UnityEngine;

namespace Runtime.Services.Providers.AssetsProvider
{
    public sealed class AssetProvider : Provider, IAssetProvider
    {
        public override void Initialize()
        {
            Add(AssetProviderKey.Hero, Load<GameObject>(AssetsPath.Hero));
            Add(AssetProviderKey.UIRoot, Load<Transform>(AssetsPath.UIRoot));
        }
    }
}
