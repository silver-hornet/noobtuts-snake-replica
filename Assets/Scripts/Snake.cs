﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour
{
    // Current movement direction
    // (by default it moves to the right
    Vector2 dir = Vector2.right;

    // Keep track of tail
    List<Transform> tail = new List<Transform>();

    // Did the snake eat something?
    bool ate = false;

    // Tail Prefab
    [SerializeField] GameObject tailPrefab;

    void Start()
    {
        // Move the snake every 300ms
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    void Update()
    {
        // Move in a new direction?
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = Vector2.down; // Could also be accomplished with dir = -Vector2.up
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = Vector2.left; // Could also be accomplished with dir = -Vector2.right
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }

    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction
        transform.Translate(dir);
        // transform.Translate means 'add this vector to my position'

        // Ate something? Then insert new element into gap
        if (ate)
        {
            // Load prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }

        // Do we have a tail?
        if (tail.Count > 0)
        {
            // Move last tail element to where the head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Food?
        if (collision.name.StartsWith("FoodPrefab"))
        // We use collision.name.StartsWith because the food is called 'FoodPrefab(Clone)' after instantiating it.
        // The more elegant way to find out if coll is food or not would be by using a Tag, but for the sake of simplicity we will use string comparison here.s
        {
            // Get longer in next move call
            ate = true;

            // Remove the food
            Destroy(collision.gameObject);
        }

        // Collided with tail or border
        else
        {
            // TODO 'You Lose' screen
        }
    }
}
