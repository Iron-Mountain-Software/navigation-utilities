using UnityEngine;
using UnityEngine.AI;

namespace IronMountain.NavigationUtilities
{
    public class NavMeshAgentFollowTransform : MonoBehaviour
    {
        [SerializeField] public NavMeshAgent navMeshAgent;
        [SerializeField] public Transform target;
        
        private void OnValidate()
        {
            if (!navMeshAgent) navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Awake()
        {
            OnValidate();
        }

        private void Update()
        {
            if (!navMeshAgent || !target) return;
            navMeshAgent.SetDestination(target.position);
        }
    }
}