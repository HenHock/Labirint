namespace Runtime.Services.Providers.AssetsProvider
{
    public interface IAssetProvider : IProvider
    {
        public void LoadAssets(ContextType contextType);
        public void UnloadAssets(ContextType contextType);
    }
}