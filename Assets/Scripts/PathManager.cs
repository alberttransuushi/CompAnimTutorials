using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    //[SerializeField]
    private List<Waypoint> path;

    public void CreateAddPoint()
    {
        Waypoint go = new Waypoint();
        path.Add(go);
    }

    public List<Waypoint> GetPath()
    {
        if (path == null)
        {
            path = new List<Waypoint>();
        }
        return path;
    }
}