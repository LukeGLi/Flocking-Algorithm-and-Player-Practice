using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;
    const float agentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float sqrMaxSpeed; //Max speed squared
    float sqrNeighborRadius; //Neighbor radius squared
    float sqrAvoidanceRadius; //Avoidance radius squared
    public float getSqrAvoidanceRadius { get { return sqrAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start() {
        sqrMaxSpeed = maxSpeed * maxSpeed;
        sqrNeighborRadius = neighborRadius * neighborRadius;
        sqrAvoidanceRadius = sqrNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for(int i = 0; i < startingCount; i++) {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * agentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent" + i;
            newAgent.init(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update() {
        foreach(FlockAgent agent in agents) {
            List<Transform> context = getNearbyObjects(agent);
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > sqrMaxSpeed) move = move.normalized * maxSpeed;
            agent.Move(move);
        }
    }

    List<Transform> getNearbyObjects(FlockAgent agent) {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders) {
            if(c != agent.getAgentCollider) context.Add(c.transform);
        }
        return context;
    }
}