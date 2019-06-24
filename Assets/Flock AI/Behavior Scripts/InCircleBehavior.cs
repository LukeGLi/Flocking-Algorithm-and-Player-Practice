using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayInCircle")]
public class InCircleBehavior : FlockBehavior
{
    public Vector2 center;
    public float radius = 25f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;
        if (t < 0.75f) return Vector2.zero;
        else return centerOffset * t * t;
    }
}
