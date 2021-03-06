﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float mass;
    public float massMultiplier;
    public List<Rigidbody2D> players = new List<Rigidbody2D>();
    public Rigidbody2D player;
    public float forceComponentConstant;
    private float UniversalGravityConstant;
    private Vector2 direction;
    private float distance;
    private Vector2 force;
    public Vector2 position;
    public int gravMultiplier;
    public CircleCollider2D colliderCircle;
    public Rigidbody2D rb;
    public bool affectPlanets = true;
    public bool affectPlayer = true;

    public int BlackWhiteHoleMultiplier = 20;

    public enum type { Planet, BlackHole, WhiteHole };
    public type bodyType;

    public bool active;

    public void Start()
    {
        UniversalGravityConstant = 6.67f * Mathf.Pow(10f, -11f);

        mass = 0.75f * Mathf.PI * Mathf.Pow(transform.localScale.x, 3f) * massMultiplier;

        rb = gameObject.GetComponent<Rigidbody2D>();

        if (affectPlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        }

        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("dynamic"))
        {
            players.Add(GO.GetComponent<Rigidbody2D>());
        }

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

        if (player)
        {
            forceComponentConstant = UniversalGravityConstant * player.mass * mass * gravMultiplier;
        }
        else
        {
            forceComponentConstant = UniversalGravityConstant * mass * gravMultiplier;
        }

        active = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && col.GetComponent<ControlPlayer>())
        {
            if (FindObjectOfType<Camera>().backgroundColor == Color.black)
            {
                FindObjectOfType<Camera>().backgroundColor = Color.white;
            }
            else
            {
                FindObjectOfType<Camera>().backgroundColor = Color.black;
            }
            FindObjectOfType<LevelManager>().Seed();
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
            planet.gameObject.GetComponent<ColourPlanet>().Colour();
        }
        else if (planet.bodyType == type.WhiteHole)
        {
            planet.bodyType = type.BlackHole;
            planet.gameObject.GetComponent<ColourPlanet>().Colour();
        }
        else if (planet.bodyType == type.Planet)
        {
            planet.massMultiplier *= -1;
        }
        planet.Start();
    }

    void FixedUpdate()
    {
        if (player)
        {
            direction = AngleBetweenPoints(transform.position, player.transform.position);
            distance = Vector2.Distance(transform.position, player.transform.position);
            force = (forceComponentConstant * direction) / (distance * distance);

            if (active && affectPlayer)
            {
                player.AddForce(force);
            }
        }

        if (affectPlanets && active)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] != rb && players[i])
                {
                    direction = AngleBetweenPoints(transform.position, players[i].transform.position);
                    distance = Vector2.Distance(transform.position, players[i].transform.position);
                    force = (forceComponentConstant * direction * players[i].mass) / (distance * distance);

                    if (active)
                    {
                        players[i].AddForce(force);
                    }
                }
            }
        }
    }

    Vector2 AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y).normalized;
    }
}
