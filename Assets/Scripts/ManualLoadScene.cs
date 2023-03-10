using Core.Player;
using Levels.ScriptableObjects.Sections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualLoadScene : MonoBehaviour
{
    [SerializeField] private LevelSectionData section;

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.TryGetComponent(out PlatformingController player);

        if (player) SceneManager.LoadScene(section.Scene);
    }
}
