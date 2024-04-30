using System;
using Runtime.Configs;
using Runtime.Configs.Infrastructure;
using Runtime.Services.Providers.AssetsProvider;

namespace Runtime.Services.Providers.ConfigsProvider
{
    public sealed class ConfigProvider : Provider, IConfigProvider
    {
        public void LoadConfigs(ContextType contextType)
        {
            switch (contextType)
            {
                case ContextType.Boot:
                    LoadBootConfigs();
                    break;
                case ContextType.Gameplay:
                    LoadGameplayConfigs();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(contextType), contextType, null);
            }
        }

        public void UnloadConfigs(ContextType contextType)
        {
            switch (contextType)
            {
                case ContextType.Boot:
                    UnloadBootConfigs();
                    break;
                case ContextType.Gameplay:
                    UnloadGameplayConfigs();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(contextType), contextType, null);
            }
        }

        private void LoadBootConfigs()
        {
            Add(Load<GameConfig>(ConfigsPath.Game));
        }

        private void UnloadBootConfigs()
        {
            Remove<GameConfig>();
        }

        private void LoadGameplayConfigs()
        {
            Add(Load<HeroConfig>(ConfigsPath.Hero));
            Add(LoadAll<LevelConfig>(ConfigsPath.LevelFolder));
        }

        private void UnloadGameplayConfigs()
        {
            Remove<HeroConfig>();
            Remove<LevelConfig[]>();
        }
    }
}