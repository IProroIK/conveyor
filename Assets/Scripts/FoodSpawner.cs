using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private List<Food> _food;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnDelay = 1f;
    private int _poolCount = 6;
    private bool _autoExpand = true;
    [SerializeField]private PoolMono<Food>[] _pool;


    private void Awake()
    {
        _pool = new PoolMono<Food>[_food.Count];
        for (int i = 0; i < _food.Count; i++)
        {
            _pool[i] = new PoolMono<Food>(_food[i], _poolCount, _poolContainer);
            _pool[i].autoExapand = _autoExpand;
        }
        StartCoroutine(FoodSpawn());
    }

    private IEnumerator FoodSpawn()
    {
        var food = _pool[(int)Random.Range(0, _pool.Length)].GetFreeElement();
        food.transform.position = _spawnPoint.position;
        yield return new WaitForSeconds(_spawnDelay);
        StartCoroutine(FoodSpawn());
    }


}
