using Godot;

namespace DTOS;

public partial class MovementDTO : Node
{
    [Export] public float Speed { get; set; }
    [Export] public float Acceleration { get; set; }
    [Export] public float Deceleration { get; set; }
}
