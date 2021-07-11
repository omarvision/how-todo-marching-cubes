using UnityEngine;

public class ProceduralMesh : MonoBehaviour
{
    public Vector3 GridSize = new Vector3(10, 5, 10);
    public Material material = null;
    public float zoom = 1f;
    public float noiselimit = 0.5f;
    private Mesh mesh = null;

    private void Start()
    {
        MakeGrid();
        //Noise2d();
        Noise3d();
        March();
    }
    private void MakeGrid()
    {
        //allocate
        MarchingCube.grd = new GridPoint[(int)GridSize.x, (int)GridSize.y, (int)GridSize.z];

        // define the points
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    MarchingCube.grd[x, y, z] = new GridPoint();
                    MarchingCube.grd[x, y, z].Position = new Vector3(x, y, z);
                    MarchingCube.grd[x, y, z].On = false;
                }
            }
        }
    }
    private void Noise2d()
    {
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                float nx = (x / GridSize.x) * zoom;
                float nz = (z / GridSize.z) * zoom;
                float height = Mathf.PerlinNoise(nx, nz) * GridSize.y;

                for (int y = 0; y < GridSize.y; y++)
                {
                    MarchingCube.grd[x, y, z].On = (y < height);
                }
            }
        }
    }
    private void Noise3d()
    {
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    float nx = (x / GridSize.x) * zoom;
                    float ny = (y / GridSize.y) * zoom;
                    float nz = (z / GridSize.z) * zoom;
                    float noise = MarchingCube.PerlinNoise3D(nx, ny, nz);  //0..1

                    MarchingCube.grd[x, y, z].On = (noise > noiselimit);
                }
            }
        }
    }
    private void March()
    {
        GameObject go = this.gameObject;
        mesh = MarchingCube.GetMesh(ref go, ref material);

        MarchingCube.Clear();
        MarchingCube.MarchCubes();

        MarchingCube.SetMesh(ref mesh);
    }
}
