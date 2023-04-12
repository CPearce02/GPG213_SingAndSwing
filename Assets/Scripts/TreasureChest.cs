using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Player;
using Events;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] string nextScene = "MainMenu";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.TryGetComponent(out PlatformingController player);

        if (player) StartCoroutine(EndGameSequence());
    }

    IEnumerator EndGameSequence()
    {
        GameEvents.onPlayerFreezeEvent?.Invoke();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextScene);
    }
}
