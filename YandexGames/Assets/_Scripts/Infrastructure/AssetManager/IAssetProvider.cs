using _Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Scripts.Infrastructure.AssetManager
{
    public interface IAssetProvider: IService
    {
        GameObject Instantiate(string path, Transform parent);
        GameObject InstantiateAt(string path, Vector3 position);
    }
}