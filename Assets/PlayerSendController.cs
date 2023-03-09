using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Player;
using Events;

public class PlayerSendController : MonoBehaviour
{

    [SerializeField][ReadOnly] PlatformingController player;

    private void Awake()
    {
        player = gameObject.GetComponent<PlatformingController>();
    }

    private void OnEnable()
    {
        GameEvents.onRequestPlayerEvent += SendPlayer;
    }

    private void OnDisable()
    {
        GameEvents.onRequestPlayerEvent -= SendPlayer;
    }

    // We might need to refactor when we have two players

    private void SendPlayer() => GameEvents.onSendPlayerEvent?.Invoke(player);

}
