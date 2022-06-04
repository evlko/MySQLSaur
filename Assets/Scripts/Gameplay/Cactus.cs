using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour, IPoolable
{
    private float _speed;
    private ObjectsPool _assignedPool;

    public float Speed
    {
        set => _speed = value;
    }
    
    private void FixedUpdate()
    {
        transform.position -= new Vector3(_speed, 0, 0);
    }
    
    public void AssignPool(ObjectsPool objectsPool)
    {
        _assignedPool = objectsPool;
    }
}
