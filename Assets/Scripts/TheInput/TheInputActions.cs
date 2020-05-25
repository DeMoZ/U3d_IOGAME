// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/TheInput/TheInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace TheInput
{
    public class @TheInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @TheInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""TheInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""1da8be8e-c18a-471c-accc-39189d3df429"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4fa7c856-f387-4824-a159-1bf1712aa41d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveTouch"",
                    ""type"": ""Button"",
                    ""id"": ""9d020d72-1e8f-4447-9c89-392b417c5ecc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveJoy"",
                    ""type"": ""Button"",
                    ""id"": ""cf1ea746-7d22-4d74-bdba-502680821400"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackUp"",
                    ""type"": ""Button"",
                    ""id"": ""e62f6975-cf46-4752-8668-13c0ebc97806"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackDn"",
                    ""type"": ""Button"",
                    ""id"": ""d9c78197-917d-43fd-a9fc-e3c7014cc5ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackLt"",
                    ""type"": ""Button"",
                    ""id"": ""dc0721b8-5ff0-4446-8eef-02a37f176346"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackRt"",
                    ""type"": ""Button"",
                    ""id"": ""f275de0c-462f-497c-966e-07eb86ee2da2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookMouse"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bcd7ce66-81ec-4c38-8fc3-0cc9d4315d17"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookTouch"",
                    ""type"": ""PassThrough"",
                    ""id"": ""56ea0226-a757-4ace-a267-feb6d3cc9bb5"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookJoy"",
                    ""type"": ""Button"",
                    ""id"": ""d98df8d3-fdac-4812-ac3c-9706cd31a382"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""d520d71d-a2ff-4944-9e05-537970298576"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a47d9bfb-bf6b-4969-9428-290a4e67facc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9e57065c-4316-4a10-8957-43f25a56cc89"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a83c268c-c443-44c0-8354-6f1badd0e650"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d62ce038-f4c0-4d3d-a0d7-1f9d5238aa0f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""f9a31168-3711-459a-85e4-8e6a5803a6d0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f9460a5f-9cc5-4b46-adca-8ef21e0645b4"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b9c82172-9432-497a-ac58-06040bbc0989"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""784820ee-13ad-4212-96b4-dd0a9b046668"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d5d538f6-6f87-4711-88dd-13e1de9267f2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""969d1614-a6f7-4538-aa1b-a0ecbd923202"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""AttackUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e70bbca4-c0a3-487c-8593-d383a1b38c5c"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""AttackDn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""adc2d74f-93cc-4aa0-9598-586ef4853b0a"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""AttackLt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b73712b-bfd3-4021-8711-e4a9a5a4fbf7"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""AttackRt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c12653c-b26e-48cc-8060-f1084e6061d5"",
                    ""path"": ""<Touchscreen>/touch2/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""MoveTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe13b17e-56d7-4024-a0b6-69bdef727639"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookTouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f662d83-e329-4e64-b06a-53dbc05d5fa9"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookJoy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c7b4975-5f76-4106-acf4-71ca4e888029"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveJoy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc008bbe-153e-488c-941c-4878fcc5323d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""LookMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
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
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // PlayerControls
            m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
            m_PlayerControls_Move = m_PlayerControls.FindAction("Move", throwIfNotFound: true);
            m_PlayerControls_MoveTouch = m_PlayerControls.FindAction("MoveTouch", throwIfNotFound: true);
            m_PlayerControls_MoveJoy = m_PlayerControls.FindAction("MoveJoy", throwIfNotFound: true);
            m_PlayerControls_AttackUp = m_PlayerControls.FindAction("AttackUp", throwIfNotFound: true);
            m_PlayerControls_AttackDn = m_PlayerControls.FindAction("AttackDn", throwIfNotFound: true);
            m_PlayerControls_AttackLt = m_PlayerControls.FindAction("AttackLt", throwIfNotFound: true);
            m_PlayerControls_AttackRt = m_PlayerControls.FindAction("AttackRt", throwIfNotFound: true);
            m_PlayerControls_LookMouse = m_PlayerControls.FindAction("LookMouse", throwIfNotFound: true);
            m_PlayerControls_LookTouch = m_PlayerControls.FindAction("LookTouch", throwIfNotFound: true);
            m_PlayerControls_LookJoy = m_PlayerControls.FindAction("LookJoy", throwIfNotFound: true);
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

        // PlayerControls
        private readonly InputActionMap m_PlayerControls;
        private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
        private readonly InputAction m_PlayerControls_Move;
        private readonly InputAction m_PlayerControls_MoveTouch;
        private readonly InputAction m_PlayerControls_MoveJoy;
        private readonly InputAction m_PlayerControls_AttackUp;
        private readonly InputAction m_PlayerControls_AttackDn;
        private readonly InputAction m_PlayerControls_AttackLt;
        private readonly InputAction m_PlayerControls_AttackRt;
        private readonly InputAction m_PlayerControls_LookMouse;
        private readonly InputAction m_PlayerControls_LookTouch;
        private readonly InputAction m_PlayerControls_LookJoy;
        public struct PlayerControlsActions
        {
            private @TheInputActions m_Wrapper;
            public PlayerControlsActions(@TheInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_PlayerControls_Move;
            public InputAction @MoveTouch => m_Wrapper.m_PlayerControls_MoveTouch;
            public InputAction @MoveJoy => m_Wrapper.m_PlayerControls_MoveJoy;
            public InputAction @AttackUp => m_Wrapper.m_PlayerControls_AttackUp;
            public InputAction @AttackDn => m_Wrapper.m_PlayerControls_AttackDn;
            public InputAction @AttackLt => m_Wrapper.m_PlayerControls_AttackLt;
            public InputAction @AttackRt => m_Wrapper.m_PlayerControls_AttackRt;
            public InputAction @LookMouse => m_Wrapper.m_PlayerControls_LookMouse;
            public InputAction @LookTouch => m_Wrapper.m_PlayerControls_LookTouch;
            public InputAction @LookJoy => m_Wrapper.m_PlayerControls_LookJoy;
            public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerControlsActions instance)
            {
                if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                    @MoveTouch.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveTouch;
                    @MoveTouch.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveTouch;
                    @MoveTouch.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveTouch;
                    @MoveJoy.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveJoy;
                    @MoveJoy.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveJoy;
                    @MoveJoy.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveJoy;
                    @AttackUp.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackUp;
                    @AttackUp.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackUp;
                    @AttackUp.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackUp;
                    @AttackDn.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackDn;
                    @AttackDn.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackDn;
                    @AttackDn.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackDn;
                    @AttackLt.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackLt;
                    @AttackLt.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackLt;
                    @AttackLt.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackLt;
                    @AttackRt.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackRt;
                    @AttackRt.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackRt;
                    @AttackRt.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAttackRt;
                    @LookMouse.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookMouse;
                    @LookMouse.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookMouse;
                    @LookMouse.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookMouse;
                    @LookTouch.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookTouch;
                    @LookTouch.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookTouch;
                    @LookTouch.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookTouch;
                    @LookJoy.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookJoy;
                    @LookJoy.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookJoy;
                    @LookJoy.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookJoy;
                }
                m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @MoveTouch.started += instance.OnMoveTouch;
                    @MoveTouch.performed += instance.OnMoveTouch;
                    @MoveTouch.canceled += instance.OnMoveTouch;
                    @MoveJoy.started += instance.OnMoveJoy;
                    @MoveJoy.performed += instance.OnMoveJoy;
                    @MoveJoy.canceled += instance.OnMoveJoy;
                    @AttackUp.started += instance.OnAttackUp;
                    @AttackUp.performed += instance.OnAttackUp;
                    @AttackUp.canceled += instance.OnAttackUp;
                    @AttackDn.started += instance.OnAttackDn;
                    @AttackDn.performed += instance.OnAttackDn;
                    @AttackDn.canceled += instance.OnAttackDn;
                    @AttackLt.started += instance.OnAttackLt;
                    @AttackLt.performed += instance.OnAttackLt;
                    @AttackLt.canceled += instance.OnAttackLt;
                    @AttackRt.started += instance.OnAttackRt;
                    @AttackRt.performed += instance.OnAttackRt;
                    @AttackRt.canceled += instance.OnAttackRt;
                    @LookMouse.started += instance.OnLookMouse;
                    @LookMouse.performed += instance.OnLookMouse;
                    @LookMouse.canceled += instance.OnLookMouse;
                    @LookTouch.started += instance.OnLookTouch;
                    @LookTouch.performed += instance.OnLookTouch;
                    @LookTouch.canceled += instance.OnLookTouch;
                    @LookJoy.started += instance.OnLookJoy;
                    @LookJoy.performed += instance.OnLookJoy;
                    @LookJoy.canceled += instance.OnLookJoy;
                }
            }
        }
        public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
        private int m_KeyboardandMouseSchemeIndex = -1;
        public InputControlScheme KeyboardandMouseScheme
        {
            get
            {
                if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
                return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
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
        public interface IPlayerControlsActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnMoveTouch(InputAction.CallbackContext context);
            void OnMoveJoy(InputAction.CallbackContext context);
            void OnAttackUp(InputAction.CallbackContext context);
            void OnAttackDn(InputAction.CallbackContext context);
            void OnAttackLt(InputAction.CallbackContext context);
            void OnAttackRt(InputAction.CallbackContext context);
            void OnLookMouse(InputAction.CallbackContext context);
            void OnLookTouch(InputAction.CallbackContext context);
            void OnLookJoy(InputAction.CallbackContext context);
        }
    }
}
