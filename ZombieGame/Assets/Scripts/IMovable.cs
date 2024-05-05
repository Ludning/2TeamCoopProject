using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public interface IMovable
{
    public void OnMove(InputAction.CallbackContext context);

    public void OnRun(InputAction.CallbackContext context);

    public void OnLook(InputAction.CallbackContext context);

    public void OnJump(InputAction.CallbackContext context);
}
