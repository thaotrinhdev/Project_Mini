using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoomController : MonoBehaviour
{
    [Header("Boom")]
    public GameObject boomPrefab;
    public KeyCode inputkey = KeyCode.Space;
    public float boomFuseTime = 10f;
    public int boomAmount = 1;
    private int boomsRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTile;
    public Destructible destructiblePrefab;
    private void OnEnable()
    {
        boomsRemaining = boomAmount;

    }
    private void Update()
    {
        if (boomsRemaining > 0 && Input.GetKeyDown(inputkey))
        {
            StartCoroutine(PlaceBoom());
        }
    }
    private IEnumerator PlaceBoom()
    {
        Vector2 postion = transform.position;
        // Lam tron so 
        postion.x = Mathf.Round(postion.x);
        postion.y = Mathf.Round(postion.y);

        GameObject boom = Instantiate(boomPrefab, postion, Quaternion.identity);
        boomsRemaining--;

        yield return new WaitForSeconds(boomFuseTime);

        postion = boom.transform.position;
        postion.x = Mathf.Round(postion.x);
        postion.y = Mathf.Round(postion.y);

        Explosion explosion = Instantiate(explosionPrefab, postion, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);
        Destroy(explosion.gameObject, explosionDuration);

        Explode(postion, Vector2.up, explosionRadius);
        Explode(postion, Vector2.down, explosionRadius);
        Explode(postion, Vector2.left, explosionRadius);
        Explode(postion, Vector2.right, explosionRadius);

        Destroy(boom);
        boomsRemaining++;
    }
    private void Explode(Vector2 postion, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }
        postion += direction;
        // Pham vi khi no sex gioi han voi nhung vat da chan duong no
        if(Physics2D.OverlapBox(postion, Vector2.one/2f,0f, explosionLayerMask))
        {
            ClearContructible(postion);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, postion, Quaternion.identity);
        explosion.SetActiveRenderer(length>1? explosion.middle: explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(postion, direction, length - 1);
    }
    private void ClearContructible(Vector2 postion) 
    {
        Vector3Int cell = destructibleTile.WorldToCell(postion);
        TileBase tile = destructibleTile.GetTile(cell);
        if(tile!= null )
        {
            // Khoi tao manh ghep bi pha huy
            Instantiate(destructiblePrefab,postion,Quaternion.identity);
            destructibleTile.SetTile(cell, null);

        }
    }
    public void AddBoom()
    {
        boomAmount++;
        boomsRemaining++;
    }
}
