using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySimulation : MonoBehaviour {

    CelestialBody[] bodies;
    static BodySimulation instance;

    void Awake()
    {
        bodies = FindObjectsOfType<CelestialBody>();
        Time.fixedDeltaTime = 0.01f;
        Debug.Log("Setting deltaTime to: " + 0.01f);
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < bodies.Length; i++)
        {
            Vector2 acceleration = CalculateAcceleration(bodies[i].Position, bodies[i]);
            bodies[i].UpdateVelocity(acceleration, 0.01f);
        }

        for(int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(0.01f);
        }
    }

    public static Vector2 CalculateAcceleration (Vector2 point, CelestialBody ignoreBody = null)
    {
        Vector2 acceleration = Vector2.zero;
        foreach(var body in Instance.bodies)
        {
            if(body != ignoreBody)
            {
                float sqrDst = (body.Position - point).sqrMagnitude;
                Vector2 forceDir = (body.Position - point).normalized;
                acceleration += forceDir * Universe.gravitationConstant * body.mass / sqrDst;
            }
        }

        return acceleration;
    }

    public static CelestialBody[] Bodies
    {
        get
        {
            return Instance.bodies;
        }
    }

    static BodySimulation Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<BodySimulation>();
            }
            return instance;
        }
    }
}
