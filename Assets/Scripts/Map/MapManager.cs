using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Animator animator;

    public Tile tile_trash_01;
    public Tile tile_trash_02;

    public Tile tile_polluted_01;
    public Tile tile_polluted_02;
    public Tile tile_polluted_03;
    public Tile tile_polluted_04;

    public Tile tile_clean_01;
    public Tile tile_clean_02;
    public Tile tile_clean_03;
    public Tile tile_clean_04;

    private List<Tile>                      tileset_clean;
    private List<Tile>                      tileset_polluted;
    private Tilemap                         tilemap;
    private List<UnityEngine.Vector3Int>    coords = new List<Vector3Int>();
    private bool direction;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        InitTilesetPolluted();
        InitTilesetClean();

        foreach ( var position in tilemap.cellBounds.allPositionsWithin)
        {
            if ( tilemap.HasTile(position))
            {
                if ( tilemap.GetTile(position).name != "trash_tile_01")
                {
                    tilemap.SetTile(position, SelectRandomTile(ref tileset_polluted));
                    coords.Add(position);
                }
            }
        }
        direction = true;
        InvokeRepeating("GenerateRandomTileClean", 2.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown("space"))
        {
            direction = (direction) ? false : true;
            animator.SetBool("Direction", direction);
        }
    }

    /**
     * Init tileset of clean tiles (grass).
     */
    private void InitTilesetClean()
    {
        tileset_clean = new List<Tile>();

        tileset_clean.Add(tile_clean_01);
        tileset_clean.Add(tile_clean_02);
        tileset_clean.Add(tile_clean_03);
        tileset_clean.Add(tile_clean_04);
    }

    /**
     * Init tileset of polluted tiles (sand).
     */
    private void InitTilesetPolluted() {
        tileset_polluted = new List<Tile>();

        tileset_polluted.Add(tile_polluted_01);
        tileset_polluted.Add(tile_polluted_02);
        tileset_polluted.Add(tile_polluted_03);
        tileset_polluted.Add(tile_polluted_04);
    }

    /**
     * Generate a random tile clean at a random coordinate.
     */
    public void GenerateRandomTileClean()
    {
        tilemap.SetTile(SelectRandomCoordinate(), SelectRandomTile(ref tileset_clean));
    }

    /**
     * Generate a random tile polluted at a random coordinate.
     */
    public void GenerateRandomTilePolluted()
    {
        tilemap.SetTile(SelectRandomCoordinate(), SelectRandomTile(ref tileset_polluted));
    }

    /**
     * Select a random coordinate within the tilemap tiles.
     */
    private UnityEngine.Vector3Int SelectRandomCoordinate()
    {
        int index = Random.Range(0, coords.Count);
        UnityEngine.Vector3Int coord = coords[index];

        return coord;
    }

    /**
     * Select a random tile within the specified tileset.
     */
    private Tile SelectRandomTile(ref List<Tile> __tileset)
    {
        return __tileset[Random.Range(0, __tileset.Count)];
    }
}
