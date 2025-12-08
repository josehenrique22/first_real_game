using Godot;

public partial class DebugMeshLine : MeshInstance3D
{

    [Export] CharacterBody3D _meshPosition;

    public override void _Process(double delta)
    {
        GenerateLine(_meshPosition.Position, Vector3.Forward, Colors.Red);
    }

    private void GenerateLine(Vector3 _pointA, Vector3 _pointB, Color _meshColor)
    {
        if (_pointA.IsEqualApprox(_pointB)) { return; }

        if (Mesh is ImmediateMesh newMeshLine)
        {
            // Clear the buffer before start drawing
            newMeshLine.ClearSurfaces();

            // Start drawing
            newMeshLine.SurfaceBegin(Mesh.PrimitiveType.Lines);

            // Set Color and Cordinates
            newMeshLine.SurfaceAddVertex(_pointA);

            newMeshLine.SurfaceAddVertex(_pointB);

            newMeshLine.SurfaceSetColor(_meshColor);

            // End Drawing
            newMeshLine.SurfaceEnd();
        }
    }

}
