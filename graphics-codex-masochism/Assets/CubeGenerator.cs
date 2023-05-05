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

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);
        CreateSquare();
        GL.PopMatrix();
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
    public void Update()
    {
        // will make the mesh appear in the Scene at origin position
        Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, material, 0);
    }
}
