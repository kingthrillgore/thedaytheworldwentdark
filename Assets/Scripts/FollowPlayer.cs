using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    NavMeshAgent nav;
    private bool _IsPlayerWithinRange = false;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Tells the NavMeshAgent who it needs to annoy
        nav.SetDestination(target.position);
    }
}
