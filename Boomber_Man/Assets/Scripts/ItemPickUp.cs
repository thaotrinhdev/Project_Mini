using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBoom,
        BlastRadius,
        SpeedIncrean,
    }
    public ItemType Type;
    private void OnitemPickUp(GameObject player)
    {
        switch (Type)
        {
            case ItemType.ExtraBoom:
                player.GetComponent<BoomController>().AddBoom();
                break;
            case ItemType.BlastRadius:
                player.GetComponent<BoomController>().explosionRadius++;
                break;
            case ItemType.SpeedIncrean:
                player.GetComponent<MovementContriller>().speed++;
                break;
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnitemPickUp(other.gameObject);
        }
    }
}
