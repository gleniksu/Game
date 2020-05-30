using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tile object, with different types
public class TileArray{
    
    // A grid object to place tiles
    private GridArray<TileArrayObject> grid;

    // Constructor
    public TileArray(int height, int width, float cellSize, Vector3 originPosition){
        grid = new GridArray<TileArrayObject>(height, width, cellSize, originPosition, 
                                              (int row, int col) => new TileArrayObject(row, col));
    }

    // Get grid
    public GridArray<TileArrayObject> GetGrid(){
        return grid;
    }

    // Get specific tile type
    public TileArrayObject.TileArrayType GetTileArrayType(Vector3 worldPosition){
        TileArrayObject tileArrayObject = grid.GetGridObject(worldPosition);
        if (tileArrayObject != null){
            return tileArrayObject.GetTileArrayType();
        }
        else{
            return new TileArrayObject.TileArrayType();
        }
    }

    // Change specific tile type
    public void SetTileArrayType(Vector3 worldPosition, TileArrayObject.TileArrayType tileArrayType){
        TileArrayObject tileArrayObject = grid.GetGridObject(worldPosition);
        if (tileArrayObject != null){
            tileArrayObject.SetTileArrayType(tileArrayType);
        }
        grid.TriggerGridObjectChanged(worldPosition);
    }

    // Change flag status
    public void SetFlagStatus(Vector3 worldPosition) {
        TileArrayObject tileArrayObject = grid.GetGridObject(worldPosition);
        if (tileArrayObject != null) {
            tileArrayObject.SetFlag(!tileArrayObject.IsFlagged());
        }
        grid.TriggerGridObjectChanged(worldPosition);
    }

    /* TODO */
    // Other change

}
