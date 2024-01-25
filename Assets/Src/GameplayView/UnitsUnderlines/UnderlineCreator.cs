namespace Src.GameplayView.UnitsUnderlines
{
    public class UnderlineCreator : IUnderlineCreator
    {
        private readonly IUnderlinePrefabProvider _underlinePrefabProvider;
        private readonly IInstantiator _instantiator;

        public UnderlineCreator(IUnderlinePrefabProvider underlinePrefabProvider, IInstantiator instantiator)
        {
            _underlinePrefabProvider = underlinePrefabProvider;
            _instantiator = instantiator;
        }

        public Underline GetUnderline()
        {
            var underlinePrefab = _underlinePrefabProvider.GetUnderlinePrefab();
            var underline = _instantiator.Instantiate(underlinePrefab);
            return underline;
        }
    }
}