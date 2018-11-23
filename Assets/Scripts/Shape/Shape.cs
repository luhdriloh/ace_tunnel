using System.Collections;
using System.Linq;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Material mats;

    // Use this for initialization
    void Start()
    {
        mats = new Material(Shader.Find("Sprites/Default"));
    }

    public void DrawShape(Vector2[] vertices2D)
    {
        Vector2[] transformed = vertices2D.Select(v => (Vector2)transform.InverseTransformPoint(v)).ToArray<Vector2>();
        var vertices3D = System.Array.ConvertAll<Vector2, Vector3>(transformed, v => v);

        // Use the triangulator to get indices for creating triangles
        var triangulator = new Triangulator(vertices2D);
        var indices = triangulator.Triangulate();

        // Generate a color for each vertex
        var colors = Enumerable.Range(0, vertices3D.Length)
            .Select(i => Color.black)
            .ToArray();

        // Create the mesh
        var mesh = new Mesh
        {
            vertices = vertices3D,
            triangles = indices,
            colors = colors
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, mats, 0);
    }
}
