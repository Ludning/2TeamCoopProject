using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public interface IMovable
{
    public void OnMove(InputValue input);

    public void OnRun(InputValue input);

    public void OnLook(InputValue input);

    public void OnJump(InputValue input);
}
