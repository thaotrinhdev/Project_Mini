using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;
    //Gioi han bang thanh process
    [Range(0f, 1f)]
    // Dieu kien de cho 1 item xuat hien bao nhieu lan trong game
    public float itemSpawnChance = 0.2f;
    public GameObject[] swanableItem;
    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }
    //Pha vo gach se sinh ra vat pham
    private void OnDestroy()
    {
        if(swanableItem.Length > 0&& Random.value<itemSpawnChance)
        {
            int randomIndex = Random.Range(0, swanableItem.Length);
            Instantiate(swanableItem[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
