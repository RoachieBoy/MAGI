using System;
using JetBrains.Annotations;
using UnityEngine.InputSystem;

namespace General
{
    public static class InputActionMapsHelper
    {
        public static void CreateInputActionMapWithActionMethod([NotNull] InputActionMap map, Action action, string key)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            
            // add the given action to the action map
            var type = map.AddAction(map.ToString(), InputActionType.Button);
            
            // bind the action to the given key
            type.AddBinding("<Keyboard>/" + key, "Hold");
            
            map.Enable();
            
            // when the action is triggered, activate the given action
            type.started += _ => { action(); };
        }

        public static void CreateInputActionMapStandard(InputActionMap map, string key)
        {
            var type = map.AddAction(key, InputActionType.Button);
            
            type.AddBinding("<Keyboard>/" + key, "Hold");
        }
    }
}