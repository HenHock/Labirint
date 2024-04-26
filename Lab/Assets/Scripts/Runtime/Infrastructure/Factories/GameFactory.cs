using Runtime.Configs;
using Runtime.Services.Providers;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure.Factories
{
    public sealed class GameFactory : BaseFactory
    {
        private GameObject _hero;
        
        public GameFactory(DiContainer container, IAssetProvider assetProvider) : base(container, assetProvider)
        {}

        public GameObject CreateHero() => 
            _hero = InstantiateAsset(AssetProviderKey.Hero, Vector2.zero, Quaternion.identity);

        public GameObject CreateEnemy(GameObject prefab, Vector2 position, Transform parent) => 
            InstantiatePrefab(prefab, position, default, parent);
    }
}