using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    // When added to an object, draws colored rays from the
    // transform position.
    public int lineCount = 100;
    public float radius = 3.0f;

    static Material lineMaterial;

    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    // Will be called after all regular rendering is done
    public void OnRenderObject()
    {
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        //GL.PushMatrix();
        //// Set transformation matrix for drawing to
        //// match our transform
        //GL.MultMatrix(transform.localToWorldMatrix);
        //CreateSquare();
        //GL.PopMatrix();
    }

    private static void CreateSquare()
    {
        GL.Begin(GL.TRIANGLES);

        GL.Color(new Color(0, 255F, 255F));

        // Front face:
        // First tri
        GL.Vertex3(-5, 5, 0);
        GL.Vertex3(-5, -5, 0);
        GL.Vertex3(5, 5, 0);
        // Second tri
        GL.Vertex3(5, 5, 0);
        GL.Vertex3(5, -5, 0);
        GL.Vertex3(-5, -5, 0);

        GL.End();
    }


    // Using Draw API
    public Mesh mesh;
    public Material material;

    private void CreateCubeMesh()
    {
        // Define the vertices of the cube mesh
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f,  0.5f), // Front bottom left
            new Vector3( 0.5f, -0.5f,  0.5f), // Front bottom right
            new Vector3( 0.5f,  0.5f,  0.5f), // Front top right
            new Vector3(-0.5f,  0.5f,  0.5f), // Front top left
            new Vector3(-0.5f, -0.5f, -0.5f), // Back bottom left
            new Vector3( 0.5f, -0.5f, -0.5f), // Back bottom right
            new Vector3( 0.5f,  0.5f, -0.5f), // Back top right
            new Vector3(-0.5f,  0.5f, -0.5f)  // Back top left
        };

        // Define the normals of the cube mesh
        Vector3[] normals = new Vector3[]
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back
        };

        // Define the triangles of the cube mesh
        int[] triangles = new int[]
        {
            // Front face
            0, 2, 1,
            0, 3, 2,

            // Back face
            4, 5, 6,
            4, 6, 7,

            // Top face
            3, 6, 2,
            3, 7, 6,

            // Bottom face
            0, 1, 5,
            0, 5, 4,

            // Left face
            0, 7, 3,
            0, 4, 7,

            // Right face
            1, 2, 6,
            1, 6, 5
        };

        // Create a new mesh and set its vertices, normals, and triangles
        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
        material = new Material(Shader.Find("Standard"));
    }

    private void DrawCubeMesh()
    {
        // Draw the mesh using Graphics.DrawMesh
        Quaternion rotation = Quaternion.Euler(0, 45, 0);
        Graphics.DrawMesh(mesh, Vector3.zero, rotation, material, 0);
    }

    private void Start()
    {
        CreateCubeMesh();
    }

    public void Update()
    {
        DrawCubeMesh();
    }
}
