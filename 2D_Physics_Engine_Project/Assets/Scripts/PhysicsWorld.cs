using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionSide
{
    TOP,
    BOTTOM,
    LEFT,
    RIGHT
}

public enum CollisionDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public struct CollisionPacket
{
    public bool collision;
    public CollisionSide side;

    public CollisionPacket(bool _collision, CollisionSide _side)
    {
        this.collision = _collision;
        this.side = _side;
    }
}

public class PhysicsWorld : MonoBehaviour
{
    // ################################################################
    //
    //      VARIABLES
    //
    // ################################################################

    private ObjectTable objectTable;

    public bool sideBySideCollision;

    // ################################################################
    //
    //      CORE FUNCTIONS
    //
    // ################################################################

    void Awake()
    {
        // create instance of ObjectTable
        objectTable = new ObjectTable();
    }

    // Physics Load Function
    void Start()
    {
        objectTable.AddObject("boxes", new PhysicsObject(0, 0, 1, 1));
    }

    // Physics Update Function
    void FixedUpdate() 
    {
        float deltaTime = Time.deltaTime;

        // check each object against every other object
        List<PhysicsObject> allObjects = objectTable.GetAllObjects();

        foreach (PhysicsObject obj1 in allObjects)
        {
            foreach (PhysicsObject obj2 in allObjects)
            {
                // ignore if objects are the same
                if (obj1 == obj2)
                    continue;
                
                // check collision between objects
                CollisionPacket packet = CheckCollision(obj1, obj2);
                if (packet.collision)
                {
                    switch(packet.side)
                    {
                        case CollisionSide.TOP:
                            BumpObjects(obj1, obj2, CollisionDirection.DOWN);
                            BumpObjects(obj2, obj1, CollisionDirection.UP);
                            break;
                        case CollisionSide.BOTTOM:
                            BumpObjects(obj1, obj2, CollisionDirection.UP);
                            BumpObjects(obj2, obj1, CollisionDirection.DOWN);
                            break;
                        case CollisionSide.LEFT:
                            BumpObjects(obj1, obj2, CollisionDirection.RIGHT);
                            BumpObjects(obj2, obj1, CollisionDirection.LEFT);
                            break;
                        case CollisionSide.RIGHT:
                            BumpObjects(obj1, obj2, CollisionDirection.LEFT);
                            BumpObjects(obj2, obj1, CollisionDirection.RIGHT);
                            break;
                    }
                }
            }
        }

    }

    // ################################################################
    //
    //      PHYSICS FUNCTIONS  
    //
    // ################################################################

    CollisionPacket CheckCollision(PhysicsObject obj1, PhysicsObject obj2)
    {
        bool collision = false;
        CollisionSide side = CollisionSide.TOP;

        float p1x = Mathf.Max(obj1.x, obj2.x);
        float p1y = Mathf.Max(obj1.y, obj2.y);

        float p2x = Mathf.Min(obj1.x + obj1.w, obj2.x + obj2.w);
        float p2y = Mathf.Min(obj1.y + obj1.h, obj2.y + obj2.h);

        if (sideBySideCollision)
        {
            if (p2x - p1x >= 0 && p2y - p1y >= 0)
                collision = true;
        }
        else
        {
            if (p2x - p1x > 0 && p2y - p1y > 0)
                collision = true;
        }

        if (collision)
        {
            float right = (obj1.x + obj1.w) - obj2.x;
            float left = (obj2.x + obj2.w) - obj1.x;
            float bottom = (obj1.y + obj1.h) - obj2.y;
            float top = (obj2.y + obj2.h) - obj1.y;

            if (right < left && right < top && right < bottom)
                side = CollisionSide.RIGHT;
            else if (left < top && left < bottom)
                side = CollisionSide.LEFT;
            else if (top < bottom)
                side = CollisionSide.TOP;
            else
                side = CollisionSide.BOTTOM;
        }

        return new CollisionPacket(collision, side);
    }

    void BumpObjects(PhysicsObject obj1, PhysicsObject obj2, CollisionDirection dir)
    {
        //float width = Mathf.Min(obj1.x + obj1.w, obj2.x + obj2.w);
        //float height = Math.Min(obj1.y + obj1.h, obj2.y + obj2.h);

        switch (dir)
        {
            case CollisionDirection.UP:
                obj1.y = obj2.y + obj2.h;
                break;
            case CollisionDirection.DOWN:
                obj1.y = obj2.y - obj2.h;
                break;
            case CollisionDirection.LEFT:
                obj1.x = obj2.x + obj2.w;
                break;
            case CollisionDirection.RIGHT:
                obj1.x = obj2.x - obj2.w;
                break;
        }
    }
}
