using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Scriptable Objects/Board/Layout")]
public class BoardLayout : ScriptableObject
{

    [Serializable]
    private class BoardScuareSetup {
        public Vector2Int position;
        public Type type;
        public TeamColor team;

    }

    [SerializeField] private BoardScuareSetup[] boardSquares;

    public int GetPiecesCount() {
        return boardSquares.Length;
    }
    public Vector2Int GetSquaresPieceCoordsAtIndex(int index) { // Coordenadas de la pieza que esta en el cuadrado
        if (boardSquares.Length <= index) {
            Debug.LogError("Index of piece is out of range");
            return new Vector2Int(-1, -1);
        }
        return new Vector2Int(boardSquares[index].position.x - 1, boardSquares[index].position.y - 1);
        
    }
    public string GetSquarePieceNameAtIndex(int index) { // Nombre de la pieza que esta en el cuadrado
        if (boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out of range");
            return "";
        }
        return boardSquares[index].type.ToString(); 
    }

    public TeamColor GetSquareTeamColorAtIndex(int Index) { //Team de la pieza en ese cuadrado
        if (boardSquares.Length <= index)
        {
            Debug.LogError("Index of piece is out of range");
            return TeamColor.Black;
        }
        return boardSquares[index].Type.ToString();
    }
}
