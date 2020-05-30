using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TilePrehabVisual : MonoBehaviour{
    [SerializeField] private Transform tilePrehab;
    [SerializeField] private Sprite flagSprite;

    private List<Transform> visualNodeList;
    private Transform[,] visualNodeArray;
    private GridArray<TileArrayObject> grid;
    private bool updateVisual;

    public void Setup(GridArray<TileArrayObject> grid){
        this.grid = grid;
        visualNodeList = new List<Transform>();
        visualNodeArray = new Transform[grid.GetHeight(), grid.GetWidth()];

        // Initialize 
        for (int row=0; row<grid.GetHeight(); row++){
            for (int col=0; col<grid.GetWidth(); col++){
                Vector3 gridPosition = grid.GetWorldPosition(row, col) + 
                                       0.5f*new Vector3(grid.GetCellSize(), grid.GetCellSize());
                Transform visualNode = CreateVisualNode(gridPosition);
                visualNodeArray[row, col] = visualNode;
                visualNodeList.Add(visualNode);
            }
        }

        UpdateVisual(grid);

        grid.OnGridObjectChanged += (object sender, GridArray<TileArrayObject>.OnGridObjectChangedEventArgs eventArgs) => {
                                     updateVisual = true;};
    }

    private void Update() {
        if (updateVisual) {
            updateVisual = false;
            UpdateVisual(grid);
        }
    }

    // Update display
    public void UpdateVisual(GridArray<TileArrayObject> grid) {
        HideNodeVisuals();

        for (int row=0; row<grid.GetHeight(); row++){
            for (int col=0; col<grid.GetWidth(); col++){
                TileArrayObject gridObject = grid.GetGridObject(row, col);
                
                Transform visualNode = visualNodeArray[row, col];
                visualNode.gameObject.SetActive(true);
                SetupVisualNode(visualNode, gridObject);
            }
        }
    }

    // Setup visual node according to different situation
    private void SetupVisualNode(Transform visualNodeTransform, TileArrayObject tileArrayObject) {
        SpriteRenderer iconSpriteRenderer = visualNodeTransform.Find("Icon").GetComponent<SpriteRenderer>();
        TextMeshPro numberText = visualNodeTransform.Find("Number").GetComponent<TextMeshPro>();
        Transform emptyTransform = visualNodeTransform.Find("Empty");

        /* TODO */
        // Node is hidden
        emptyTransform.gameObject.SetActive(true);
        numberText.gameObject.SetActive(false);

        if (tileArrayObject.IsFlagged()) {
            iconSpriteRenderer.gameObject.SetActive(true);
            iconSpriteRenderer.sprite = flagSprite;
        } else {
            iconSpriteRenderer.gameObject.SetActive(false);
        }
    }

    // Stop displaying
    private void HideNodeVisuals() {
        foreach (Transform visualNodeTransform in visualNodeList) {
            visualNodeTransform.gameObject.SetActive(false);
        }
    }

    // Create visual node with given prehab
    private Transform CreateVisualNode(Vector3 position) {
        Transform visualNode = Instantiate(tilePrehab, position, Quaternion.identity);
        return visualNode;
    }
}
