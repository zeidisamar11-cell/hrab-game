using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChase : MonoBehaviour
{
    [SerializeField] private Transform target;            // Pacman
    [SerializeField] private float speed = 3f;            // different for each ghost
    [SerializeField] private float updateInterval = 0.2f; // 5 path updates per second

    private NavMeshAgent agent;
    private float nextUpdate;
    private Vector3 startPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        startPosition = transform.position;
    }

    private void Update()
    {
        if (target == null || Time.time < nextUpdate) return;
        nextUpdate = Time.time + updateInterval;
        agent.SetDestination(target.position);
    }

    public void ResetPosition()
    {
        agent.Warp(startPosition);
        agent.ResetPath();
    }
}