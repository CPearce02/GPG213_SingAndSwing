using Enemies.EnemyStates;
using Enemies.ScriptableObjects;
using Interfaces;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    [SerializeField] public EnemyData enemyData;
    public IState CurrentState { get; set; }
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
    [SerializeField][ReadOnly] string stateName;
    
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        ChangeState(new IdleState());
    }
    
    void Update()
    {
        CurrentState.Execute(this);   
        stateName = CurrentState.ToString();
    }

    private void ChangeState(IState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = newState;
        CurrentState.Enter(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CurrentState.OnTriggerEnter2D(other);
    }

    
}
