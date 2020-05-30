using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSweeperGameHandler : MonoBehaviour{
    [SerializeField] private TilePrehabVisual tilePrehabVisual;

    private TileArray tileArray;

    // Start is called before the first frame update
    void Start(){
        int row = 4;
        int col = 6;
        float cellSize = 3f;
        Vector3 origin = new Vector3(-col/2f*cellSize, -row/2f*cellSize);

        tileArray = new TileArray(row, col, cellSize, origin);
        tilePrehabVisual.Setup(tileArray.GetGrid());
    }

    /* TODO */
    // Game logic
    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonUp(1) && Input.GetMouseButtonUp(0)){
        }
        else if (Input.GetMouseButtonUp(0)){
            Vector3 mousePosition = MeshUtils.GetMouseWorldPosition();
            tileArray.SetTileArrayType(mousePosition, TileArrayObject.TileArrayType.Mine);
        }
        else if (Input.GetMouseButtonUp(1)){
            Vector3 mousePosition = MeshUtils.GetMouseWorldPosition();
            tileArray.SetFlagStatus(mousePosition);
        }
    }
}
