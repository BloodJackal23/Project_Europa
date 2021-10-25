// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player Controls/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerActions"",
            ""id"": ""4db97b62-4f27-4b94-a6c5-54107b675108"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""111a17ae-a686-4834-bf00-165b65bf0ed9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""36261033-0447-46a7-98f4-129dd1599ae5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Detach"",
                    ""type"": ""Button"",
                    ""id"": ""23b8b320-820c-4779-9032-5c2dedfc7042"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""3389aef8-b6f7-4fd1-9b8e-9b0126da5525"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""4be0320a-4628-476f-958f-1408ba599ab9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f5fa59ff-7729-45c8-b5ea-275542ea88a5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3a818824-fb8e-42b2-9477-97aa55d93745"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0171ea10-2116-4369-8539-2519f624af09"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2c6947d3-ceaa-4eac-b3c2-a6254ef22d54"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d6171150-5f83-489a-8a0e-51433d55292b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d7569e3-5a00-4c3c-9338-e1ef019433e2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Detach"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""477e4c99-c661-47cc-bde5-20cc40877393"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MenuAcions"",
            ""id"": ""c9a5a217-787a-4057-add7-58d9d52fcc52"",
            ""actions"": [
                {
                    ""name"": ""Replay"",
                    ""type"": ""Button"",
                    ""id"": ""818c25d7-bb8b-49e1-b6d8-4afeb76152ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""010487fa-8724-4a22-94c1-1aa4cd649800"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73879170-1a32-412e-9695-815ea8bd62a2"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Replay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f374119d-7900-43ea-8906-f2ba2848f518"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Replay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfb729fd-fe90-43ea-b914-b65f9aadf746"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_Movement = m_PlayerActions.FindAction("Movement", throwIfNotFound: true);
        m_PlayerActions_Fire = m_PlayerActions.FindAction("Fire", throwIfNotFound: true);
        m_PlayerActions_Detach = m_PlayerActions.FindAction("Detach", throwIfNotFound: true);
        m_PlayerActions_Pause = m_PlayerActions.FindAction("Pause", throwIfNotFound: true);
        // MenuAcions
        m_MenuAcions = asset.FindActionMap("MenuAcions", throwIfNotFound: true);
        m_MenuAcions_Replay = m_MenuAcions.FindAction("Replay", throwIfNotFound: true);
        m_MenuAcions_Exit = m_MenuAcions.FindAction("Exit", throwIfNotFound: true);
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

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Movement;
    private readonly InputAction m_PlayerActions_Fire;
    private readonly InputAction m_PlayerActions_Detach;
    private readonly InputAction m_PlayerActions_Pause;
    public struct PlayerActionsActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActionsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerActions_Movement;
        public InputAction @Fire => m_Wrapper.m_PlayerActions_Fire;
        public InputAction @Detach => m_Wrapper.m_PlayerActions_Detach;
        public InputAction @Pause => m_Wrapper.m_PlayerActions_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMovement;
                @Fire.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFire;
                @Detach.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDetach;
                @Detach.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDetach;
                @Detach.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDetach;
                @Pause.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Detach.started += instance.OnDetach;
                @Detach.performed += instance.OnDetach;
                @Detach.canceled += instance.OnDetach;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // MenuAcions
    private readonly InputActionMap m_MenuAcions;
    private IMenuAcionsActions m_MenuAcionsActionsCallbackInterface;
    private readonly InputAction m_MenuAcions_Replay;
    private readonly InputAction m_MenuAcions_Exit;
    public struct MenuAcionsActions
    {
        private @PlayerInput m_Wrapper;
        public MenuAcionsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Replay => m_Wrapper.m_MenuAcions_Replay;
        public InputAction @Exit => m_Wrapper.m_MenuAcions_Exit;
        public InputActionMap Get() { return m_Wrapper.m_MenuAcions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuAcionsActions set) { return set.Get(); }
        public void SetCallbacks(IMenuAcionsActions instance)
        {
            if (m_Wrapper.m_MenuAcionsActionsCallbackInterface != null)
            {
                @Replay.started -= m_Wrapper.m_MenuAcionsActionsCallbackInterface.OnReplay;
                @Replay.performed -= m_Wrapper.m_MenuAcionsActionsCallbackInterface.OnReplay;
                @Replay.canceled -= m_Wrapper.m_MenuAcionsActionsCallbackInterface.OnReplay;
                @Exit.started -= m_Wrapper.m_MenuAcionsActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_MenuAcionsActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_MenuAcionsActionsCallbackInterface.OnExit;
            }
            m_Wrapper.m_MenuAcionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Replay.started += instance.OnReplay;
                @Replay.performed += instance.OnReplay;
                @Replay.canceled += instance.OnReplay;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
            }
        }
    }
    public MenuAcionsActions @MenuAcions => new MenuAcionsActions(this);
    public interface IPlayerActionsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnDetach(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IMenuAcionsActions
    {
        void OnReplay(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
    }
}
