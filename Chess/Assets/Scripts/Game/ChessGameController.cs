using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceCreator))]
public class ChessGameController : MonoBehaviour
{
    [SerializeField] private BoardLayout startingBoardLayout;
    [SerializeField] private Board board;
    private PieceCreator pieceCreator;
    Player whitePlayer;
    Player blackPlayer;
    Player activePlayer;


    private void Awake() 
    {
        SetDependencies();
        whitePlayer = new Player();
        blackPlayer = new Player();
    }

    private void SetDependencies() 
    {
        pieceCreator = GetComponent<PieceCreator>();

    }
    private void Start() 
    {
        StartNewGame();
    }

    private void StartNewGame() 
    {
        CreatePiecesFromLayout(startingBoardLayout);
        whitePlayer.SetTeamColor(TeamColor.White);
        blackPlayer.SetTeamColor(TeamColor.Black);
        activePlayer = whitePlayer;
    }

    private void CreatePiecesFromLayout(BoardLayout startingBoardLayout) { //Aca elige las piezas a las cuales crear
        for (int i=0; i< startingBoardLayout.GetPiecesCount(); i++) {
            Vector2Int squareCoords = startingBoardLayout.GetSquareCoordsAtIndex(i);
            TeamColor team = startingBoardLayout.GetSquareTeamColorAtIndex(i);
            string typeName = startingBoardLayout.GetSquarePieceNameAtIndex(i);
            Type type = Type.GetType(typeName);
            CreatePieceAndInitialize(squareCoords,team,type);
        }
    }

    private void CreatePieceAndInitialize(Vector2Int squareCoords, TeamColor team, Type type) {
        Piece newPiece = pieceCreator.CreatePiece(type).GetComponent<Piece>();
        newPiece.SetData(squareCoords,team, board);
        Material teamMaterial = pieceCreator.GetTeamMaterial(team);
        newPiece.SetMaterial(teamMaterial);
        board.addToMatrix(newPiece,squareCoords);
    }

    internal void EndTurn()
    {
        activePlayer = activePlayer == whitePlayer ? blackPlayer : whitePlayer;
    }

    internal bool IsTeamTurnActive(TeamColor team)
    {
        return activePlayer.team == team;
    }
}
