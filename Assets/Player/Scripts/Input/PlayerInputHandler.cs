using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public bool BombPlacedInput { get; private set; }

    public void OnMoveInput(CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
    }

    public void OnBombPlacedInput(CallbackContext context)
    {
        if (context.started)
        {
            BombPlacedInput = true;
        }
        if (context.canceled)
        {
            BombPlacedInput = false;
        }
    }
}