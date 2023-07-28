using System.Collections;
using UnityEngine;


namespace FallingBall
{
    public static class Utilities
    {
        public static T GetRandomEnum<T>()
        {
            System.Array A = System.Enum.GetValues(typeof(T));
            T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
            return V;
        }

        public static IEnumerator WaitAfter(float time, System.Action callback)
        {
            yield return new WaitForSeconds(time);
            callback?.Invoke();
        }

        public static IEnumerator WaitAfterRealtime(float time, System.Action callback)
        {
            yield return new WaitForSecondsRealtime(time);
            callback?.Invoke();
        }
    }
}
