//Putri Kiara Salsabila Arief (2306250743)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyClickSpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyVariants;
    [SerializeField] private int selectedVariant = 0;

    void Start()
    {
    Assert.IsTrue(enemyVariants.Length > 0, "Tambahkan 1 Prefab Enemy ke dalam Array Enemy Variants");
    }

    private void Update()
    {
        for (int i = 1; i <= enemyVariants.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                selectedVariant = i - 1;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        if (selectedVariant < enemyVariants.Length)
        {
            Instantiate(enemyVariants[selectedVariant]);
        }
    }
}