using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LobbyCreator
{
    public class ClientLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        public void SetupLabel(ClientData data)
        {
            label.text = data.name;
        }
    }
}
