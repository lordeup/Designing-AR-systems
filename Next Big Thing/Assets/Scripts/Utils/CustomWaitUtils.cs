using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public static class CustomWaitUtils
    {
        public delegate void DelegateWaitMethod();

        public static IEnumerator WaitForSeconds(DelegateWaitMethod method, float second)
        {
            yield return new WaitForSeconds(second);
            method?.Invoke();
        }

        public static IEnumerator WaitWhile(Func<bool> condition, DelegateWaitMethod method)
        {
            yield return new WaitWhile(condition);
            method?.Invoke();
        }
    }
}
