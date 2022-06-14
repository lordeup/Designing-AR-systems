using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public static class CustomWaitUtils
    {
        public static IEnumerator WaitForSeconds(SharedUtils.DelegateMethod method, float second)
        {
            yield return new WaitForSeconds(second);
            method?.Invoke();
        }

        public static IEnumerator WaitWhile(Func<bool> condition, SharedUtils.DelegateMethod method)
        {
            yield return new WaitWhile(condition);
            method?.Invoke();
        }
        
        public static IEnumerator WaitWhile(Func<bool> condition)
        {
            yield return new WaitWhile(condition);
        }
    }
}
