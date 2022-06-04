using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<Transform> entityPrefabs;
    public int initialPoolSize;

    private readonly Queue<Transform> _pool = new Queue<Transform>();
    
    private void Start()
    {
        for (var i = 0; i < initialPoolSize; i++)
        {
            InstantiateNewEntity();
        }
    }

    private Transform InstantiateNewEntity()
    {
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        var entityPrefab = entityPrefabs[Random.Range(0, entityPrefabs.Count)];
        var newEntityInstance = Instantiate(entityPrefab,
            spawnPoint.position, Quaternion.identity);
        newEntityInstance.parent = spawnPoint.transform;
        _pool.Enqueue(newEntityInstance);
        newEntityInstance.GetComponent<IPoolable>().AssignPool(this);
        newEntityInstance.gameObject.SetActive(false);
        return newEntityInstance;
    }

    public Transform GetEntityFromPool()
    {
        var entity = _pool.Count > 0 ? _pool.Dequeue() : InstantiateNewEntity();
        entity.gameObject.SetActive(true);
        StartCoroutine(entity.GetComponent<IPoolable>().AutoReturn(5));
        return entity;
    }

    public void ReturnEntityToPool(Transform entity)
    {
        entity.gameObject.SetActive(false);
        entity.position = entity.parent.position;
        entity.rotation = entity.parent.rotation;
        _pool.Enqueue(entity);
    }
}
