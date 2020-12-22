using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject
{
    // x and y position
    public float x;
    public float y;

    // width and height
    public float w;
    public float h;

    // physics variables 
    public float friction;

    public PhysicsObject(float x, float y, float w, float h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }

    public void Update()
    {

    }

    public void Draw()
    {

    }
}
