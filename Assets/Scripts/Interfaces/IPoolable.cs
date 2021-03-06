using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    void AssignPool(ObjectsPool objectsPool);

    IEnumerator AutoReturn(float seconds);
}
