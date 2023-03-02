//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/GameSections/Bard Abilities/BardControlActions.inputactions
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

public partial class @BardControlActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @BardControlActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BardControlActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""654492b0-a2e8-4618-ac95-825974917faf"",
            ""actions"": [
                {
                    ""name"": ""Button1"",
                    ""type"": ""Button"",
                    ""id"": ""2f46026d-3dbe-41ba-ae06-2364a511967f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Button2"",
                    ""type"": ""Button"",
                    ""id"": ""d6f7049b-1a60-476c-80e2-fd030b494161"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Button3"",
                    ""type"": ""Button"",
                    ""id"": ""aaae0408-94c8-4285-a11e-79e62b9dd684"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Button4"",
                    ""type"": ""Button"",
                    ""id"": ""c0cba9d1-8217-48c2-9535-48634740f7ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""04397046-2a02-41c7-8348-af6259f5b649"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""341769bf-1252-43da-9b62-b97a08fe15d0"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c4e207a-1bf6-4bff-9b30-a00e081aaea2"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00425f6c-0d5e-4e19-b705-746a4b7ab5a0"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa122ef5-494e-4bb4-bc4c-858b7c1cc8ab"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9bcc0e2-6fc8-473c-87f9-566b2e4ec8e6"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Button3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef70071e-fe06-4b31-aac2-870fc93198c8"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8c26c40-b265-451c-b849-5b8995c53696"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Button4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c00977bf-7a05-46fa-bd11-10d30f4ec490"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Button1 = m_Player.FindAction("Button1", throwIfNotFound: true);
        m_Player_Button2 = m_Player.FindAction("Button2", throwIfNotFound: true);
        m_Player_Button3 = m_Player.FindAction("Button3", throwIfNotFound: true);
        m_Player_Button4 = m_Player.FindAction("Button4", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Button1;
    private readonly InputAction m_Player_Button2;
    private readonly InputAction m_Player_Button3;
    private readonly InputAction m_Player_Button4;
    public struct PlayerActions
    {
        private @BardControlActions m_Wrapper;
        public PlayerActions(@BardControlActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Button1 => m_Wrapper.m_Player_Button1;
        public InputAction @Button2 => m_Wrapper.m_Player_Button2;
        public InputAction @Button3 => m_Wrapper.m_Player_Button3;
        public InputAction @Button4 => m_Wrapper.m_Player_Button4;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Button1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton1;
                @Button1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton1;
                @Button1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton1;
                @Button2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton2;
                @Button2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton2;
                @Button2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton2;
                @Button3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton3;
                @Button3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton3;
                @Button3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton3;
                @Button4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton4;
                @Button4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton4;
                @Button4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButton4;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Button1.started += instance.OnButton1;
                @Button1.performed += instance.OnButton1;
                @Button1.canceled += instance.OnButton1;
                @Button2.started += instance.OnButton2;
                @Button2.performed += instance.OnButton2;
                @Button2.canceled += instance.OnButton2;
                @Button3.started += instance.OnButton3;
                @Button3.performed += instance.OnButton3;
                @Button3.canceled += instance.OnButton3;
                @Button4.started += instance.OnButton4;
                @Button4.performed += instance.OnButton4;
                @Button4.canceled += instance.OnButton4;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnButton1(InputAction.CallbackContext context);
        void OnButton2(InputAction.CallbackContext context);
        void OnButton3(InputAction.CallbackContext context);
        void OnButton4(InputAction.CallbackContext context);
    }
}
