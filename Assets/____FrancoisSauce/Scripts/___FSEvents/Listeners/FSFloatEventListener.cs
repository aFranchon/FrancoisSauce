﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using FrancoisSauce.Scripts.FSEvents.SO;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace FrancoisSauce.Scripts.FSEvents.Listeners
{
    /// <summary>
    /// <see cref="float"/> implementation of <see cref="IFSOneArgumentEventListener{T}"/>
    /// </summary>
    /// <inheritdoc cref="IFSOneArgumentEventListener{T}"/>
    /// <inheritdoc cref="IFSEventListener"/>
    public class FSFloatEventListener : IFSOneArgumentEventListener<float>
    {
        /// <summary>
        /// The FSEventSO to subscribe to of type <see cref="FSFloatEventSO"/>
        /// </summary>
        [Tooltip("The float event to listen")]
        public FSFloatEventSO fsFloatEventSo;

        /// <summary>
        /// Public <see cref="UnityEvent{T0}"/> to be raised when the given <see cref="FSFloatEventSO"/> is raised
        /// </summary>
        [Tooltip("The methods to be called when above event is raised")]
        [SerializeField] public FSFloatEvent listeners = new FSFloatEvent();
        
        /// <summary>
        /// This function is call if you click on the auto add button in the unity editor
        /// Also called at start if there are no event registered in the <see cref="UnityEvent{T0}"/>
        /// Your method name HAVE to be the SAME as the <see cref="fsFloatEventSo"/> name
        /// </summary>
#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [Button("Auto Add")]
        [ContextMenu("Auto Add")]
#endif
        private void AutoAdd()
        {
            // Keeping track of already registered events
            var listenersName = new List<string>();
            var listenersTarget = new List<Type>();

            for (var i = 0; i < listeners.GetPersistentEventCount(); i++)
            {
                listenersName.Add(listeners.GetPersistentMethodName(i));
                listenersTarget.Add(listeners.GetPersistentTarget(i).GetType());
            }
            
            // Find all behaviours in the current gameObject
            var allBehaviours = GetComponents<MonoBehaviour>();
            // Keep track of event name
            var eventName = fsFloatEventSo.name.Replace(" ", "");

            foreach (var monoBehaviour in allBehaviours)
            {
                if (monoBehaviour == this) continue;
                
                // Get all method names in the behaviour
                MethodInfo[] methodName = monoBehaviour.GetType().GetMethods();
                foreach (var methodInfo in methodName)
                {
                    // Reject non equal names
                    if (eventName != methodInfo.Name) continue;
                    // Reject already registered events
                    if (listenersTarget.Contains(monoBehaviour.GetType()) && listenersName.Contains(methodInfo.Name)) continue;

                    // If not rejected create new delegate and finish the method
                    var action = methodInfo.CreateDelegate(typeof(UnityAction), monoBehaviour) as UnityAction<float>;
                    UnityEventTools.AddPersistentListener(listeners, action);
                }
            }
        }

#endif

        #region Listener Handler

        protected override void AddListeners()
        {
            fsFloatEventSo.fsOneArgumentAction += Invoke;
        }

        protected override void RemoveListeners()
        {
            fsFloatEventSo.fsOneArgumentAction -= Invoke;
        }

        protected override void Invoke(float value)
        {
            listeners.Invoke(value);
        }

        #endregion
        
        #region Unity Functions

        protected override void Awake()
        {
#if UNITY_EDITOR
            if (listeners.GetPersistentEventCount() == 0)
                AutoAdd();
#endif
            
            RemoveListeners();
            AddListeners();
        }

        protected override void OnEnable()
        {
            RemoveListeners();
            AddListeners();
        }

        protected override void OnDisable()
        {
            RemoveListeners();
        }

        protected override void OnDestroy()
        {
            RemoveListeners();
        }

        #endregion
    }
}