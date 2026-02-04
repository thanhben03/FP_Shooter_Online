using System.Collections.Generic;
using UnityEngine;

public class AmmoPoolManager : MonoBehaviour
{
    public static AmmoPoolManager Instance;

    public GameObject ammoPrefab;
    public int poolSize = 5;

    private List<GameObject> pool = new();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(ammoPrefab, transform);
            obj.SetActive(false);
            obj.name = "Ammo_" + i;
            pool.Add(obj);
        }
    }

    public GameObject GetAmmoByIndex(int index)
    {
        if (index < 0 || index >= pool.Count) return null;
        return pool[index];
    }
}
