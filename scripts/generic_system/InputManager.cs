using Godot;

namespace InputManagerSystem;
public partial class InputManager : Node
{
    public float XAxisInput => Input.GetAxis("left", "right");

    public float ZAxisInput => Input.GetAxis("foward", "backward");
}
