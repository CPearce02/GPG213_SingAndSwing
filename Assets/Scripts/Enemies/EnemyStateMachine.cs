using Enemies.BossStates;
using Enemies.EnemyStates;
using Enemies.ScriptableObjects;
using Interfaces;
using UnityEngine;

namespace Enemies
{
    public class EnemyStateMachine : MonoBehaviour
    {
        IState CurrentState { get; set; }
        [SerializeField][ReadOnly] string stateName;
        
        [Header("Settings")]
        [SerializeField] public EnemyData enemyData;
        public Animator animator;
        [field:SerializeField] public Rigidbody2D Rb { get; private set; }
        [field:SerializeField] public LayerMask PlayerLayer { get; private set; }

        void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            
            // if enemyData.type is Boss, then change state to BossIdleState else change state to IdleState
            if(enemyData.type == Enums.EnemyType.Boss)
            {
                ChangeState(new BossIdleState());
            }
            else
            {
                ChangeState(new IdleState());
            }
        }
    
        void Update()
        {
            CurrentState.Execute(this);   
            stateName = CurrentState.ToString();
        }

        public void ChangeState(IState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }

            CurrentState = newState;
            CurrentState.Enter(this);
        }


    }
}
