using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractionInterface
{
    void interact(bool active, Transform caller);
    void grabPress(Transform caller);
    void grabHeld(Transform caller);
    void grabRelease(Transform caller);
}
