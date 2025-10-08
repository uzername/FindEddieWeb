using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// class that controls navigation of Eddie
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EddieMind : MonoBehaviour
{
    /// <summary>
    /// list of navigation points to which Eddie can move. On init Eddie picks the starting position. 
    /// Once she reaches a position she randomly picks another one.
    /// It is Transform, so user can pick it in editor
    /// </summary>
    public List<Transform> NavPoints = new List<Transform>();
    private Animator animator;
    private NavMeshAgent agent;
    private Transform currentTarget;
    // Threshold to consider that Eddie has reached the target
    public float stoppingDistance = 0.5f;

    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        if (NavPoints.Count == 0)
        {
            Debug.LogWarning("EddieMind: No NavPoints assigned.");
            return;
        }

        // Pick initial destination
        PickNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused || agent == null || NavPoints.Count == 0)
            return;

        if (!agent.pathPending && agent.remainingDistance <= stoppingDistance)
        {
            // Reached the current target. Let Eddie pause a bit and then continue walking
            StartCoroutine(PauseThenContinue());
        }
    }

    IEnumerator PauseThenContinue()
    {
        isPaused = true;

        // Stop moving
        agent.isStopped = true;
        animator.Play("EddiePose"); // play your idle/pose animation

        yield return new WaitForSeconds(4f); // wait 4 seconds

        // Resume walking
        PickNewDestination();
        animator.Play("EddieWalk"); // optional: resume walk animation
        agent.isStopped = false;
        isPaused = false;
    }

    /// <summary>
    /// Picks a new random destination different from the current one
    /// </summary>
    void PickNewDestination()
    {
        if (NavPoints.Count == 0)
            return;

        Transform newTarget;
        do
        {
            newTarget = NavPoints[Random.Range(0, NavPoints.Count)];
        }
        while (newTarget == currentTarget && NavPoints.Count > 1);

        currentTarget = newTarget;
        agent.SetDestination(currentTarget.position);
    }

}
