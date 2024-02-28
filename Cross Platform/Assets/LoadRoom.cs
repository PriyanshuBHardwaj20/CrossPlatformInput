using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadRoom : MonoBehaviour
{
    [SerializeField] AssetReferenceGameObject RoomAssetReference;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomAssetReference.LoadAssetAsync().Completed += OnAddressableLoaded;
        }
    }


    void OnAddressableLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(handle.Result);
        }
        else
        {
            Debug.LogError("Loading Asset Failed!");
        }
    }
}