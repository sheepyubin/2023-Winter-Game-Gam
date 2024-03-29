using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boids/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : BoidsBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public override Vector2 CalculateMove(BoidsAgent agent, List<Transform> context, Boids boid)
    {
        // 근처에 없다면 0을 반환
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up,cohesionMove,ref currentVelocity,agentSmoothTime);
        return cohesionMove;
    }
}
