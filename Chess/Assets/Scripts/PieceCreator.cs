using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] piecesPrefabs;
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material whiteMaterial;

    private Dictionary<string, GameObject> nameToPieceDic = new Dictionary<string, GameObject>();

    private void Awake() {
        foreach (var piece in piecesPrefabs) {
            nameToPieceDic.Add(piece.GetComponent<Piece>().GetType().ToString(), piece); //King; Skin del king (Material)
        }
    }
    public GameObject CreatePiece(Type type) {
        GameObject prefab = nameToPieceDic[type.ToString()]; //Pide la sking con el nombre en el diccionario
        if (prefab)
        {
            GameObject newPiece = Instantiate(prefab);
            return newPiece;
        }
        return null;
    }
    public Material GetTeamMaterial(TeamColor team) { 
        return team==TeamColor.White ? whiteMaterial : blackMaterial; //Devuelve el material segun el team
    }
}
