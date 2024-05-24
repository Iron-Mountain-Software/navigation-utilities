using UnityEngine;
using UnityEngine.AI;

namespace IronMountain.NavigationUtilities
{
    public class NavMeshAgentFollowTransform : MonoBehaviour
    {
        public enum RotationType
        {
            None,
            LookAtTarget,
            AlignWithTarget,
        }
        
        [SerializeField] public NavMeshAgent navMeshAgent;
        [SerializeField] public Transform target;
        [SerializeField] public RotationType rotationType = RotationType.LookAtTarget;
        [SerializeField] [Range(0, 12)] public float rotationSpeed = 6;

        public bool HasRemainingDistance => 
            navMeshAgent
            && navMeshAgent.hasPath
            && navMeshAgent.remainingDistance >= navMeshAgent.stoppingDistance;

        private void OnValidate()
        {
            if (!navMeshAgent) navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Awake() => OnValidate();

        private void Update()
        {
            if (!navMeshAgent) return;
            
            if (!target)
            {
                navMeshAgent.ResetPath();
                return;
            }

            if (Vector3.Distance(transform.position, target.position) <= navMeshAgent.stoppingDistance)
            {
                navMeshAgent.ResetPath();

                Vector3 lookDirection = Vector3.zero;

                switch (rotationType)
                {
                    case RotationType.AlignWithTarget:
                        lookDirection = target.forward;
                        break;
                    case RotationType.LookAtTarget:
                        lookDirection = target.position - transform.position;
                        break;
                }

                lookDirection = Vector3.ProjectOnPlane(lookDirection, Vector3.up).normalized;

                if (lookDirection != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                    transform.rotation =
                        Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                }
            }
            else navMeshAgent.SetDestination(target.position);
        }
    }
}