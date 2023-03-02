using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ManualLoadScene : MonoBehaviour
{
    public SceneAsset scene;

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.TryGetComponent(out PlatformingController player);

        if (player) SceneManager.LoadScene(scene.name);
    }
}
