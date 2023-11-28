using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;
    [SerializeField] private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        bottomLeftEdge = tilemap.localBounds.min + new Vector3(0.5f, 1f, 0f);
        topRightEdge = tilemap.localBounds.max + new Vector3(-0.5f, -1f, 0f);

        Player.Instance.SetLimits(bottomLeftEdge, topRightEdge);
        Debug.Log("sets new limits");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
