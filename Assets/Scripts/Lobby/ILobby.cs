using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LobbyCreator
{
    public interface ILobby
    {
        void Join(ClientData clientData);
    }
}
