using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class Teleport : MonoBehaviour
{
    [SerializeField] string sceneAddress;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadSceneAsync();
        }
    }

    void LoadSceneAsync()
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAddress, UnityEngine.SceneManagement.LoadSceneMode.Single, activateOnLoad: false);
        handle.Completed += OnSceneLoaded;
    }

    void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            SceneInstance sceneInstance = handle.Result;
            sceneInstance.ActivateAsync();
            Debug.Log("Scene Loaded: " + sceneInstance.Scene.name);
        }
        else
        {
            Debug.LogError("Loading Scene Failed!");
            Debug.LogError("Error: " + handle.OperationException);
        }
    }
}