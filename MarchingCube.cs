using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// REQUIRED:
//  1. GridPoint script 
//  2. a grid of GridPoint
//  3. define Points.A - H before calling methods

// PURPOSE: defines mesh of each 'marching cube' config (2^8 = 256)

#region --- helpers ---
public class GridPoint
{
    private Vector3 _position = Vector3.zero;
    private bool _on = false; 

    public Vector3 Position 
    {
        get
        {
            return _position;
        }
        set
        {
            _position = new Vector3(value.x, value.y, value.z);
        }
    }
    public bool On
    {
        get
        {
            return _on;
        }
        set
        {
            _on = value;
        }
    }
    public override string ToString()
    {
        return string.Format("{0} {1}", Position, On);
    }
}
public static class Points
{
    /*      E ------ F
     *      |        |
     *      | A ------- B
     *      | |      |  |
     *      G | ---- H  |
     *        |         |
     *        C ------- D
    */

    // CORNERS
    public static GridPoint A = null;
    public static GridPoint B = null;
    public static GridPoint C = null;
    public static GridPoint D = null;
    public static GridPoint E = null;
    public static GridPoint F = null;
    public static GridPoint G = null;
    public static GridPoint H = null;

    // HALF-WAY POINTS
    public static Vector3 ab
    {
        get { return C.Position + new Vector3(0.5f, 1f, 0f); }
    }
    public static Vector3 ba
    {
        get { return C.Position + new Vector3(0.5f, 1f, 0f); }
    }
    public static Vector3 bd
    {
        get { return C.Position + new Vector3(1f, 0.5f, 0f); }
    }
    public static Vector3 db
    {
        get { return C.Position + new Vector3(1f, 0.5f, 0f); }
    }
    public static Vector3 dc
    {
        get { return C.Position + new Vector3(0.5f, 0f, 0f); }
    }
    public static Vector3 cd
    {
        get { return C.Position + new Vector3(0.5f, 0f, 0f); }
    }
    public static Vector3 ca
    {
        get { return C.Position + new Vector3(0f, 0.5f, 0f); }
    }
    public static Vector3 ac
    {
        get { return C.Position + new Vector3(0f, 0.5f, 0f); }
    }

    public static Vector3 ef
    {
        get { return C.Position + new Vector3(0.5f, 1f, 1f); }
    }
    public static Vector3 fe
    {
        get { return C.Position + new Vector3(0.5f, 1f, 1f); }
    }
    public static Vector3 fh
    {
        get { return C.Position + new Vector3(1f, 0.5f, 1f); }
    }
    public static Vector3 hf
    {
        get { return C.Position + new Vector3(1f, 0.5f, 1f); }
    }
    public static Vector3 hg
    {
        get { return C.Position + new Vector3(0.5f, 0f, 1f); }
    }
    public static Vector3 gh
    {
        get { return C.Position + new Vector3(0.5f, 0f, 1f); }
    }
    public static Vector3 ge
    {
        get { return C.Position + new Vector3(0f, 0.5f, 1f); }
    }
    public static Vector3 eg
    {
        get { return C.Position + new Vector3(0f, 0.5f, 1f); }
    }

    public static Vector3 fb
    {
        get { return C.Position + new Vector3(1f, 1f, 0.5f); }
    }
    public static Vector3 bf
    {
        get { return C.Position + new Vector3(1f, 1f, 0.5f); }
    }
    public static Vector3 ae
    {
        get { return C.Position + new Vector3(0f, 1f, 0.5f); }
    }
    public static Vector3 ea
    {
        get { return C.Position + new Vector3(0f, 1f, 0.5f); }
    }
    public static Vector3 hd
    {
        get { return C.Position + new Vector3(1f, 0f, 0.5f); }
    }
    public static Vector3 dh
    {
        get { return C.Position + new Vector3(1f, 0f, 0.5f); }
    }
    public static Vector3 cg
    {
        get { return C.Position + new Vector3(0f, 0f, 0.5f); }
    }
    public static Vector3 gc
    {
        get { return C.Position + new Vector3(0f, 0f, 0.5f); }
    }
}
public static class Bits
{
    // TO CHECK IF BIT IS ON OR OFF
    public static int A = (int)Mathf.Pow(2, 0);
    public static int B = (int)Mathf.Pow(2, 1);
    public static int C = (int)Mathf.Pow(2, 2);
    public static int D = (int)Mathf.Pow(2, 3);
    public static int E = (int)Mathf.Pow(2, 4);
    public static int F = (int)Mathf.Pow(2, 5);
    public static int G = (int)Mathf.Pow(2, 6);
    public static int H = (int)Mathf.Pow(2, 7);

    public static string BinaryForm(int config)
    {
        string A = ((config & (1 << 0)) != 0) ? "A" : "-";
        string B = ((config & (1 << 1)) != 0) ? "B" : "-";
        string C = ((config & (1 << 2)) != 0) ? "C" : "-";
        string D = ((config & (1 << 3)) != 0) ? "D" : "-";
        string E = ((config & (1 << 4)) != 0) ? "E" : "-";
        string F = ((config & (1 << 5)) != 0) ? "F" : "-";
        string G = ((config & (1 << 6)) != 0) ? "G" : "-";
        string H = ((config & (1 << 7)) != 0) ? "H" : "-";

        return H + G + F + E + D + C + B + A;
    }
    public static bool isBitSet(int config, string letter)
    {
        bool ret = false;

        switch (letter)
        {
            case "A":
                ret = ((config & (1 << 0)) != 0);
                break;
            case "B":
                ret = ((config & (1 << 1)) != 0);
                break;
            case "C":
                ret = ((config & (1 << 2)) != 0);
                break;
            case "D":
                ret = ((config & (1 << 3)) != 0);
                break;
            case "E":
                ret = ((config & (1 << 4)) != 0);
                break;
            case "F":
                ret = ((config & (1 << 5)) != 0);
                break;
            case "G":
                ret = ((config & (1 << 6)) != 0);
                break;
            case "H":
                ret = ((config & (1 << 7)) != 0);
                break;
        }

        return ret;
    }
}
public static class UVCoord
{
    /*  A ------ B
        |        |
        |        |
        C ------ D  */
    public static Vector2 A = new Vector2(0, 1);
    public static Vector2 B = new Vector2(1, 1);
    public static Vector2 C = new Vector2(0, 0);
    public static Vector2 D = new Vector2(1, 0);
}
#endregion

public static class MarchingCube
{
    //grid of points
    public static GridPoint[,,] grd = null;
    //mesh data
    public static List<Vector3> vertices = new List<Vector3>();
    public static List<int> triangles = new List<int>();
    public static List<Vector2> uv = new List<Vector2>();

