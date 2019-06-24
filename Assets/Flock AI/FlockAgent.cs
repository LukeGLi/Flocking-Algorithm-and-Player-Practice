using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour {

    Flock agentFlock;
    public Flock getAgentFlock { get { return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D getAgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start() {
        agentCollider = GetComponent<Collider2D>();    
    }

    public void init(Flock flock) {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity) {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
