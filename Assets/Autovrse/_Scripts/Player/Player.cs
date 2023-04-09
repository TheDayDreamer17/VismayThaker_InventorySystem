using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class Player : MonoBehaviour
    {
        private PlayerItemPicker _playerItemPicker;

        private void Awake()
        {
            _playerItemPicker = GetComponent<PlayerItemPicker>();
        }
    }
}
