using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSuporter : MonoBehaviour
{
    [SerializeField] List<Transform> supportPoints;
    [SerializeField] GameObject fuelPrefab;
    [SerializeField] float maxNumberOfFuel = 3f;
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] float delayToDropItem = 3f;
    [SerializeField] float timeBetweenSpawns = 5f;

    float timeSinceLastSpawn = Mathf.Infinity;
    float timeSinceRemoveItem = Mathf.Infinity;
    Dictionary<GameObject, Transform> supportItems = new Dictionary<GameObject, Transform>();

    GameObject ammo;

    private void Update()
    {
        timeSinceRemoveItem += Time.deltaTime;

        if (timeSinceLastSpawn > timeBetweenSpawns && supportItems.Count < maxNumberOfFuel && timeSinceRemoveItem > delayToDropItem) 
        {
            if(ammo==null)
            {
                SpawnAmmo();
            }
            else
            {
                SpawnFuel();
            }
            timeSinceLastSpawn = 0f;
        }
      
        timeSinceLastSpawn += Time.deltaTime;
    }

    private void SpawnFuel()
    {
        Transform instantiatePos = GetInstantiatePos();
        GameObject fuel = Instantiate(fuelPrefab, instantiatePos.position, Quaternion.identity, transform);
        fuel.GetComponent<Fuel>().gameSuporter = this;
        supportItems.Add(fuel, instantiatePos);
    }

    private void SpawnAmmo()
    {
        Transform instantiatePos = GetInstantiatePos();
        ammo = Instantiate(ammoPrefab, instantiatePos.position, Quaternion.identity, transform);
        ammo.GetComponent<Ammo>().gameSuporter = this;
        supportItems.Add(ammo, instantiatePos);
    }

    private Transform GetInstantiatePos()
    {
        List<Transform> newPointsList = supportPoints.Except(supportItems.Values).ToList();
        int pointIndex = Random.Range(0, newPointsList.Count);

        return newPointsList[pointIndex];
    }

    public void RemoveItem(GameObject fuelExample)
    {
        supportItems.Remove(fuelExample);
        timeSinceRemoveItem = 0f;
    }
}
