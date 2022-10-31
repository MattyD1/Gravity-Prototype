using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody2D))]
public class CelestialBody : MonoBehaviour {

    public float radius;
    public float surfaceGravity;
    public Vector2 initialVelocity;
    public string bodyName = "undefined";

    public Vector2 velocity { get; private set; }
    public float mass { get; private set; }

    Rigidbody2D rb2d;

    private void Awake() {

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.mass = mass;
        velocity = initialVelocity;
        
    }

    public void UpdateVelocity (CelestialBody[] allBodies, float timeStep) {
        foreach(var otherBody in allBodies)
        {
            if(otherBody != this)
            {
                float sqrDst = (otherBody.rb2d.position - rb2d.position).sqrMagnitude;
                Vector2 forceDir = (otherBody.rb2d.position - rb2d.position).normalized;

                Vector2 acceleration = forceDir * Universe.gravitationConstant * otherBody.mass / sqrDst;
                velocity += acceleration * timeStep;
            }
        }
    }

    public void UpdateVelocity (Vector2 acceleration, float timeStep)
    {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition (float timeStep)
    {
        rb2d.MovePosition(rb2d.position + velocity * timeStep);
    }

    void OnValidate()
    {
        mass = surfaceGravity * radius * radius / Universe.gravitationConstant;
        gameObject.name = bodyName;
    }

    public Rigidbody2D RigidBody
    {
        get
        {
            return rb2d;
        }
    }

    public Vector2 Position
    {
        get
        {
            return rb2d.position;
        }
    }


}
