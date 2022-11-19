using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Movement
{
    public interface IBody
    {
        /// <summary>
        /// Add movement Value from -1, 1. -1 is left edge, 1 is rigt
        /// </summary>
        /// <param name="movement"></param>
        void Move(float movement);
        void Crouch(float crouchValue);
        void Jump(float force);
    }
}