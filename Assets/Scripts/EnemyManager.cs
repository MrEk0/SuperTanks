using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    //static EnemyManager instance;
    [SerializeField] GameObject winPanel;

    List<EnemyHealth> enemies = new List<EnemyHealth>();

    private void Awake()
    {
        //if(instance!=null && instance !=this)
        //{
        //    Destroy(gameObject);
        //}

        //instance = this;
        enemies = GetComponentsInChildren<EnemyHealth>().ToList();
    }

    public void Add(EnemyHealth enemy)
    {
        enemies.Add(enemy);
    }

    public void Remove(EnemyHealth enemy)
    {
        enemies.Remove(enemy);

        if(enemies.Count==0)
        {
            winPanel.SetActive(true);
        }
    }
}
