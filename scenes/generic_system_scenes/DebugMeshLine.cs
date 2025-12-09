using Godot;

public partial class DebugMeshLine : MeshInstance3D
{

    [Export] CharacterBody3D _meshPosition;

    public override void _Process(double delta)
    {
        GenerateLine(Vector3.Zero, Vector3.Forward * 3, Colors.Red);
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

            var lineDebugPosition = MeshSamePositionToPlayer() ?
                "Position is equal to player" : "Position is not equal to the player";

            Position = _meshPosition.Position;
            if (!MeshSamePositionToPlayer())
            {
                GlobalPosition = _meshPosition.GlobalPosition;
            }

            GD.Print(lineDebugPosition);

            // End Drawing
            newMeshLine.SurfaceEnd();
        }
    }

    private bool MeshSamePositionToPlayer()
    {
        return Position == _meshPosition.Position;
    }
}
