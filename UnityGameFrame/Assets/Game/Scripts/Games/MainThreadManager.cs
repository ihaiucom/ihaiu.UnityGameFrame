using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Games
{
    public class MainThreadManager : MonoBehaviour 
    {

        public delegate void UpdateEvent();
        public UpdateEvent unityUpdate = null;
        public UpdateEvent unityFixedUpdate = null;

        public bool UseFixedUpdate = false;

        /// <summary>
        /// A list of functions to run
        /// </summary>
        private List<Action> mainThreadActions = new List<Action>();

        /// <summary>
        /// A mutex to be used to prevent threads from overriding each others logic
        /// </summary>
        private object mutex = new object();





        /// <summary>
        /// Add a function to the list of functions to call on the main thread via the Update function
        /// </summary>
        /// <param name="action">The method that is to be run on the main thread</param>
        public void Run(Action action)
        {

            // Make sure to lock the mutex so that we don't override
            // other threads actions
            lock (mutex)
            {
                mainThreadActions.Add(action);
            }
        }

        private void HandleActions() {
            // If there are any functions in the list, then run
            // them all and then clear the list
            if (mainThreadActions.Count > 0)
            {
                // Get a copy of all of the actions
                List<Action> copiedActions = new List<Action>();

                lock (mutex)
                {
                    copiedActions.AddRange(mainThreadActions.ToArray());
                    mainThreadActions.Clear();
                }

                foreach (Action action in copiedActions)
                    action();
            }

            // If there are any buffered actions then move them to the main list
            //if (mainThreadActionsBuffer.Count > 0)
            //{
            //  mainThreadActions.AddRange(mainThreadActionsBuffer.ToArray());
            //  mainThreadActionsBuffer.Clear();
            //}
        }

        private void Update()
        {
            // JM: added support for actions to be handled in fixed loop
            if (!UseFixedUpdate) 
                HandleActions();

            if (unityUpdate != null)
                unityUpdate();
        }

        private void FixedUpdate()
        {
            // JM: added support for actions to be handled in fixed loop
            if (UseFixedUpdate) 
                HandleActions ();

            if (unityFixedUpdate != null)
                unityFixedUpdate();
        }

        private Dictionary<IEnumerator, Coroutine> coroutines = new Dictionary<IEnumerator, Coroutine>();
		public Coroutine MStartCoroutine(IEnumerator routine)
        {
            Debug.Log(coroutines.ContainsKey(routine) + "   " + routine.ToString());
            if (coroutines.ContainsKey(routine))
            {
                StopCoroutine(coroutines[routine]);
                coroutines.Remove(routine);
            }
            Coroutine corutine = base.StartCoroutine(routine);
            coroutines.Add(routine, corutine);
            return corutine;
        }


		public void MStopAllCoroutines()
        {
            coroutines.Clear();
            base.StopAllCoroutines();
        }

    }
}