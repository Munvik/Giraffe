using Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class Lobby : MonoBehaviour, ILobby
    {
        [Header("Clients data")]
        [SerializeField] private List<ClientData> clients;
        [SerializeField] private int clientsCount = 2;
        [SerializeField] private List<LabelClientHandler> labelHandlers;

        [Header("UI References")]
        [SerializeField] private List<ClientLabel> clientLabels;
        [SerializeField] private Button startButton;

        [Header("Other References")]
        [SerializeField] private NetworkSender networkSender;

        private void Start()
        {
            startButton.interactable = false;
            startButton.onClick.AddListener(StartGame);
        }

        public void Join(ClientData clientData)
        {
            ClientLabel label = clientLabels[clients.Count];
            clients.Add(clientData);
            labelHandlers.Add(new LabelClientHandler(clientData, label));
            FillClientLabel(clientData, label);

            Debug.Log("User connected to lobby, name is " + clientData.name);
            if(clients.Count >= clientsCount)
            {
                startButton.interactable = true;
            }
        }

        private void StartGame()
        {
            Debug.Log("Start the game");
            networkSender.StartGame();
        }

        private void FillClientLabel(ClientData clientData, ClientLabel label)
        {
            label.SetupLabel(clientData);
        }

        #region DEVELOPMENT REGION
        public ClientData testClient;

        [ContextMenu("Add test client")]
        public void AddTestClient()
        {
            Join(testClient);
        }
        #endregion
    }

    [System.Serializable]
    public class ClientData
    {
        public string name;
    }

    [System.Serializable]
    public class LabelClientHandler
    {
        public ClientLabel label;
        public ClientData clientData;

        public LabelClientHandler(ClientData clientData, ClientLabel label)
        {
            this.clientData = clientData;
            this.label = label;
        }
    }
}
