using Runtime.Configs;

namespace Runtime.Services.Providers.ConfigsProvider
{
    public sealed class ConfigProvider : Provider, IConfigProvider
    {
        public override void Initialize()
        {
            Add(Load<GameConfig>(ConfigsPath.Game));
        }
    }

}