using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetsManager : Singleton<AssetsManager>
{
    [Header("Asset Reference")]
    public AssetReference _rocket;
    public AssetReference _yellowProjectile;
    public AssetReference _redProjectile;
    public AssetReference _utility;

    public void InitPrefab(AssetReference asset, Action<GameObject> onLoaded)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(asset);
        handle.Completed += (AsyncOperationHandle<GameObject> task) =>
        {
            if (task.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject prefab = Instantiate(task.Result);
                onLoaded?.Invoke(prefab);
            }
            else
            {
                Debug.LogError($"Failed to load {asset}");
            }
        };
    }
}
