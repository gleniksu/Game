using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a (height x width) Grid Class
public class GridArray<GridObject>{

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs: EventArgs{
        public int row;
        public int col;
    }

    private Vector3 origin;
    private int width;
    private int height;
    private float cellSize;

    private GridObject[,] grid;

    public GridArray(int height, int width, float cellSize, Vector3 origin,
                     Func<int, int, GridObject> createGridObject){
        this.origin = origin;
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        // Create grid
        grid = new GridObject[height, width];
        for (int row=0; row < grid.GetLength(0); row++){
            for (int col=0; col < grid.GetLength(1); col++){
                grid[row, col] = createGridObject(row, col);
            }
        }

        // Debug Visualization
        bool DebugVisualization = true;
        if (DebugVisualization){
            TextMesh[,] debugGrid = debugGrid = new TextMesh[height, width];
            // Create visual
            for (int row=0; row < grid.GetLength(0); row++){
                for (int col=0; col < grid.GetLength(1); col++){
                    // write text in each block
                    Vector3 textPosition = GetWorldPosition(row, col) + 0.5f*new Vector3(cellSize, cellSize);
                    debugGrid[row, col] = MeshUtils.createWorldText(null, grid[row, col]?.ToString(), textPosition,
                                                                    Color.black, TextAnchor.MiddleCenter, 
                                                                    TextAlignment.Center, 15);
                    // lines (debug)
                    Debug.DrawLine(GetWorldPosition(row, col), GetWorldPosition(row, col+1), Color.white, 20);
                    Debug.DrawLine(GetWorldPosition(row, col), GetWorldPosition(row+1, col), Color.white, 20);
                }
            }
            // Draw complete lines (debug)
            Debug.DrawLine(GetWorldPosition(0, width), GetWorldPosition(height, width), Color.white, 20);
            Debug.DrawLine(GetWorldPosition(height, 0), GetWorldPosition(height, width), Color.white, 20);
            
            // Change mesh objects
            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => {
                debugGrid[eventArgs.row, eventArgs.col].text = grid[eventArgs.row, eventArgs.col]?.ToString();
            };
        }
    }


    // Get grid property
    public int GetHeight(){
        return height;
    }
    public int GetWidth(){
        return width;
    }
    public float GetCellSize(){
        return cellSize;
    }

    // Get grid object
    public GridObject GetGridObject(int row, int col){
        if (row >= 0 && col >= 0 && row < height && col < width){
            return grid[row, col];
        } else {
            return default(GridObject);
        }
    }
    public GridObject GetGridObject(Vector3 worldPosition){
        int row, col;
        GetGridPosition(worldPosition, out row, out col);
        return GetGridObject(row, col);
    }

    // Set grid object
    public void SetGridObject(int row, int col, GridObject gridObject){
        if (row >= 0 && col >= 0 && row < height && col < width){
            grid[row, col] = gridObject;
            if (OnGridObjectChanged != null)
                OnGridObjectChanged(this, new OnGridObjectChangedEventArgs{row=row, col=col});
        }
    }
    public void SetGridObject(Vector3 worldPosition, GridObject gridObject){
        int row, col;
        GetGridPosition(worldPosition, out row, out col);
        SetGridObject(row, col, gridObject);
    }

    // Triger event
    public void TriggerGridObjectChanged(int row, int col){
        if (OnGridObjectChanged != null)
            OnGridObjectChanged(this, new OnGridObjectChangedEventArgs{row=row, col=col});
    }
    public void TriggerGridObjectChanged(Vector3 worldPosition){
        if (OnGridObjectChanged != null){
            int row, col;
            GetGridPosition(worldPosition, out row, out col);
            OnGridObjectChanged(this, new OnGridObjectChangedEventArgs{row=row, col=col});
        }
    }

    // Grid Position -> World Position
    public Vector3 GetWorldPosition(int row, int col){
        float x = col * cellSize + origin.x;
        float y = row * cellSize + origin.y;
        return new Vector3(x, y);
    }

    // World Position -> Grid Position
    public void GetGridPosition(Vector3 worldPosition, out int row, out int col){
        row = Mathf.FloorToInt((worldPosition-origin).y / cellSize);
        col = Mathf.FloorToInt((worldPosition-origin).x / cellSize);
    }
}
