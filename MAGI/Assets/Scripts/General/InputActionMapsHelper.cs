using System;
using JetBrains.Annotations;
using UnityEngine.InputSystem;

namespace General
{
    public static class InputActionMapsHelper
    {
        /// <summary>
        ///  Creates an input action map with the given key and action method.
        /// </summary>
        /// <param name="map"> the map to fill with the action data </param>
        /// <param name="action"> what happens when the key is pressed? </param>
        /// <param name="key"> the name of the key </param>
        public static void CreateInputActionMapWithActionMethod([NotNull] InputActionMap map, Action action, string key)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));
            
            // add the given action to the action map
            var type = map.AddAction(map.ToString(), InputActionType.Button);
            
            // bind the action to the given key
            type.AddBinding("<Keyboard>/" + key, "Hold");
            
            // enable the action map
            map.Enable();
            
            // use the callback and invoke the action when the key is pressed
            type.started += _ => { action(); };
        }
        
        /// <summary>
        ///  Creates an input action map with the given key and returns the action type.
        /// </summary>
        /// <param name="map"> the map to fill with the action data </param>
        /// <param name="key"> the key name </param>
        public static InputAction CreateInputActionMapStandard(InputActionMap map, string key)
        {
            var type = map.AddAction(key, InputActionType.Button);
            
            type.AddBinding("<Keyboard>/" + key, "Hold");
            
            return type;
        }
    }
}