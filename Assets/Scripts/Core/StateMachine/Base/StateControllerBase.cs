using System;
using System.Collections.Generic;
using Core.StateMachine.Interface;
using UnityEngine;

namespace Core.StateMachine.Base
{
    public class StateControllerBase
    {
        private readonly Dictionary<Type, IState> _states = new();
        private IState _currentState;

        public IState GetState<T>()
        {
            return _states.GetValueOrDefault(typeof(T));
        }

        protected void AddState<T>(T state) where T : IState
        {
            if (_states.ContainsKey(typeof(T)))
            {
                Debug.LogError($"State {typeof(T)} already exists in the dictionary.");
                return;
            }
            _states.Add(typeof(T), state);
        }
        
        public void Update() => _currentState.Update();

        public void SetState<T>() where T : IState
        {
            if (_states.TryGetValue(typeof(T), out var newState))
            {
                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
            }
            else
            {
                Debug.LogError($"State {typeof(T)} not found in the dictionary.");
            }
        }
        public IState GetCurrentState() => _currentState;
    }
}