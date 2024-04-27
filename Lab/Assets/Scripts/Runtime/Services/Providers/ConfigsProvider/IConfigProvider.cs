using Runtime.Services.Providers.AssetsProvider;

namespace Runtime.Services.Providers.ConfigsProvider
{
    public interface IConfigProvider : IProvider
    {
        public void LoadConfigs(ContextType contextType);
        public void UnloadConfigs(ContextType contextType);
    }
}