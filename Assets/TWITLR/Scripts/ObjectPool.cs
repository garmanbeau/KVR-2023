using Autohand;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int poolSize = 10;

    private List<GameObject> poolObjects;

    private int currentIndex = 0;

    private Vector3 spawnPoint;

    void Awake()
    {
        spawnPoint = transform.position;
        PopulatePool();
    }

    void PopulatePool()
    {
        poolObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, spawnPoint, Quaternion.identity);
            obj.GetComponent<PoolObject>().pool = this;
            obj.SetActive(false);
            poolObjects.Add(obj);
        }
    }

    public void SpawnObject()
    {

        if (currentIndex >= poolObjects.Count)
        {
          
            currentIndex = 0;
        }
       
        GameObject obj = poolObjects[currentIndex];
        obj.SetActive(true);
        obj.transform.position = spawnPoint;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        currentIndex++;

    }

    private void Start()
    {
        SpawnObject();
    }
  

}