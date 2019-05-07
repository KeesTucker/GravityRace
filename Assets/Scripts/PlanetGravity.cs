using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float mass;
    public float massMultiplier;
    public Rigidbody2D player;
    public float forceComponentConstant;
    private float UniversalGravityConstant;
    private Vector2 direction;
    private float distance;
    private Vector2 force;
    public Vector2 position;
    public int gravMultiplier;
    public Collider2D colliderCircle;

    public int BlackWhiteHoleMultiplier = 20;

    public enum type { Planet, BlackHole, WhiteHole };
    public type bodyType;

    void Start()
    {
        UniversalGravityConstant = 6.67f * Mathf.Pow(10f, -11f);

        mass = 0.75f * Mathf.PI * Mathf.Pow(transform.localScale.x, 3f) * massMultiplier;

        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();

        position = transform.position;

        if (bodyType == type.Planet)
        {
            gravMultiplier = 1;
            colliderCircle.enabled = true;
        }
        else if (bodyType == type.BlackHole)
        {
            gravMultiplier = BlackWhiteHoleMultiplier;
            colliderCircle.enabled = false;
        }
        else if (bodyType == type.WhiteHole)
        {
            gravMultiplier = -BlackWhiteHoleMultiplier;
            colliderCircle.enabled = false;
        }

        forceComponentConstant = UniversalGravityConstant * player.mass * mass * gravMultiplier;
    }

    void FixedUpdate()
    {
        direction = AngleBetweenPoints(transform.position, player.transform.position);
        distance = Vector2.Distance(transform.position, player.transform.position);
        force = (forceComponentConstant * direction) / (distance * distance);

        player.AddForce(force);
    }

    Vector2 AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y).normalized;
    }
}
