﻿// for uGUI
#if UNITY_4_6

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

namespace UniRx
{
    public static partial class UnityEventExtensions
    {
        public static IObservable<Unit> AsObservable(this UnityEngine.Events.UnityEvent unityEvent)
        {
            return Observable.FromEvent<UnityAction>(h => h.Invoke, h => unityEvent.AddListener(h), h => unityEvent.RemoveListener(h));
        }

        public static IObservable<T> AsObservable<T>(this UnityEngine.Events.UnityEvent<T> unityEvent)
        {
            return Observable.FromEvent<UnityAction<T>, T>(h => h.Invoke, h => unityEvent.AddListener(h), h => unityEvent.RemoveListener(h));
        }

        public static IObservable<Tuple<T0, T1>> AsObservable<T0, T1>(this UnityEngine.Events.UnityEvent<T0, T1> unityEvent)
        {
            return Observable.FromEvent<UnityAction<T0, T1>, Tuple<T0, T1>>(
                h => (t0, t1) => h(Tuple.Create(t0, t1)),
                h => unityEvent.AddListener(h), h => unityEvent.RemoveListener(h));
        }

        public static IObservable<Tuple<T0, T1, T2>> AsObservable<T0, T1, T2>(this UnityEngine.Events.UnityEvent<T0, T1, T2> unityEvent)
        {
            return Observable.FromEvent<UnityAction<T0, T1, T2>, Tuple<T0, T1, T2>>(
                h => (t0, t1, t2) => h(Tuple.Create(t0, t1, t2)),
                h => unityEvent.AddListener(h), h => unityEvent.RemoveListener(h));
        }

        public static IObservable<Tuple<T0, T1, T2, T3>> AsObservable<T0, T1, T2, T3>(this UnityEngine.Events.UnityEvent<T0, T1, T2, T3> unityEvent)
        {
            return Observable.FromEvent<UnityAction<T0, T1, T2, T3>, Tuple<T0, T1, T2, T3>>(
                h => (t0, t1, t2, t3) => h(Tuple.Create(t0, t1, t2, t3)),
                h => unityEvent.AddListener(h), h => unityEvent.RemoveListener(h));
        }
    }
}

#endif