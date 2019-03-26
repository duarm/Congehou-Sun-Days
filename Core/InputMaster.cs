// GENERATED AUTOMATICALLY FROM 'Assets/Game/Input/InputMaster.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


namespace Congehou
{
    [Serializable]
    public class InputMaster : InputActionAssetReference
    {
        public InputMaster()
        {
        }
        public InputMaster(InputActionAsset asset)
            : base(asset)
        {
        }
        private bool m_Initialized;
        private void Initialize()
        {
            // Gameplay
            m_Gameplay = asset.GetActionMap("Gameplay");
            m_Gameplay_Shoot = m_Gameplay.GetAction("Shoot");
            m_Gameplay_Movement = m_Gameplay.GetAction("Movement");
            m_Gameplay_Dash = m_Gameplay.GetAction("Dash");
            m_Gameplay_Walk = m_Gameplay.GetAction("Walk");
            m_Gameplay_StopTime = m_Gameplay.GetAction("StopTime");
            m_Gameplay_CheatMode = m_Gameplay.GetAction("CheatMode");
            m_Gameplay_Skip = m_Gameplay.GetAction("Skip");
            // Menu
            m_Menu = asset.GetActionMap("Menu");
            m_Menu_Enter = m_Menu.GetAction("Enter");
            m_Menu_Up = m_Menu.GetAction("Up");
            m_Menu_Down = m_Menu.GetAction("Down");
            m_Initialized = true;
        }
        private void Uninitialize()
        {
            if (m_GameplayActionsCallbackInterface != null)
            {
                Gameplay.SetCallbacks(null);
            }
            m_Gameplay = null;
            m_Gameplay_Shoot = null;
            m_Gameplay_Movement = null;
            m_Gameplay_Dash = null;
            m_Gameplay_Walk = null;
            m_Gameplay_StopTime = null;
            m_Gameplay_CheatMode = null;
            m_Gameplay_Skip = null;
            if (m_MenuActionsCallbackInterface != null)
            {
                Menu.SetCallbacks(null);
            }
            m_Menu = null;
            m_Menu_Enter = null;
            m_Menu_Up = null;
            m_Menu_Down = null;
            m_Initialized = false;
        }
        public void SetAsset(InputActionAsset newAsset)
        {
            if (newAsset == asset) return;
            var GameplayCallbacks = m_GameplayActionsCallbackInterface;
            var MenuCallbacks = m_MenuActionsCallbackInterface;
            if (m_Initialized) Uninitialize();
            asset = newAsset;
            Gameplay.SetCallbacks(GameplayCallbacks);
            Menu.SetCallbacks(MenuCallbacks);
        }
        public override void MakePrivateCopyOfActions()
        {
            SetAsset(ScriptableObject.Instantiate(asset));
        }
        // Gameplay
        private InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private InputAction m_Gameplay_Shoot;
        private InputAction m_Gameplay_Movement;
        private InputAction m_Gameplay_Dash;
        private InputAction m_Gameplay_Walk;
        private InputAction m_Gameplay_StopTime;
        private InputAction m_Gameplay_CheatMode;
        private InputAction m_Gameplay_Skip;
        public struct GameplayActions
        {
            private InputMaster m_Wrapper;
            public GameplayActions(InputMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @Shoot { get { return m_Wrapper.m_Gameplay_Shoot; } }
            public InputAction @Movement { get { return m_Wrapper.m_Gameplay_Movement; } }
            public InputAction @Dash { get { return m_Wrapper.m_Gameplay_Dash; } }
            public InputAction @Walk { get { return m_Wrapper.m_Gameplay_Walk; } }
            public InputAction @StopTime { get { return m_Wrapper.m_Gameplay_StopTime; } }
            public InputAction @CheatMode { get { return m_Wrapper.m_Gameplay_CheatMode; } }
            public InputAction @Skip { get { return m_Wrapper.m_Gameplay_Skip; } }
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled { get { return Get().enabled; } }
            public InputActionMap Clone() { return Get().Clone(); }
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                    Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                    Shoot.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                    Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    Movement.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                    Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                    Dash.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                    Walk.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnWalk;
                    Walk.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnWalk;
                    Walk.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnWalk;
                    StopTime.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStopTime;
                    StopTime.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStopTime;
                    StopTime.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStopTime;
                    CheatMode.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCheatMode;
                    CheatMode.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCheatMode;
                    CheatMode.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCheatMode;
                    Skip.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkip;
                    Skip.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkip;
                    Skip.cancelled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSkip;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Shoot.started += instance.OnShoot;
                    Shoot.performed += instance.OnShoot;
                    Shoot.cancelled += instance.OnShoot;
                    Movement.started += instance.OnMovement;
                    Movement.performed += instance.OnMovement;
                    Movement.cancelled += instance.OnMovement;
                    Dash.started += instance.OnDash;
                    Dash.performed += instance.OnDash;
                    Dash.cancelled += instance.OnDash;
                    Walk.started += instance.OnWalk;
                    Walk.performed += instance.OnWalk;
                    Walk.cancelled += instance.OnWalk;
                    StopTime.started += instance.OnStopTime;
                    StopTime.performed += instance.OnStopTime;
                    StopTime.cancelled += instance.OnStopTime;
                    CheatMode.started += instance.OnCheatMode;
                    CheatMode.performed += instance.OnCheatMode;
                    CheatMode.cancelled += instance.OnCheatMode;
                    Skip.started += instance.OnSkip;
                    Skip.performed += instance.OnSkip;
                    Skip.cancelled += instance.OnSkip;
                }
            }
        }
        public GameplayActions @Gameplay
        {
            get
            {
                if (!m_Initialized) Initialize();
                return new GameplayActions(this);
            }
        }
        // Menu
        private InputActionMap m_Menu;
        private IMenuActions m_MenuActionsCallbackInterface;
        private InputAction m_Menu_Enter;
        private InputAction m_Menu_Up;
        private InputAction m_Menu_Down;
        public struct MenuActions
        {
            private InputMaster m_Wrapper;
            public MenuActions(InputMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @Enter { get { return m_Wrapper.m_Menu_Enter; } }
            public InputAction @Up { get { return m_Wrapper.m_Menu_Up; } }
            public InputAction @Down { get { return m_Wrapper.m_Menu_Down; } }
            public InputActionMap Get() { return m_Wrapper.m_Menu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled { get { return Get().enabled; } }
            public InputActionMap Clone() { return Get().Clone(); }
            public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
            public void SetCallbacks(IMenuActions instance)
            {
                if (m_Wrapper.m_MenuActionsCallbackInterface != null)
                {
                    Enter.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnEnter;
                    Enter.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnEnter;
                    Enter.cancelled -= m_Wrapper.m_MenuActionsCallbackInterface.OnEnter;
                    Up.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnUp;
                    Up.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnUp;
                    Up.cancelled -= m_Wrapper.m_MenuActionsCallbackInterface.OnUp;
                    Down.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnDown;
                    Down.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnDown;
                    Down.cancelled -= m_Wrapper.m_MenuActionsCallbackInterface.OnDown;
                }
                m_Wrapper.m_MenuActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Enter.started += instance.OnEnter;
                    Enter.performed += instance.OnEnter;
                    Enter.cancelled += instance.OnEnter;
                    Up.started += instance.OnUp;
                    Up.performed += instance.OnUp;
                    Up.cancelled += instance.OnUp;
                    Down.started += instance.OnDown;
                    Down.performed += instance.OnDown;
                    Down.cancelled += instance.OnDown;
                }
            }
        }
        public MenuActions @Menu
        {
            get
            {
                if (!m_Initialized) Initialize();
                return new MenuActions(this);
            }
        }
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get

            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.GetControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get

            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
    }
    public interface IGameplayActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
        void OnStopTime(InputAction.CallbackContext context);
        void OnCheatMode(InputAction.CallbackContext context);
        void OnSkip(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnEnter(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
    }
}
