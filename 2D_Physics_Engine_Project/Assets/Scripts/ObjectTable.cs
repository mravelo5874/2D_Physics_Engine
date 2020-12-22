using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTable
{
    private Dictionary<string, List<PhysicsObject>> dictionary;
    private List<PhysicsObject> objects;

    public ObjectTable()
    {
        dictionary = new Dictionary<string, List<PhysicsObject>>();
        objects = new List<PhysicsObject>();
    }

    public bool KeyExists(string key)
    {
        return dictionary.ContainsKey(key);
    }

    public List<string> GetKeys()
    {
        return new List<string>(dictionary.Keys);
    }

    public void AddObject(string key, PhysicsObject obj)
    {
        if (!KeyExists(key))
        {
            List<PhysicsObject> newList = new List<PhysicsObject>();
            dictionary.Add(key, newList);
        }

        dictionary[key].Add(obj);
        objects.Add(obj);
    }

    public List<PhysicsObject> GetAllObjects()
    {
        return objects;
    }   
}
