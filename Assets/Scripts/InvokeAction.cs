using System;
using System.Collections;
using System.Linq;
using System.Text;

using UnityEngine;

public static class InvokeAction
{
    public static void Invoke(Action action, float delay)
    {
        CoroutineHelper.Instance.StartCoroutine(InternalInvoke(action, delay));
    }

    private static IEnumerator InternalInvoke(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (action != null)
            action();
    }
}

public class CoroutineHelper : MonoBehaviourSingleton<CoroutineHelper>
{
}

// Taken from http://wiki.unity3d.com/index.php/CoroutineHelper
public class MonoBehaviourSingleton<TSelfType> : MonoBehaviour where TSelfType : MonoBehaviour
{
    private static TSelfType m_Instance = null;
    public static TSelfType Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (TSelfType)FindObjectOfType(typeof(TSelfType));
                if (m_Instance == null)
                {
                    m_Instance = (new GameObject(typeof(TSelfType).Name)).AddComponent<TSelfType>();
                }
                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }
}