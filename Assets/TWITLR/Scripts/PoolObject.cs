using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    // Start is called before the first frame update

    public ObjectPool pool;

    public void Reset()
    {
        pool.SpawnObject();
    }

}
