using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobby
{
    public interface ILobby
    {
        void Join(ClientData clientData);
    }
}
