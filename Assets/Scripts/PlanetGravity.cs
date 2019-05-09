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
    public CircleCollider2D colliderCircle;

    public int BlackWhiteHoleMultiplier = 20;

    public enum type { Planet, BlackHole, WhiteHole };
    public type bodyType;

    public bool active;

    public void Start()
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
            colliderCircle.isTrigger = true;
        }
        else if (bodyType == type.WhiteHole)
        {
            gravMultiplier = -BlackWhiteHoleMultiplier;
            colliderCircle.isTrigger = true;
        }

        forceComponentConstant = UniversalGravityConstant * player.mass * mass * gravMultiplier;

        active = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (FindObjectOfType<Camera>().backgroundColor == Color.black)
            {
                FindObjectOfType<Camera>().backgroundColor = Color.white;
            }
            else
            {
                FindObjectOfType<Camera>().backgroundColor = Color.black;
            }
            foreach (PlanetGravity planet in FindObjectsOfType<PlanetGravity>())
            {
                StartCoroutine(DelaySwitch(planet));
            }
        }
    }

    IEnumerator DelaySwitch(PlanetGravity planet)
    {
        planet.active = false;
        yield return new WaitForSeconds(0.1f);
        if (planet.bodyType == type.BlackHole)
        {
            planet.bodyType = type.WhiteHole;
            planet.gameObject.GetComponent<ColourPlanet>().Start();
        }
        else if (planet.bodyType == type.WhiteHole)
        {
            planet.bodyType = type.BlackHole;
            planet.gameObject.GetComponent<ColourPlanet>().Start();
        }
        else if (planet.bodyType == type.Planet)
        {
            planet.massMultiplier *= -1;
        }
        planet.Start();
    }

    void FixedUpdate()
    {
        direction = AngleBetweenPoints(transform.position, player.transform.position);
        distance = Vector2.Distance(transform.position, player.transform.position);
        force = (forceComponentConstant * direction) / (distance * distance);

        if (active)
        {
            player.AddForce(force);
        }
    }

    Vector2 AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y).normalized;
    }
}
