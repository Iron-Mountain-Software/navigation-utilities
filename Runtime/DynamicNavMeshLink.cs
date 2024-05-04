using Unity.AI.Navigation;
using UnityEngine;

namespace IronMountain.NavigationUtilities
{
    [ExecuteAlways]
    [RequireComponent(typeof(NavMeshLink))]
    public class DynamicNavMeshLink : MonoBehaviour
    {
        [SerializeField] private NavMeshLink navMeshLink;
        [SerializeField] public Transform start;
        [SerializeField] public Transform end;

        private void OnValidate()
        {
            if (!navMeshLink) navMeshLink = GetComponent<NavMeshLink>();
        }

        private void Awake() => OnValidate();

        private void Update()
        {
            if (!navMeshLink || !start || !end) return;
            Vector3 startPoint = start.position;
            Vector3 endPoint = end.position;
            Vector3 midPoint = (startPoint + endPoint) / 2;
            navMeshLink.transform.rotation = Quaternion.identity;
            navMeshLink.transform.position = midPoint;
            navMeshLink.startPoint = midPoint - startPoint;
            navMeshLink.endPoint = midPoint - endPoint;
        }
    }
}
