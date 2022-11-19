using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public interface IHead
    {
        void ApplyPosition(Vector3 position);
        void ApplyRotation(Vector3 rotation);
    }
}