using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePattern : MonoBehaviour {

    public float searchTurnSpeed = 140f;
    public float searchDur = 4f;
    public float sideRange = 20f;
    public Transform eyes;

    public Transform[] wayPoints;

    public MeshRenderer indicator;

    [HideInInspector]
    public IEnenmyState currentState;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public ChaseState chaseState;

    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    private void Awake()
    {
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();




    }

    // Use this for initialization
    void Start () {

        currentState = patrolState;
		
	}
	
	// Update is called once per frame
	void Update () {

        currentState.UpdateState();


	}

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTrigger(other);
    }
}
