using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Eclipse
{
    public enum Character
    {
        LightPlayer,
        DarkPlayer
    }
    public static class PlayerRegister
    {
        public class PlayerInfo
        {
            public Gamepad AssignedGamepad { get; set; }
            public Character ChosenCharacter { get; set; }
            public int GamepadIdx { get; set; }
        }

        private static List<PlayerInfo> m_players = new();

        public static void RegisterPlayer(Gamepad gamepad, Character chosenCharacter)
        {

            m_players.Add(new PlayerInfo
            {
                AssignedGamepad = gamepad,
                ChosenCharacter = chosenCharacter
            });
        }

        public static PlayerInfo Get(Character character)
        {
            return m_players.First(player => player.ChosenCharacter == character);
        }

        public static List<PlayerInfo> GetAll()
        {
            return m_players;
        }
    }
}

