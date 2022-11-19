using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LobbyCreator
{
    public class Client : MonoBehaviour
    {
        [SerializeField] private ClientData myClientData;

        public void JoinLobby(ILobby lobby)
        {
            lobby.Join(myClientData);
        }
    }
}
