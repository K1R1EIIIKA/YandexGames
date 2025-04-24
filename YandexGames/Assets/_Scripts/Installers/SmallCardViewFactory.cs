using _Scripts.View;
using Zenject;

public class SmallCardViewFactory
{
    private readonly DiContainer _container;
    private readonly SmallCardView _prefab;

    public SmallCardViewFactory(DiContainer container, SmallCardView prefab)
    {
        _container = container;
        _prefab = prefab;
    }

    public SmallCardView Create()
    {
        return _container.InstantiatePrefabForComponent<SmallCardView>(_prefab);
    }
}