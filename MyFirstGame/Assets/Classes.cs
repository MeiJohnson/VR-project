using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour
{

}
public class CAtom
{
    public string stringname;   // 01-06 "ATOM"
    public int number;          // 07-11 atom serial number
    public string atomname;     // 13-16 atom name
    public char altLoc;         // 17    alternate location indicator
    public string residue;      // 18-20 residue name
    public string chain_id;     // 22    chain ID
    public int nresidue;        // 23-26 residue sequence number
    public char iCode;          // 27    code for insertion of residues 
    public float x;             // 31-38 coord x
    public float y;             // 39-46 coord y
    public float z;             // 47-54 coord z
    public float occupancy;     // 55-60 occupancy
    public float temp;          // 61-66 temperature factor
    public string symbol;       // 77-78 symbol
    public string charge;       // 79-80 charge of the atom

    public GameObject sphere;
    public float size;
    public Color color;
}
public class CMolecula
{
    public int natoms;
    public CAtom[] atom;
    public string name, fullname;
    public GameObject handle;
}