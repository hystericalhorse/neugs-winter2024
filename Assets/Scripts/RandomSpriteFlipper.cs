using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteFlipper : MonoBehaviour
{
    [SerializeField, Range(1,100)] private int chance = 50;
    void Start()
    {
        Random rand = new Random(Guid.NewGuid().GetHashCode());
        if (rand.Next(100) <= chance) GetComponent<SpriteRenderer>().flipX = true;
    }
}
