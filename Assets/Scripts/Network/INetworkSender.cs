using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public interface INetworkSender
    {
        void StartGame();
        void Send(string methodName, string data);
    }
}
