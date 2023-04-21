using Core.Player;
using Events;
using Levels.ScriptableObjects.Sections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualLoadScene : MonoBehaviour
{
    [SerializeField] private SectionData section;

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.TryGetComponent(out PlatformingController player);

        if (player)
        {
            GameEvents.onPlayerFreezeEvent?.Invoke();
            SceneManager.LoadScene(section.Scene);
        }
    }
}
