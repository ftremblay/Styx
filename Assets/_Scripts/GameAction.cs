using System;

namespace Assets._Scripts
{
    public enum ActionType
    {
        PLAYER_GRAB_PUCK,
        PLAYER_PASS_PUCK
    }

    public class GameAction
    {
        public ActionType Name;
        public Action Callback;

        public static GameAction Create(ActionType name, Action callback = null)
        {
            return new GameAction(name, callback);
        }

        private GameAction(ActionType name, Action callback)
        {
            Name = name;
            Callback = callback;
        }
    }
}