    public static void Clear()
    {
        vertices.Clear();
        triangles.Clear();
        uv.Clear();
    }
    public static void MarchCubes()
    {
        int config = 0;

        Clear();

        for (int z = 0; z < grd.GetLength(2) - 1; z++)
        {
            for (int y = 0; y < grd.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < grd.GetLength(0) - 1; x++)
                {
                    //current cube corners
                    Points.A = grd[x, y + 1, z];
                    Points.B = grd[x + 1, y + 1, z];
                    Points.C = grd[x, y, z];
                    Points.D = grd[x + 1, y, z];
                    Points.E = grd[x, y + 1, z + 1];
                    Points.F = grd[x + 1, y + 1, z + 1];
                    Points.G = grd[x, y, z + 1];
                    Points.H = grd[x + 1, y, z + 1];

                    //cube face configuration
                    config = GetCubeConfig();                    
                    IsoFaces(config);
                }
            }
        }
    }
    public static int SetCubeToConfig(Vector3 p)
    {
        int x = (int)p.x;
        int y = (int)p.y;
        int z = (int)p.z;

        try
        {
            Points.A = grd[x, y + 1, z];
            Points.B = grd[x + 1, y + 1, z];
            Points.C = grd[x, y, z];
            Points.D = grd[x + 1, y, z];
            Points.E = grd[x, y + 1, z + 1];
            Points.F = grd[x + 1, y + 1, z + 1];
            Points.G = grd[x, y, z + 1];
            Points.H = grd[x + 1, y, z + 1];
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }

        int config = GetCubeConfig();
        IsoFaces(config);

        return config;
    }
    public static int GetCubeConfig()
    {
        // config code based on current cube on points
        int config = 0;

        config += Points.A.On ? Bits.A : 0;
        config += Points.B.On ? Bits.B : 0;
        config += Points.C.On ? Bits.C : 0;
        config += Points.D.On ? Bits.D : 0;
        config += Points.E.On ? Bits.E : 0;
        config += Points.F.On ? Bits.F : 0;
        config += Points.G.On ? Bits.G : 0;
        config += Points.H.On ? Bits.H : 0;

        return config;
    }
    public static int IsoFaces(int config)
    {
        // IMPORTANT GOLDEN INFORMATION!!! - the mesh triangles to make for each cube configuration

        // SANITY CHECK RULES:
        //  1. on cube corners are considered the inside of a mesh 2^8 = 256 
        //  2. connect triangle points in clockwise direction, on corners inside the mesh
        //  3. on off corners should always be separated by triangle corner
        //  4. same side corners should not have separation by triangle corner

        //vertices
        int beforeCount = vertices.Count;
        switch (config)
        {
            case 0:     // --------
                break;
            case 1:     // -------A
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                break;
            case 2:     // ------B-
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                break;
            case 3:     // ------BA
                vertices.Add(Points.bf);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                break;
            case 4:     // -----C--
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                break;
            case 5:     // -----C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                break;
            case 6:     // -----CB-
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                break;
            case 7:     // -----CBA                
                vertices.Add(Points.bf);
                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.bd);
                vertices.Add(Points.fb);
                vertices.Add(Points.cd);
                vertices.Add(Points.fb);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                break;
            case 8:     // ----D---
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.db);
                break;
            case 9:     // ----D--A
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.ab);
                vertices.Add(Points.dc);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 10:    // ----D-B-
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                break;
            case 11:    // ----D-BA
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.dc);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.cd);
                break;
            case 12:    // ----DC--
                vertices.Add(Points.db);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.db);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                break;
            case 13:    // ----DC-A
                vertices.Add(Points.ba);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.ba);
                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.db);
                vertices.Add(Points.ba);
                break;
            case 14:    // ----DCB-
                vertices.Add(Points.bf);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.cg);
                vertices.Add(Points.ba);
                vertices.Add(Points.ac);
                vertices.Add(Points.cg);
                break;
            case 15:    // ----DCBA
                vertices.Add(Points.bf);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                break;
            case 16:    // ---E----
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                break;
            case 17:    // ---E---A
                vertices.Add(Points.ac);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                break;
            case 18:    // ---E--B-
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                break;
            case 19:    // ---E--BA
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ac);
                vertices.Add(Points.ac);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.ac);
                vertices.Add(Points.bf);
                break;
            case 20:    // ---E-C--
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                break;
            case 21:    // ---E-C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.cg);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                break;
            case 22:    // ---E-CB-
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                break;
            case 23:    // ---E-CBA
                vertices.Add(Points.bf);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.fb);
                vertices.Add(Points.gc);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                break;
            case 24:    // ---ED---
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                break;
            case 25:    // ---ED--A
                vertices.Add(Points.ab);
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.ab);
                vertices.Add(Points.dc);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 26:    // ---ED-B-
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                break;
            case 27:    // ---ED-BA
                vertices.Add(Points.bf);
                vertices.Add(Points.ac);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.ac);
                vertices.Add(Points.fe);
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.dh);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 28:    // ---EDC--
                vertices.Add(Points.bd);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ac);
                vertices.Add(Points.cg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                break;
            case 29:    // ---EDC-A
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.cg);
                vertices.Add(Points.fe);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.dh);
                break;
            case 30:    // ---EDCB-
                vertices.Add(Points.bf);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.cg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ba);
                vertices.Add(Points.ac);
                vertices.Add(Points.gc);
                break;
            case 31:    // ---EDCBA
                vertices.Add(Points.bf);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.cg);
                vertices.Add(Points.fe);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                break;
            case 32:    // --F-----
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                break;
            case 33:    // --F----A
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                break;
            case 34:    // --F---B-
                vertices.Add(Points.ba);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.fh);
                break;
            case 35:    // --F---BA
                vertices.Add(Points.fh);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ac);
                vertices.Add(Points.fe);
                vertices.Add(Points.ea);
                vertices.Add(Points.ac);
                break;
            case 36:    // --F--C--
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                break;
            case 37:    // --F--C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                break;
            case 38:    // --F--CB-
                vertices.Add(Points.fe);
                vertices.Add(Points.bd);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                break;
            case 39:    // --F--CBA
                vertices.Add(Points.fe);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.cd);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.bd);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                break;
            case 40:    // --F-D---
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                break;
            case 41:    // --F-D--A
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.dc);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 42:    // --F-D-B-
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.dh);
                break;
            case 43:    // --F-D-BA
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.dc);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.ea);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 44:    // --F-DC--
                vertices.Add(Points.db);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.db);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                break;
            case 45:    // --F-DC-A
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                break;
            case 46:    // --F-DCB-
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                break;
            case 47:    // --F-DCBA
                vertices.Add(Points.fe);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                break;
            case 48:    // --FE----
                vertices.Add(Points.eg);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                break;
            case 49:    // --FE---A
                vertices.Add(Points.ac);
                vertices.Add(Points.fh);
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.fh);
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.hf);
                break;
            case 50:    // --FE--B-
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fh);
                vertices.Add(Points.fh);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.ea);
                vertices.Add(Points.ab);
                vertices.Add(Points.fh);
                break;
            case 51:    // --FE--BA
                vertices.Add(Points.eg);
                vertices.Add(Points.bd);
                vertices.Add(Points.fh);
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                break;
            case 52:    // --FE-C--
                vertices.Add(Points.ea);
                vertices.Add(Points.fh);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                break;
            case 53:    // --FE-C-A
                vertices.Add(Points.cg);
                vertices.Add(Points.ab);
                vertices.Add(Points.eg);
                vertices.Add(Points.eg);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.eg);
                vertices.Add(Points.dc);
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                break;
            case 54:    // --FE-CB-
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fh);
                vertices.Add(Points.fh);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                vertices.Add(Points.fh);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                break;
            case 55:    // --FE-CBA
                vertices.Add(Points.hf);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.fh);
                vertices.Add(Points.fh);
                vertices.Add(Points.gc);
                vertices.Add(Points.cd);
                break;
            case 56:    // --FED---
                vertices.Add(Points.ea);
                vertices.Add(Points.fh);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                break;
            case 57:    // --FED--A
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.eg);
                vertices.Add(Points.eg);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.eg);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                break;
            case 58:    // --FED-B-
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fh);
                vertices.Add(Points.fh);
                vertices.Add(Points.ba);
                vertices.Add(Points.dh);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                vertices.Add(Points.fh);
                vertices.Add(Points.hd);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                break;
            case 59:    // --FED-BA
                vertices.Add(Points.fh);
                vertices.Add(Points.eg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.hd);
                vertices.Add(Points.eg);
                vertices.Add(Points.dc);
                break;
            case 60:    // --FEDC--
                vertices.Add(Points.ea);
                vertices.Add(Points.fh);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.db);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                break;
            case 61:    // --FEDC-A
                vertices.Add(Points.fh);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.bd);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.hf);
                break;
            case 62:    // --FEDCB-
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.hd);
                vertices.Add(Points.ge);
                vertices.Add(Points.hd);
                vertices.Add(Points.hf);
                vertices.Add(Points.ae);
                vertices.Add(Points.gc);
                vertices.Add(Points.ge);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.gc);
                break;
            case 63:    // --FEDCBA
                vertices.Add(Points.eg);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                vertices.Add(Points.dh);
                break;
            case 64:    // -G------
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                break;
            case 65:    // -G-----A
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                break;
            case 66:    // -G----B-
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                break;
            case 67:    // -G----BA
                vertices.Add(Points.bf);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                break;
            case 68:    // -G---C--
                vertices.Add(Points.cd);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ge);
                break;
            case 69:    // -G---C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.gh);
                vertices.Add(Points.ae);
                vertices.Add(Points.eg);
                vertices.Add(Points.hg);
                break;
            case 70:    // -G---CB-
                vertices.Add(Points.ca);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                break;
            case 71:    // -G---CBA
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.eg);
                vertices.Add(Points.bf);
                vertices.Add(Points.eg);
                vertices.Add(Points.hg);
                vertices.Add(Points.bf);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                break;
            case 72:    // -G--D---
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dh);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                break;
            case 73:    // -G--D--A
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.dh);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.ab);
                vertices.Add(Points.dc);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 74:    // -G--D-B-
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dh);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                break;
            case 75:    // -G--D-BA
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.bf);
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.bf);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dh);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                break;
            case 76:    // -G--DC--
                vertices.Add(Points.ca);
                vertices.Add(Points.dh);
                vertices.Add(Points.db);
                vertices.Add(Points.ca);
                vertices.Add(Points.ge);
                vertices.Add(Points.dh);
                vertices.Add(Points.dh);
                vertices.Add(Points.eg);
                vertices.Add(Points.gh);
                break;
            case 77:    // -G--DC-A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ae);
                vertices.Add(Points.eg);
                vertices.Add(Points.gh);
                break;
            case 78:    // -G--DCB-
                vertices.Add(Points.bf);
                vertices.Add(Points.hg);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.hg);
                vertices.Add(Points.ac);
                vertices.Add(Points.eg);
                vertices.Add(Points.hg);
                vertices.Add(Points.ba);
                vertices.Add(Points.ac);
                vertices.Add(Points.hg);
                break;
            case 79:    // -G--DCBA
                vertices.Add(Points.bf);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.gh);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                vertices.Add(Points.hg);
                break;
            case 80:    // -G-E----
                vertices.Add(Points.ea);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                break;
            case 81:    // -G-E---A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.cg);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                break;
            case 82:    // -G-E--B-
                vertices.Add(Points.ea);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                break;
            case 83:    // -G-E--BA
                vertices.Add(Points.bf);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.cg);
                vertices.Add(Points.ac);
                break;
            case 84:    // -G-E-C--
                vertices.Add(Points.ea);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                break;
            case 85:    // -G-E-C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                break;
            case 86:    // -G-E-CB-
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.gh);
                vertices.Add(Points.gh);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                break;
            case 87:    // -G-E-CBA
                vertices.Add(Points.bf);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.cd);
                break;
            case 88:    // -G-ED---
                vertices.Add(Points.ea);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.dh);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                break;
            case 89:    // -G-ED--A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                break;
            case 90:    // -G-ED-B-
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                vertices.Add(Points.ea);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.ef);
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                break;
            case 91:    // -G-ED-BA
                vertices.Add(Points.fb);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fe);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.bf);
                vertices.Add(Points.ac);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ef);
                vertices.Add(Points.gc);
                vertices.Add(Points.bf);
                vertices.Add(Points.fe);
                vertices.Add(Points.ac);
                break;
            case 92:    // -G-EDC--
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.ac);
                vertices.Add(Points.ac);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ac);
                vertices.Add(Points.hg);
                vertices.Add(Points.bd);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                break;
            case 93:    // -G-EDC-A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ab);
                vertices.Add(Points.dh);
                break;
            case 94:    // -G-EDCB-
                vertices.Add(Points.fb);
                vertices.Add(Points.ba);
                vertices.Add(Points.dh);
                vertices.Add(Points.ae);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.ba);
                vertices.Add(Points.ac);
                vertices.Add(Points.ca);
                vertices.Add(Points.ae);
                vertices.Add(Points.hg);
                vertices.Add(Points.dh);
                vertices.Add(Points.ac);
                vertices.Add(Points.hg);
                break;
            case 95:    // -G-EDCBA
                vertices.Add(Points.bf);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.gh);
                break;
            case 96:    // -GF-----
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                break;
            case 97:    // -GF----A
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.fe);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                break;
            case 98:    // -GF---B-
                vertices.Add(Points.fe);
                vertices.Add(Points.bd);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.fe);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                break;
            case 99:    // -GF---BA
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.fh);
                vertices.Add(Points.hg);
                vertices.Add(Points.bd);
                vertices.Add(Points.bd);
                vertices.Add(Points.cg);
                vertices.Add(Points.ac);
                vertices.Add(Points.cg);
                vertices.Add(Points.bd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.hg);
                break;
            case 100:   // -GF--C--
                vertices.Add(Points.ca);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                break;
            case 101:   // -GF--C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.gh);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.fe);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                break;
            case 102:   // -GF--CB-
                vertices.Add(Points.ca);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.fe);
                vertices.Add(Points.bd);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.fe);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                break;
            case 103:   // -GF--CBA
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.dc);
                vertices.Add(Points.hf);
                vertices.Add(Points.dc);
                vertices.Add(Points.db);
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.hg);
                break;
            case 104:   // -GF-D---
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                break;
            case 105:   // -GF-D--A
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.dc);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                vertices.Add(Points.ae);
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                break;
            case 106:   // -GF-D-B-
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.hd);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.dh);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                break;
            case 107:   // -GF-D-BA
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.dc);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ac);
                vertices.Add(Points.dh);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                break;
            case 108:   // -GF-DC--
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ac);
                vertices.Add(Points.dh);
                vertices.Add(Points.ca);
                vertices.Add(Points.eg);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.ca);
                vertices.Add(Points.gh);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                vertices.Add(Points.fe);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                break;
            case 109:   // -GF-DC-A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.gh);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.dh);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.fe);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                break;
            case 110:   // -GF-DCB-
                vertices.Add(Points.ef);
                vertices.Add(Points.ac);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.ef);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.hg);
                break;
            case 111:   // -GF-DCBA
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                vertices.Add(Points.ef);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.hg);
                break;
            case 112:   // -GFE----
                vertices.Add(Points.ea);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.gh);
                vertices.Add(Points.bf);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                break;
            case 113:   // -GFE---A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.bf);
                vertices.Add(Points.fh);
                break;
            case 114:   // -GFE--B-
                vertices.Add(Points.ea);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                vertices.Add(Points.gh);
                vertices.Add(Points.ab);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                vertices.Add(Points.ab);
                vertices.Add(Points.bd);
                vertices.Add(Points.fh);
                break;
            case 115:   // -GFE--BA
                vertices.Add(Points.ac);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.fh);
                vertices.Add(Points.ac);
                vertices.Add(Points.gh);
                vertices.Add(Points.cg);
                break;
            case 116:   // -GFE-C--
                vertices.Add(Points.ea);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.gh);
                vertices.Add(Points.ae);
                vertices.Add(Points.cd);
                vertices.Add(Points.ac);
                vertices.Add(Points.gh);
                vertices.Add(Points.bf);
                vertices.Add(Points.fh);
                break;
            case 117:   // -GFE-C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.gh);
                vertices.Add(Points.bf);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                break;
            case 118:   // -GFE-CB-
                vertices.Add(Points.db);
                vertices.Add(Points.hg);
                vertices.Add(Points.dc);
                vertices.Add(Points.db);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.ac);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.db);
                break;
            case 119:   // -GFE-CBA
                vertices.Add(Points.bd);
                vertices.Add(Points.gh);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                break;
            case 120:   // -GFED---
                vertices.Add(Points.ea);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.gh);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.fh);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                break;
            case 121:   // -GFED--A
                vertices.Add(Points.ab);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.gh);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.gh);
                vertices.Add(Points.bf);
                vertices.Add(Points.fh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                break;
            case 122:   // -GFED-B-
                vertices.Add(Points.ea);
                vertices.Add(Points.ab);
                vertices.Add(Points.fh);
                vertices.Add(Points.hg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fh);
                vertices.Add(Points.fh);
                vertices.Add(Points.ab);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ae);
                vertices.Add(Points.gh);
                vertices.Add(Points.ab);
                vertices.Add(Points.dc);
                vertices.Add(Points.hd);
                break;
            case 123:   // -GFED-BA
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.cg);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.hd);
                break;
            case 124:   // -GFEDC--
                vertices.Add(Points.ae);
                vertices.Add(Points.bd);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.bd);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.hf);
                break;
            case 125:   // -GFEDC-A
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.bd);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.hf);
                break;
            case 126:   // -GFEDCB-
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                break;
            case 127:   // -GFEDCBA
                vertices.Add(Points.hf);
                vertices.Add(Points.gh);
                vertices.Add(Points.dh);
                break;
            case 128:   // H-------
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                break;
            case 129:   // H------A
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                break;
            case 130:   // H-----B-
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.bf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                break;
            case 131:   // H-----BA
                vertices.Add(Points.bf);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.bf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                break;
            case 132:   // H----C--
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.hg);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                break;
            case 133:   // H----C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.hg);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                break;
            case 134:   // H----CB-
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.bf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                break;
            case 135:   // H----CBA
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.fb);
                vertices.Add(Points.ae);
                vertices.Add(Points.dc);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                vertices.Add(Points.dc);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.cd);
                vertices.Add(Points.cd);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                break;
            case 136:   // H---D---
                vertices.Add(Points.db);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                break;
            case 137:   // H---D--A
                vertices.Add(Points.hf);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.dc);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 138:   // H---D-B-
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.fb);
                break;
            case 139:   // H---D-BA
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.fb);
                vertices.Add(Points.ea);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 140:   // H---DC--
                vertices.Add(Points.hf);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.hf);
                vertices.Add(Points.db);
                vertices.Add(Points.ca);
                vertices.Add(Points.hg);
                vertices.Add(Points.fh);
                vertices.Add(Points.gc);
                break;
            case 141:   // H---DC-A
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.hg);
                vertices.Add(Points.fh);
                vertices.Add(Points.bd);
                vertices.Add(Points.hg);
                break;
            case 142:   // H---DCB-
                vertices.Add(Points.bf);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.bf);
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.hg);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.gc);
                break;
            case 143:   // H---DCBA
                vertices.Add(Points.bf);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                break;
            case 144:   // H--E----
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                break;
            case 145:   // H--E---A
                vertices.Add(Points.ab);
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                break;
            case 146:   // H--E--B-
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                break;
            case 147:   // H--E--BA
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ac);
                vertices.Add(Points.ac);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.bf);
                vertices.Add(Points.fe);
                vertices.Add(Points.ac);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.bf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                break;
            case 148:   // H--E-C--
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                vertices.Add(Points.cd);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                break;
            case 149:   // H--E-C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.cg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.cg);
                vertices.Add(Points.fe);
                vertices.Add(Points.eg);
                vertices.Add(Points.cd);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                break;
            case 150:   // H--E-CB-
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);

                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);

                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.ef);
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                break;
            case 151:   // H--E-CBA
                vertices.Add(Points.bf);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.cg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                vertices.Add(Points.fe);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                break;
            case 152:   // H--ED---
                vertices.Add(Points.hf);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                break;
            case 153:   // H--ED--A
                vertices.Add(Points.ab);
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                vertices.Add(Points.ab);
                vertices.Add(Points.dc);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 154:   // H--ED-B-
                vertices.Add(Points.bf);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.fb);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                break;
            case 155:   // H--ED-BA
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.ge);
                vertices.Add(Points.cd);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.fh);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ge);
                break;
            case 156:   // H--EDC--
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.db);
                vertices.Add(Points.db);
                vertices.Add(Points.hg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.bd);
                vertices.Add(Points.cg);
                vertices.Add(Points.eg);
                vertices.Add(Points.hf);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                break;
            case 157:   // H--EDC-A
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.db);
                vertices.Add(Points.hg);
                vertices.Add(Points.bd);
                vertices.Add(Points.cg);
                vertices.Add(Points.cg);
                vertices.Add(Points.db);
                vertices.Add(Points.hg);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.cg);
                vertices.Add(Points.fh);
                vertices.Add(Points.db);
                vertices.Add(Points.hg);
                vertices.Add(Points.cg);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                break;
            case 158:   // H--EDCB-
                vertices.Add(Points.fb);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.fb);
                vertices.Add(Points.ba);
                vertices.Add(Points.cg);
                vertices.Add(Points.bf);
                vertices.Add(Points.hg);
                vertices.Add(Points.fh);
                vertices.Add(Points.ba);
                vertices.Add(Points.ac);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.cg);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.cg);
                break;
            case 159:   // H--EDCBA
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.fh);
                vertices.Add(Points.ge);
                vertices.Add(Points.gh);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ge);
                break;
            case 160:   // H-F-----
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                break;
            case 161:   // H-F----A
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.ae);
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                break;
            case 162:   // H-F---B-
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.hd);
                vertices.Add(Points.ab);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                break;
            case 163:   // H-F---BA
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.hd);
                vertices.Add(Points.ea);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ea);
                vertices.Add(Points.ac);
                break;
            case 164:   // H-F--C--
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.hg);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                break;
            case 165:   // H-F--C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.fb);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                break;
            case 166:   // H-F--CB-
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ef);
                vertices.Add(Points.ba);
                vertices.Add(Points.hd);
                vertices.Add(Points.ab);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                break;
            case 167:   // H-F--CBA
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.ef);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dc);
                break;
            case 168:   // H-F-D---
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.dc);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                vertices.Add(Points.cd);
                break;
            case 169:   // H-F-D--A
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.dc);
                vertices.Add(Points.fb);
                vertices.Add(Points.bd);
                vertices.Add(Points.dc);
                vertices.Add(Points.ae);
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                break;
            case 170:   // H-F-D-B-
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                break;
            case 171:   // H-F-D-BA
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.dc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 172:   // H-F-DC--
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.hg);
                vertices.Add(Points.fb);
                vertices.Add(Points.bd);
                vertices.Add(Points.hg);
                vertices.Add(Points.bd);
                vertices.Add(Points.ac);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.bd);
                vertices.Add(Points.cg);
                break;
            case 173:   // H-F-DC-A
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.ef);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.gc);
                vertices.Add(Points.bf);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.ea);
                break;
            case 174:   // H-F-DCB-
                vertices.Add(Points.fe);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.cg);
                vertices.Add(Points.ba);
                vertices.Add(Points.ac);
                vertices.Add(Points.gc);
                break;
            case 175:   // H-F-DCBA
                vertices.Add(Points.fe);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.cg);
                break;
            case 176:   // H-FE----
                vertices.Add(Points.ea);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.hg);
                break;
            case 177:   // H-FE---A
                vertices.Add(Points.ab);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.gh);
                vertices.Add(Points.eg);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.eg);
                break;
            case 178:   // H-FE--B-
                vertices.Add(Points.ea);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                vertices.Add(Points.hd);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.gh);
                vertices.Add(Points.ab);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                break;
            case 179:   // H-FE--BA
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.gh);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                vertices.Add(Points.gh);
                vertices.Add(Points.ac);
                vertices.Add(Points.hd);
                break;
            case 180:   // H-FE-C--
                vertices.Add(Points.ea);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.hg);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                break;
            case 181:   // H-FE-C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.gh);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dc);
                break;
            case 182:   // H-FE-CB-
                vertices.Add(Points.ea);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                vertices.Add(Points.hd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.cd);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ab);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                vertices.Add(Points.hg);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.hg);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                break;
            case 183:   // H-FE-CBA
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);

                vertices.Add(Points.dc);
                vertices.Add(Points.gh);
                vertices.Add(Points.gc);
                vertices.Add(Points.dc);
                vertices.Add(Points.dh);
                vertices.Add(Points.gh);

                break;
            case 184:   // H-FED---
                vertices.Add(Points.ea);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.cd);
                vertices.Add(Points.fb);
                vertices.Add(Points.bd);
                break;
            case 185:   // H-FED--A
                vertices.Add(Points.ge);
                vertices.Add(Points.cd);
                vertices.Add(Points.gh);
                vertices.Add(Points.ge);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.cd);
                break;
            case 186:   // H-FED-B-
                vertices.Add(Points.ea);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                break;
            case 187:   // H-FED-BA
                vertices.Add(Points.eg);
                vertices.Add(Points.dc);
                vertices.Add(Points.hg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                break;
            case 188:   // H-FEDC--
                vertices.Add(Points.ae);
                vertices.Add(Points.bd);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.ae);
                vertices.Add(Points.gc);
                vertices.Add(Points.ge);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.gc);
                break;
            case 189:   // H-FEDC-A
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                break;
            case 190:   // H-FEDCB-
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.gc);
                vertices.Add(Points.ge);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.gc);
                break;
            case 191:   // H-FEDCBA
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.gh);
                break;
            case 192:   // HG------
                vertices.Add(Points.ge);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ge);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                break;
            case 193:   // HG-----A
                vertices.Add(Points.ge);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ge);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                break;
            case 194:   // HG----B-
                vertices.Add(Points.ge);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ge);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                break;
            case 195:   // HG----BA
                vertices.Add(Points.ge);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ge);
                vertices.Add(Points.hf);
                vertices.Add(Points.hd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.bf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                break;
            case 196:   // HG---C--
                vertices.Add(Points.ge);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ge);
                vertices.Add(Points.hf);
                vertices.Add(Points.cd);
                vertices.Add(Points.cd);
                vertices.Add(Points.fh);
                vertices.Add(Points.hd);
                break;
            case 197:   // HG---C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.hd);
                vertices.Add(Points.ae);
                vertices.Add(Points.eg);
                vertices.Add(Points.dh);
                vertices.Add(Points.eg);
                vertices.Add(Points.fh);
                vertices.Add(Points.dh);
                break;
            case 198:   // HG---CB-
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ge);
                vertices.Add(Points.cd);
                vertices.Add(Points.ge);
                vertices.Add(Points.hd);
                vertices.Add(Points.eg);
                vertices.Add(Points.fh);
                vertices.Add(Points.hd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                break;
            case 199:   // HG---CBA
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.bf);
                vertices.Add(Points.eg);
                vertices.Add(Points.fh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.eg);
                vertices.Add(Points.fb);
                vertices.Add(Points.dh);
                vertices.Add(Points.db);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.dh);
                break;
            case 200:   // HG--D---
                vertices.Add(Points.ge);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.ge);
                vertices.Add(Points.hf);
                vertices.Add(Points.db);
                vertices.Add(Points.gc);
                vertices.Add(Points.eg);
                vertices.Add(Points.cd);
                break;
            case 201:   // HG--D--A
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.hf);
                vertices.Add(Points.hf);
                vertices.Add(Points.gc);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.hf);
                vertices.Add(Points.dc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ge);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                break;
            case 202:   // HG--D-B-
                vertices.Add(Points.bf);
                vertices.Add(Points.eg);
                vertices.Add(Points.fh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.bf);
                vertices.Add(Points.eg);
                vertices.Add(Points.fb);
                vertices.Add(Points.gc);
                break;
            case 203:   // HG--D-BA
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.fb);
                vertices.Add(Points.eg);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.ae);
                vertices.Add(Points.eg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ca);
                break;
            case 204:   // HG--DC--
                vertices.Add(Points.hf);
                vertices.Add(Points.ca);
                vertices.Add(Points.ge);
                vertices.Add(Points.hf);
                vertices.Add(Points.db);
                vertices.Add(Points.ca);
                break;
            case 205:   // HG--DC-A
                vertices.Add(Points.ge);
                vertices.Add(Points.hf);
                vertices.Add(Points.ae);
                vertices.Add(Points.hf);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.ea);
                vertices.Add(Points.hf);
                vertices.Add(Points.ab);
                break;
            case 206:   // HG--DCB-
                vertices.Add(Points.eg);
                vertices.Add(Points.fh);
                vertices.Add(Points.fb);
                vertices.Add(Points.eg);
                vertices.Add(Points.ba);
                vertices.Add(Points.ac);
                vertices.Add(Points.fb);
                vertices.Add(Points.ba);
                vertices.Add(Points.eg);
                break;
            case 207:   // HG--DCBA
                vertices.Add(Points.bf);
                vertices.Add(Points.ge);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                break;
            case 208:   // HG-E----
                vertices.Add(Points.ea);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.hd);
                vertices.Add(Points.ef);
                vertices.Add(Points.fh);
                vertices.Add(Points.hd);
                break;
            case 209:   // HG-E---A
                vertices.Add(Points.ab);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.hd);
                vertices.Add(Points.dh);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                break;
            case 210:   // HG-E--B-
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.ea);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.hd);
                vertices.Add(Points.ef);
                vertices.Add(Points.fh);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.hd);
                vertices.Add(Points.hf);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                break;
            case 211:   // HG-E--BA
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.ac);
                vertices.Add(Points.dh);
                vertices.Add(Points.cg);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.dh);
                vertices.Add(Points.fb);
                vertices.Add(Points.dh);
                vertices.Add(Points.db);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.dh);
                break;
            case 212:   // HG-E-C--
                vertices.Add(Points.ea);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.hd);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.cd);
                vertices.Add(Points.dh);
                vertices.Add(Points.ef);
                vertices.Add(Points.fh);
                break;
            case 213:   // HG-E-C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.hd);
                vertices.Add(Points.dc);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.dh);
                vertices.Add(Points.dh);
                vertices.Add(Points.ef);
                vertices.Add(Points.fh);
                break;
            case 214:   // HG-E-CB-
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.ea);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ae);
                vertices.Add(Points.ef);
                vertices.Add(Points.fh);
                vertices.Add(Points.dh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                break;
            case 215:   // HG-E-CBA
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.dc);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.fb);
                vertices.Add(Points.dh);
                vertices.Add(Points.db);
                vertices.Add(Points.fb);
                vertices.Add(Points.fh);
                vertices.Add(Points.dh);
                break;
            case 216:   // HG-ED---
                vertices.Add(Points.ea);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.ae);
                vertices.Add(Points.ef);
                vertices.Add(Points.cd);
                vertices.Add(Points.dc);
                vertices.Add(Points.ef);
                vertices.Add(Points.fh);
                vertices.Add(Points.cd);
                vertices.Add(Points.hf);
                vertices.Add(Points.bd);
                break;
            case 217:   // HG-ED--A
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.ba);
                vertices.Add(Points.fh);
                vertices.Add(Points.bd);
                vertices.Add(Points.ba);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.ba);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.cd);
                break;
            case 218:   // HG-ED-B-
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.ae);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.cd);
                vertices.Add(Points.fe);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.ab);
                break;
            case 219:   // HG-ED-BA
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                break;
            case 220:   // HG-EDC--
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.db);
                vertices.Add(Points.db);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.bd);
                break;
            case 221:   // HG-EDC-A
                vertices.Add(Points.ab);
                vertices.Add(Points.hf);
                vertices.Add(Points.db);
                vertices.Add(Points.ab);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                break;
            case 222:   // HG-EDCB-
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.fh);
                vertices.Add(Points.fe);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.ab);
                break;
            case 223:   // HG-EDCBA
                vertices.Add(Points.bf);
                vertices.Add(Points.ef);
                vertices.Add(Points.hf);
                break;
            case 224:   // HGF-----
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                break;
            case 225:   // HGF----A
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.ab);
                vertices.Add(Points.ae);
                vertices.Add(Points.ac);
                vertices.Add(Points.gc);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.ae);
                vertices.Add(Points.fb);
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                break;
            case 226:   // HGF---B-
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.ab);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                break;
            case 227:   // HGF---BA
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.ac);
                vertices.Add(Points.dh);
                vertices.Add(Points.cg);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.dh);
                vertices.Add(Points.eg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ca);
                break;
            case 228:   // HGF--C--
                vertices.Add(Points.fe);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.ac);
                vertices.Add(Points.eg);
                vertices.Add(Points.cd);
                vertices.Add(Points.ge);
                vertices.Add(Points.ef);
                break;
            case 229:   // HGF--C-A
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.ab);
                vertices.Add(Points.dh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.bf);
                vertices.Add(Points.dh);
                vertices.Add(Points.bf);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.ea);
                break;
            case 230:   // HGF--CB-
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.fe);
                vertices.Add(Points.ac);
                vertices.Add(Points.eg);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.ac);
                vertices.Add(Points.ac);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.db);
                break;
            case 231:   // HGF--CBA
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                break;
            case 232:   // HGF-D---
                vertices.Add(Points.fe);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.fe);
                vertices.Add(Points.fb);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.cd);
                vertices.Add(Points.fb);
                vertices.Add(Points.bd);
                break;
            case 233:   // HGF-D--A
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.fe);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ca);
                vertices.Add(Points.ba);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.cd);
                break;
            case 234:   // HGF-D-B-
                vertices.Add(Points.ef);
                vertices.Add(Points.ba);
                vertices.Add(Points.gc);
                vertices.Add(Points.gc);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.gc);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                break;
            case 235:   // HGF-D-BA
                vertices.Add(Points.ef);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ca);
                vertices.Add(Points.cg);
                vertices.Add(Points.eg);
                vertices.Add(Points.ea);
                vertices.Add(Points.ca);
                break;
            case 236:   // HGF-DC--
                vertices.Add(Points.ef);
                vertices.Add(Points.fb);
                vertices.Add(Points.ac);
                vertices.Add(Points.ac);
                vertices.Add(Points.eg);
                vertices.Add(Points.ef);
                vertices.Add(Points.ac);
                vertices.Add(Points.fb);
                vertices.Add(Points.bd);
                break;
            case 237:   // HGF-DC-A
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.fe);
                vertices.Add(Points.ea);
                vertices.Add(Points.eg);
                vertices.Add(Points.bf);
                vertices.Add(Points.ea);
                vertices.Add(Points.ef);
                vertices.Add(Points.bf);
                vertices.Add(Points.ba);
                vertices.Add(Points.ea);
                break;
            case 238:   // HGF-DCB-
                vertices.Add(Points.fe);
                vertices.Add(Points.ca);
                vertices.Add(Points.ge);
                vertices.Add(Points.fe);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                break;
            case 239:   // HGF-DCBA
                vertices.Add(Points.fe);
                vertices.Add(Points.ae);
                vertices.Add(Points.ge);
                break;
            case 240:   // HGFE----
                vertices.Add(Points.ea);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                break;
            case 241:   // HGFE---A
                vertices.Add(Points.ab);
                vertices.Add(Points.bf);
                vertices.Add(Points.cg);
                vertices.Add(Points.cg);
                vertices.Add(Points.bf);
                vertices.Add(Points.dh);
                vertices.Add(Points.cg);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                break;
            case 242:   // HGFE--B-
                vertices.Add(Points.ea);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                vertices.Add(Points.hd);
                vertices.Add(Points.ab);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                break;
            case 243:   // HGFE--BA
                vertices.Add(Points.ac);
                vertices.Add(Points.hd);
                vertices.Add(Points.gc);
                vertices.Add(Points.ac);
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                break;
            case 244:   // HGFE-C--
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                vertices.Add(Points.ac);
                vertices.Add(Points.ae);
                vertices.Add(Points.dh);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.dh);
                break;
            case 245:   // HGFE-C-A
                vertices.Add(Points.ab);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.hd);
                break;
            case 246:   // HGFE-CB-
                vertices.Add(Points.ae);
                vertices.Add(Points.ab);
                vertices.Add(Points.ac);
                vertices.Add(Points.db);
                vertices.Add(Points.dh);
                vertices.Add(Points.dc);
                vertices.Add(Points.ac);
                vertices.Add(Points.db);
                vertices.Add(Points.dc);
                vertices.Add(Points.ac);
                vertices.Add(Points.ab);
                vertices.Add(Points.db);
                break;
            case 247:   // HGFE-CBA
                vertices.Add(Points.bd);
                vertices.Add(Points.hd);
                vertices.Add(Points.cd);
                break;
            case 248:   // HGFED---
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.gc);
                vertices.Add(Points.gc);
                vertices.Add(Points.fb);
                vertices.Add(Points.bd);
                vertices.Add(Points.gc);
                vertices.Add(Points.bd);
                vertices.Add(Points.dc);
                break;
            case 249:   // HGFED--A
                vertices.Add(Points.ba);
                vertices.Add(Points.bf);
                vertices.Add(Points.bd);
                vertices.Add(Points.ca);
                vertices.Add(Points.cd);
                vertices.Add(Points.cg);
                vertices.Add(Points.ba);
                vertices.Add(Points.cd);
                vertices.Add(Points.ca);
                vertices.Add(Points.ba);
                vertices.Add(Points.bd);
                vertices.Add(Points.cd);
                break;
            case 250:   // HGFED-B-
                vertices.Add(Points.ea);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                vertices.Add(Points.dc);
                break;
            case 251:   // HGFED-BA
                vertices.Add(Points.ac);
                vertices.Add(Points.dc);
                vertices.Add(Points.gc);
                break;
            case 252:   // HGFEDC--
                vertices.Add(Points.ea);
                vertices.Add(Points.db);
                vertices.Add(Points.ca);
                vertices.Add(Points.ea);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                break;
            case 253:   // HGFEDC-A
                vertices.Add(Points.ab);
                vertices.Add(Points.fb);
                vertices.Add(Points.db);
                break;
            case 254:   // HGFEDCB-
                vertices.Add(Points.ea);
                vertices.Add(Points.ba);
                vertices.Add(Points.ca);
                break;
            case 255:   // HGFEDCBA
                break;
            default:
                Debug.LogError(string.Format("unhandled config encountered [config]=" + config));
                break;
        }
        int addedCount = vertices.Count - beforeCount;

        //triangles, uvs (verticies added count enables to add proper triangle, uvs array items)
        switch (addedCount)
        {
            case 0:
                break;
            case 3:     // 1 triangle
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                break;
            case 6:     // 2 triangle
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                break;
            case 9:     // 3 triangle
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                break;
            case 12:    // 4 triangle
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                break;
            case 15:    // 5 triangles
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                break;
            case 18:    // 6 triangles
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                break;
            case 21: // 7 triangles
                triangles.Add(vertices.Count - 21);
                triangles.Add(vertices.Count - 20);
                triangles.Add(vertices.Count - 19);
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                break;
            case 24: // 8 triangles
                triangles.Add(vertices.Count - 24);
                triangles.Add(vertices.Count - 23);
                triangles.Add(vertices.Count - 22);
                triangles.Add(vertices.Count - 21);
                triangles.Add(vertices.Count - 20);
                triangles.Add(vertices.Count - 19);
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                break;
            case 27:    //9 triangles
                triangles.Add(vertices.Count - 27);
                triangles.Add(vertices.Count - 26);
                triangles.Add(vertices.Count - 25);
                triangles.Add(vertices.Count - 24);
                triangles.Add(vertices.Count - 23);
                triangles.Add(vertices.Count - 22);
                triangles.Add(vertices.Count - 21);
                triangles.Add(vertices.Count - 20);
                triangles.Add(vertices.Count - 19);
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                break;
            case 30:    //10 triangles
                triangles.Add(vertices.Count - 30);
                triangles.Add(vertices.Count - 29);
                triangles.Add(vertices.Count - 28);
                triangles.Add(vertices.Count - 27);
                triangles.Add(vertices.Count - 26);
                triangles.Add(vertices.Count - 25);
                triangles.Add(vertices.Count - 24);
                triangles.Add(vertices.Count - 23);
                triangles.Add(vertices.Count - 22);
                triangles.Add(vertices.Count - 21);
                triangles.Add(vertices.Count - 20);
                triangles.Add(vertices.Count - 19);
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                break;
            case 33:    //11 triangles
                triangles.Add(vertices.Count - 33);
                triangles.Add(vertices.Count - 32);
                triangles.Add(vertices.Count - 31);
                triangles.Add(vertices.Count - 30);
                triangles.Add(vertices.Count - 29);
                triangles.Add(vertices.Count - 28);
                triangles.Add(vertices.Count - 27);
                triangles.Add(vertices.Count - 26);
                triangles.Add(vertices.Count - 25);
                triangles.Add(vertices.Count - 24);
                triangles.Add(vertices.Count - 23);
                triangles.Add(vertices.Count - 22);
                triangles.Add(vertices.Count - 21);
                triangles.Add(vertices.Count - 20);
                triangles.Add(vertices.Count - 19);
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                break;
            case 36:    //12 triangles
                triangles.Add(vertices.Count - 36);
                triangles.Add(vertices.Count - 35);
                triangles.Add(vertices.Count - 34);
                triangles.Add(vertices.Count - 33);
                triangles.Add(vertices.Count - 32);
                triangles.Add(vertices.Count - 31);
                triangles.Add(vertices.Count - 30);
                triangles.Add(vertices.Count - 29);
                triangles.Add(vertices.Count - 28);
                triangles.Add(vertices.Count - 27);
                triangles.Add(vertices.Count - 26);
                triangles.Add(vertices.Count - 25);
                triangles.Add(vertices.Count - 24);
                triangles.Add(vertices.Count - 23);
                triangles.Add(vertices.Count - 22);
                triangles.Add(vertices.Count - 21);
                triangles.Add(vertices.Count - 20);
                triangles.Add(vertices.Count - 19);
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                break;
            case 39:    //13 triangles
                triangles.Add(vertices.Count - 39);
                triangles.Add(vertices.Count - 38);
                triangles.Add(vertices.Count - 37);
                triangles.Add(vertices.Count - 36);
                triangles.Add(vertices.Count - 35);
                triangles.Add(vertices.Count - 34);
                triangles.Add(vertices.Count - 33);
                triangles.Add(vertices.Count - 32);
                triangles.Add(vertices.Count - 31);
                triangles.Add(vertices.Count - 30);
                triangles.Add(vertices.Count - 29);
                triangles.Add(vertices.Count - 28);
                triangles.Add(vertices.Count - 27);
                triangles.Add(vertices.Count - 26);
                triangles.Add(vertices.Count - 25);
                triangles.Add(vertices.Count - 24);
                triangles.Add(vertices.Count - 23);
                triangles.Add(vertices.Count - 22);
                triangles.Add(vertices.Count - 21);
                triangles.Add(vertices.Count - 20);
                triangles.Add(vertices.Count - 19);
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                break;
            case 42:    //14 triangles
                triangles.Add(vertices.Count - 42);
                triangles.Add(vertices.Count - 41);
                triangles.Add(vertices.Count - 40);
                triangles.Add(vertices.Count - 39);
                triangles.Add(vertices.Count - 38);
                triangles.Add(vertices.Count - 37);
                triangles.Add(vertices.Count - 36);
                triangles.Add(vertices.Count - 35);
                triangles.Add(vertices.Count - 34);
                triangles.Add(vertices.Count - 33);
                triangles.Add(vertices.Count - 32);
                triangles.Add(vertices.Count - 31);
                triangles.Add(vertices.Count - 30);
                triangles.Add(vertices.Count - 29);
                triangles.Add(vertices.Count - 28);
                triangles.Add(vertices.Count - 27);
                triangles.Add(vertices.Count - 26);
                triangles.Add(vertices.Count - 25);
                triangles.Add(vertices.Count - 24);
                triangles.Add(vertices.Count - 23);
                triangles.Add(vertices.Count - 22);
                triangles.Add(vertices.Count - 21);
                triangles.Add(vertices.Count - 20);
                triangles.Add(vertices.Count - 19);
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                break;
            case 45:    //15 triangles
                triangles.Add(vertices.Count - 45);
                triangles.Add(vertices.Count - 44);
                triangles.Add(vertices.Count - 43);
                triangles.Add(vertices.Count - 42);
                triangles.Add(vertices.Count - 41);
                triangles.Add(vertices.Count - 40);
                triangles.Add(vertices.Count - 39);
                triangles.Add(vertices.Count - 38);
                triangles.Add(vertices.Count - 37);
                triangles.Add(vertices.Count - 36);
                triangles.Add(vertices.Count - 35);
                triangles.Add(vertices.Count - 34);
                triangles.Add(vertices.Count - 33);
                triangles.Add(vertices.Count - 32);
                triangles.Add(vertices.Count - 31);
                triangles.Add(vertices.Count - 30);
                triangles.Add(vertices.Count - 29);
                triangles.Add(vertices.Count - 28);
                triangles.Add(vertices.Count - 27);
                triangles.Add(vertices.Count - 26);
                triangles.Add(vertices.Count - 25);
                triangles.Add(vertices.Count - 24);
                triangles.Add(vertices.Count - 23);
                triangles.Add(vertices.Count - 22);
                triangles.Add(vertices.Count - 21);
                triangles.Add(vertices.Count - 20);
                triangles.Add(vertices.Count - 19);
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                break;
            case 48:    //16 triangles
                triangles.Add(vertices.Count - 48);
                triangles.Add(vertices.Count - 47);
                triangles.Add(vertices.Count - 46);
                triangles.Add(vertices.Count - 45);
                triangles.Add(vertices.Count - 44);
                triangles.Add(vertices.Count - 43);
                triangles.Add(vertices.Count - 42);
                triangles.Add(vertices.Count - 41);
                triangles.Add(vertices.Count - 40);
                triangles.Add(vertices.Count - 39);
                triangles.Add(vertices.Count - 38);
                triangles.Add(vertices.Count - 37);
                triangles.Add(vertices.Count - 36);
                triangles.Add(vertices.Count - 35);
                triangles.Add(vertices.Count - 34);
                triangles.Add(vertices.Count - 33);
                triangles.Add(vertices.Count - 32);
                triangles.Add(vertices.Count - 31);
                triangles.Add(vertices.Count - 30);
                triangles.Add(vertices.Count - 29);
                triangles.Add(vertices.Count - 28);
                triangles.Add(vertices.Count - 27);
                triangles.Add(vertices.Count - 26);
                triangles.Add(vertices.Count - 25);
                triangles.Add(vertices.Count - 24);
                triangles.Add(vertices.Count - 23);
                triangles.Add(vertices.Count - 22);
                triangles.Add(vertices.Count - 21);
                triangles.Add(vertices.Count - 20);
                triangles.Add(vertices.Count - 19);
                triangles.Add(vertices.Count - 18);
                triangles.Add(vertices.Count - 17);
                triangles.Add(vertices.Count - 16);
                triangles.Add(vertices.Count - 15);
                triangles.Add(vertices.Count - 14);
                triangles.Add(vertices.Count - 13);
                triangles.Add(vertices.Count - 12);
                triangles.Add(vertices.Count - 11);
                triangles.Add(vertices.Count - 10);
                triangles.Add(vertices.Count - 9);
                triangles.Add(vertices.Count - 8);
                triangles.Add(vertices.Count - 7);
                triangles.Add(vertices.Count - 6);
                triangles.Add(vertices.Count - 5);
                triangles.Add(vertices.Count - 4);
                triangles.Add(vertices.Count - 3);
                triangles.Add(vertices.Count - 2);
                triangles.Add(vertices.Count - 1);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.D);
                uv.Add(UVCoord.C);
                uv.Add(UVCoord.A);
                uv.Add(UVCoord.B);
                uv.Add(UVCoord.D);
                break;
            default:
                Debug.LogError("unhandled addedCount encountered [addedCount]= " + addedCount);
                break;
        }

        return addedCount;
    }
    public static void SetCubeConfig(int config)
    {
        //cube corners on off based on config code
        Points.A.On = Bits.isBitSet(config, "A");
        Points.B.On = Bits.isBitSet(config, "B");
        Points.C.On = Bits.isBitSet(config, "C");
        Points.D.On = Bits.isBitSet(config, "D");
        Points.E.On = Bits.isBitSet(config, "E");
        Points.F.On = Bits.isBitSet(config, "F");
        Points.G.On = Bits.isBitSet(config, "G");
        Points.H.On = Bits.isBitSet(config, "H");
    }
    public static Mesh GetMesh(ref GameObject go, ref Material material)
    {
        Mesh mesh = null;

        MeshFilter mf = go.GetComponent<MeshFilter>(); //add meshfilter component
        if (mf == null)
        {
            mf = go.AddComponent<MeshFilter>();
        }

        MeshRenderer mr = go.GetComponent<MeshRenderer>(); //add meshrenderer component
        if (mr == null)
        {
            mr = go.AddComponent<MeshRenderer>();
        }

        mr.material = material;

        //allocate mesh 
        if (Application.isEditor == true)
        {
            mesh = mf.sharedMesh;
            if (mesh == null)
            {
                mf.sharedMesh = new Mesh();
                mesh = mf.sharedMesh;
            }
        }
        else
        {
            mesh = mf.mesh;
            if (mesh == null)
            {
                mf.mesh = new Mesh();
                mesh = mf.mesh;
            }
        }
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; //increase max vertices per mesh
        mesh.name = "ProceduralMesh";

        return mesh;
    }
    public static void SetMesh(ref Mesh mesh)
    {
        //our mesh data to meshfilter
        mesh.Clear();

        mesh.vertices = MarchingCube.vertices.ToArray();
        mesh.triangles = MarchingCube.triangles.ToArray();
        mesh.uv = MarchingCube.uv.ToArray();

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
    public static float PerlinNoise3D(float x, float y, float z)
    {
        // use unity's PerlinNoise 2D
        // youtube video time 0:25sec = https://www.youtube.com/watch?v=TZFv493D7jo&t=25s  
        // youtube video time 0:20sec = https://www.youtube.com/watch?v=Aga0TBJkchM

        float AB = Mathf.PerlinNoise(x, y);         // get all three(3) permutations of noise for x,y and z
        float BC = Mathf.PerlinNoise(y, z);
        float AC = Mathf.PerlinNoise(x, z);

        float BA = Mathf.PerlinNoise(y, x);         // and their reverses
        float CB = Mathf.PerlinNoise(z, y);
        float CA = Mathf.PerlinNoise(z, x);

        float ABC = AB + BC + AC + BA + CB + CA;    // and return the average
        return ABC / 6f;
    }
}
