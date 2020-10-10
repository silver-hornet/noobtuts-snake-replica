using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    // Food Prefab
    [SerializeField] GameObject foodPrefab;

    // Borders
    [SerializeField] Transform borderTop;
    [SerializeField] Transform borderBottom;
    [SerializeField] Transform borderLeft;
    [SerializeField] Transform borderRight;

    void Start()
    {
        // Spawn food every 4 seconds, starting in 3
        InvokeRepeating("Spawn", 3, 4);
    }

    void Spawn()
    {
        // x position between left and right border
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        // (int) rounds the number to make sure that the food is always spawned at a position like (1, 2) but never at something like (1.234, 2.74565).

        // y position between top and bottom border
        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);
        // (int) rounds the number to make sure that the food is always spawned at a position like (1, 2) but never at something like (1.234, 2.74565).

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity); // default rotation
    }
}
