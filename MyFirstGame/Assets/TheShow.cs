using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TheShow : MonoBehaviour
{
    public ArrayList strATOM;
    public CAtom[] atom;
    public CMolecula[] molecula;
    public int currentMol, numberMol;
    public float mouseScroll;
    public new GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I am alive!");
        camera = GameObject.Find("Main Camera");
        Vector3 campos = camera.transform.position;
        camera.transform.position += new Vector3(0, -1f, 1f);
        strATOM = new ArrayList();
        currentMol = 4;
        MoleculaReader(currentMol);
    }

    // Update is called once per frame
    public float angle;
    public float x;
    public float y;
    public float speed;

    void Update()
    {
        //molecula[currentMol].handle.transform.Rotate(Vector3.up, angle, Space.World);
        angle = -0.1f;
        speed = 5f;

        //Вращаю объект
        if (Input.GetMouseButton(0))
        {
     
            y = -speed * Input.GetAxis("Mouse X");
            x = speed * Input.GetAxis("Mouse Y");
            molecula[currentMol].handle.transform.Rotate(x, y, 0);
        }

        mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        camera.transform.position += new Vector3(0, 0, mouseScroll * 5);

      
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), molecula[currentMol].fullname);
        if (GUI.Button(new Rect(Screen.width/2, Screen.height - 40, 40, 20), " > "))
        {
            GameObject.Destroy(molecula[currentMol].handle);
            currentMol = (currentMol < numberMol - 1) ? ++currentMol : 0;
            MoleculaReader(currentMol);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height - 40, 40, 20), " < "))
        {
            GameObject.Destroy(molecula[currentMol].handle);
            currentMol = (currentMol > 0) ? --currentMol : numberMol - 1;
            MoleculaReader(currentMol);
        }
    }

    void MoleculaReader(int number)
    {
        string inputDir = $"_ligands";
        string[] directory = Directory.GetFiles(inputDir);
        numberMol = directory.Length;
        molecula = new CMolecula[numberMol];

        string inputFile = $"_ligands\\ALA.pdb";

        for (int ifile = 0; ifile < numberMol; ifile++)
        {
            inputFile = directory[ifile];
            string filename = Path.GetFileNameWithoutExtension(inputFile);
            string fullname = "";
            switch (filename)
            {
                case "ALA": fullname = "ALA - АЛАНИН"; break;
                case "ARG": fullname = "ARG - АРГИНИН"; break;
                case "ASN": fullname = "ASN - АСПАРАГИН"; break;
                case "ASP": fullname = "ASP - АСПАРАГИНОВАЯ К-ТА"; break;
                case "CYS": fullname = "CYS - ЦИСТЕИН"; break;
                case "GLN": fullname = "GLN - ГЛУТАМИН"; break;
                case "GLU": fullname = "GLU - ГЛУТАМИНОВАЯ К-ТА"; break;
                case "GLY": fullname = "GLY - ГЛИЦИН"; break;
                case "HIS": fullname = "HIS - ГИСТИДИН"; break;
                case "ILE": fullname = "ILE - ИЗОЛЕЙЦИН"; break;
                case "LEU": fullname = "LEU - ЛЕЙЦИН"; break;
                case "LYS": fullname = "LIS - ЛИЗИН"; break;
                case "MET": fullname = "MET - МЕТИОНИН"; break;
                case "PHE": fullname = "PHE - ФЕНИЛАЛАНИН"; break;
                case "PRO": fullname = "PRO - ПРОЛИН"; break;
                case "SER": fullname = "SER - СЕРИН"; break;
                case "THR": fullname = "THR - ТРЕОНИН"; break;
                case "TRP": fullname = "TRP - ТРИПТОФАН"; break;
                case "TYR": fullname = "TYR - ТИРОЗИН"; break;
                case "VAL": fullname = "VAL - ВАЛИН"; break;
            }

            int count = 0;
            StreamReader reader = new StreamReader(inputFile);
            using (reader)
            {
                while (!reader.EndOfStream)
                {
                    string str = reader.ReadLine();
                    string[] strs = str.Split();
                    if (strs[0] == "ATOM")
                    {
                        strATOM.Add(str);
                        count++;
                    }
                }
            }
            molecula[ifile] = new CMolecula();
            molecula[ifile].fullname = fullname;
            molecula[ifile].name = filename;
            if (ifile == currentMol) // сurrentMol == number ?
            {
                molecula[ifile].handle = new GameObject();
                molecula[ifile].handle.name = molecula[ifile].name;
            
                atom = new CAtom[count];
                for (int iString = 0; iString < count; iString++)
                {
                    string str = (string)strATOM[iString];
                    atom[iString] = new CAtom();
                    atom[iString].stringname = str.Substring(0, 6).Trim();
                    atom[iString].number = Convert.ToInt32(str.Substring(6, 5).Trim());
                    atom[iString].atomname = str.Substring(12, 4).Trim();
                    atom[iString].altLoc = str.Substring(16, 1)[0];
                    atom[iString].residue = str.Substring(17, 3).Trim();
                    atom[iString].chain_id = str.Substring(21, 1).Trim();
                    atom[iString].nresidue = Convert.ToInt32(str.Substring(22, 4).Trim());
                    atom[iString].iCode = str.Substring(26, 1)[0];
                    atom[iString].x = (float)Convert.ToDouble(str.Substring(30, 8).Trim().Replace(".",","));
                    atom[iString].y = (float)Convert.ToDouble(str.Substring(38, 8).Trim().Replace(".", ","));
                    atom[iString].z = (float)Convert.ToDouble(str.Substring(46, 8).Trim().Replace(".", ","));
                    atom[iString].occupancy = (float)Convert.ToDouble(str.Substring(54, 6).Trim().Replace(".", ","));
                    atom[iString].temp = (float)Convert.ToDouble(str.Substring(60, 6).Trim().Replace(".", ","));
                    atom[iString].symbol = str.Substring(76, 2).Trim();
                    atom[iString].charge = " 0";

                    //COLOR
//                    switch (atom[iString].symbol)
                    switch (atom[iString].atomname.Substring(0, 1))
                    {
                        case "H": atom[iString].color = Color.white; break;
                        case "C": atom[iString].color = Color.gray; break;
                        case "O": atom[iString].color = Color.red; break;
                        case "N": atom[iString].color = Color.blue; break;
                        default: atom[iString].color = Color.black; break;
                    };
                   
                    //SIZE
                    switch (atom[iString].atomname.Substring(0, 1))
                    {
                        case "H": atom[iString].size = 0.31f; break;
                        case "C": atom[iString].size = 0.76f; break;
                        case "O": atom[iString].size = 0.71f; break;
                        case "N": atom[iString].size = 0.66f; break;
                        default: atom[iString].size = 0.99f; break;
                    };
                    atom[iString].sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    atom[iString].sphere.GetComponent<Renderer>().material.SetFloat("_SpecularHighlights", 0);
                    Renderer rend = atom[iString].sphere.GetComponent<Renderer>();
                    rend.material.color = atom[iString].color;
                    float t = 2.0f;
                    atom[iString].sphere.transform.localScale = new Vector3(atom[iString].size * t, atom[iString].size * t, atom[iString].size * t);
                    atom[iString].sphere.transform.position = new Vector3(-atom[iString].x, atom[iString].y, atom[iString].z);
                    atom[iString].sphere.transform.parent = molecula[ifile].handle.transform;
                    atom[iString].sphere.name = atom[iString].symbol;
                }
            }
        }
    }


}
