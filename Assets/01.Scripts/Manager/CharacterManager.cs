using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Manager<CharacterManager>
{
    private PlayerSprit _player;
    public PlayerSprit Player 
    {
        get { return _player; }
        set { _player = value; }
    }

}
