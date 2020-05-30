using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileArrayObject
{
    // tile type
    public enum TileArrayType {
        Even,
        Odd,
        Empty,
        Mine,
        Num_1,
        Num_2,
        Num_3,
        Num_4,
        Num_5,
        Num_6,
        Num_7,
        Num_8
    };


    private int row;
    private int col;
    private TileArrayType tileArrayType;
    private bool flagged;
    private bool revealed;

    public TileArrayObject(int row, int col){
        this.row = row;
        this.col = col;
        if ((row+col) % 2 == 0){
            this.tileArrayType = TileArrayType.Even;
        } else {
            this.tileArrayType = TileArrayType.Odd;
        }
        flagged = false;
        revealed = false;
    }


    public int GetRow(){
        return row;
    }
    public int GetCol(){
        return col;
    }
    // Get tile type
    public TileArrayType GetTileArrayType(){
        return tileArrayType;
    }
    // Flagged ?
    public bool IsFlagged(){
        return flagged;
    }
    // Revealed ?
    public bool IsRevealed(){
        return revealed;
    }


    // Change tile type
    public void SetTileArrayType(TileArrayType tileArrayType){
        this.tileArrayType = tileArrayType;
    }
    // Set Flag
    public void SetFlag(bool flagged){
        this.flagged = flagged;
    }
    // Set Reveal
    public void SetReveal(bool revealed){
        this.revealed = revealed;
    }

    
    // Mesh display
    public override string ToString(){
        return tileArrayType.ToString();
    }
}
