//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/JoystickController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @JoystickController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @JoystickController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""JoystickController"",
    ""maps"": [
        {
            ""name"": ""JSController"",
            ""id"": ""f0ec733a-04db-43b4-a723-fe5b94670b36"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""bb1df6cc-1caa-47a6-bd85-749ee5698856"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ab95d073-d97c-4c37-83dd-71eb13e2e7b7"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // JSController
        m_JSController = asset.FindActionMap("JSController", throwIfNotFound: true);
        m_JSController_Move = m_JSController.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // JSController
    private readonly InputActionMap m_JSController;
    private IJSControllerActions m_JSControllerActionsCallbackInterface;
    private readonly InputAction m_JSController_Move;
    public struct JSControllerActions
    {
        private @JoystickController m_Wrapper;
        public JSControllerActions(@JoystickController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_JSController_Move;
        public InputActionMap Get() { return m_Wrapper.m_JSController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JSControllerActions set) { return set.Get(); }
        public void SetCallbacks(IJSControllerActions instance)
        {
            if (m_Wrapper.m_JSControllerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_JSControllerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_JSControllerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_JSControllerActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_JSControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public JSControllerActions @JSController => new JSControllerActions(this);
    public interface IJSControllerActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
