using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform bottomLeftSquareTransform;
    [SerializeField] private float squareSize;
    const int BOARD_SIZE = 8;
    Piece[,] pieceMatrix;
    Piece selectedPiece = null;
    private ChessGameController chessController;
    private SquareSelectorCreator squareSelector;

    private void Awake()
    {
        squareSelector = GetComponent<SquareSelectorCreator>();
        pieceMatrix = new Piece[BOARD_SIZE, BOARD_SIZE];
    }
    public bool CheckIfCoordinatesAreOnBoard(Vector2Int coords)
    {
        if (coords.x < 0 || coords.y < 0 || coords.x >= BOARD_SIZE || coords.y >= BOARD_SIZE)
            return false;
        return true;
    }

    public void addToMatrix(Piece piece, Vector2Int coords)
    {
        if (CheckIfCoordinatesAreOnBoard(coords)) 
        { 
            pieceMatrix[coords.x, coords.y] = piece;
        }
    }

    public Piece GetPieceOnSquare(Vector2Int coords)
    {
        return pieceMatrix[coords.x, coords.y];
    }

    public Vector3 CalculatePositionFromCoords(Vector2Int coords) 
    {
        return bottomLeftSquareTransform.position + new Vector3(coords.x * squareSize, 0f, coords.y * squareSize);
    }

    public void SetChessGameController(ChessGameController cGameController)
    {
        this.chessController = cGameController;
    }
    private Vector2Int CalculateCoordsFromPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt(transform.InverseTransformPoint(inputPosition).x / squareSize) + BOARD_SIZE / 2;
        int y = Mathf.FloorToInt(transform.InverseTransformPoint(inputPosition).z / squareSize) + BOARD_SIZE / 2;
        return new Vector2Int(x, y);
    }

    internal void OnSquareSelected(Vector3 inputPosition)
    {
        Vector2Int coords = CalculateCoordsFromPosition(inputPosition);
        Piece piece = GetPieceOnSquare(coords);
        if (selectedPiece)
        {
            if (piece != null && selectedPiece == piece)
                DeselectPiece();
            else if (piece != null && selectedPiece != piece && chessController.IsTeamTurnActive(piece.team))
                SelectPiece(piece);
            else if (selectedPiece.CanMoveTo(coords))
                OnSelectedPieceMoved(coords, selectedPiece);
        }
        else
        {
            if (piece != null && chessController.IsTeamTurnActive(piece.team))
                SelectPiece(piece);
        }
    }

    private void OnSelectedPieceMoved(Vector2Int coords, Piece selectedPiece)
    {
        UpdateBoardOnPieceMove(coords, selectedPiece.occupiedSquare, selectedPiece, null);
        selectedPiece.MovePiece(coords);
        DeselectPiece();
        EndTurn();
    }

    private void EndTurn()
    {
        chessController.EndTurn();
    }

    private void DeselectPiece()
    {
        selectedPiece = null;
        squareSelector.ClearSelection(); 
    }

    private void UpdateBoardOnPieceMove(Vector2Int coords, Vector2Int occupiedSquare, Piece piece, Piece p) //Intercambia las piezas despues del movimiento
    {
        pieceMatrix[occupiedSquare.x, occupiedSquare.y] = p;  
        pieceMatrix[coords.x, coords.y] = piece;
    }

    public void SelectPiece(Piece piece)
    {
        selectedPiece = piece;
        List<Vector2Int> moves = selectedPiece.avaliableMoves;
        ShowSelectionSquares(moves);
    }

    public void ShowSelectionSquares(List<Vector2Int> selection)
    {
        Dictionary<Vector3, bool> squaresData = new Dictionary<Vector3, bool>();
        for (int i = 0; i < selection.Count; i++)
        {
            Vector3 position = CalculatePositionFromCoords(selection[i]);
            bool isSquareFree = GetPieceOnSquare(selection[i]) == null;
            squaresData.Add(position, isSquareFree);
        }
        squareSelector.ShowSelection(squaresData);
    }
  
}
