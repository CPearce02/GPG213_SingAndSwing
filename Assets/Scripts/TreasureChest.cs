using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Structs;

public class TreasureChest : MonoBehaviour
{
    Animator animator;

    [SerializeField] float delayEventTime = 1;
    [SerializeField] bool isOpen = false;
    [SerializeField] UnityEvent onOpenEvent;
    [SerializeField] ParticleEvent particles;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpen) return;

        Open();
    }

    void Open()
    {
        animator.CrossFade("Open", 0);
        isOpen = true;
        StartCoroutine(DelayEvent());
    }

    IEnumerator DelayEvent()
    {
        yield return new WaitForSeconds(delayEventTime);
        onOpenEvent?.Invoke();
    }

    void InvokeParticles()
    {
        particles.Invoke();
    }

}
