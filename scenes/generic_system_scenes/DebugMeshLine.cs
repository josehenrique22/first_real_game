using Godot;

public partial class DebugMeshLine : MeshInstance3D
{

    [Export] CharacterBody3D _meshPosition;

    private enum MeshLineMagniture : int
    {
        LineLenght = 5
    }

    public override void _Process(double delta)
    {
        GenerateLine();
    }

    private void GenerateLine()
    {
        DrawMeshLine(Vector3.Zero,
        Vector3.Forward *
        (int)MeshLineMagniture.LineLenght,
        Colors.Red);
        SetMeshLinePosition();
    }

    private void SetMeshLinePosition()
    {
        Position = _meshPosition.Position;
        if (!MeshConnectToEntity())
        {
            GlobalPosition = _meshPosition.GlobalPosition;
        }
    }

    private void DrawMeshLine(Vector3 _pointA, Vector3 _pointB, Color _meshColor)
    {
        if (_pointA.IsEqualApprox(_pointB)) { return; }

        var newMeshLine = Mesh as ImmediateMesh;

        newMeshLine.ClearSurfaces();

        newMeshLine.SurfaceBegin(Mesh.PrimitiveType.Lines);

        newMeshLine.SurfaceAddVertex(_pointA);
        newMeshLine.SurfaceAddVertex(_pointB);

        newMeshLine.SurfaceSetColor(_meshColor);

        newMeshLine.SurfaceEnd();
    }

    private bool MeshConnectToEntity()
    {
        return Position == _meshPosition.Position;
    }
}
