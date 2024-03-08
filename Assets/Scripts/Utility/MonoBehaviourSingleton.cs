using UnityEngine;

// With apologies to ErkrodC @ https://github.com/ErkrodC & Kokowolo @ https://www.reddit.com/user/Kokowolo/
public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    static T m_instance { get; set; }
    public static T instance
    {
        get
        {
            if (m_instance is null)
				m_instance = FindObjectOfType<T>() ?? new GameObject().AddComponent<T>();

            return m_instance;
		}
        
        private set
        {
			m_instance = value;
        }
    }

    public static void Set(T instance, bool dontDestroyOnLoad = true)
    {
        if (m_instance)
        {
            if (!ReferenceEquals(m_instance, instance))
                DestroyImmediate(instance.gameObject);
        }
		else
        {
			m_instance = instance;
            if (dontDestroyOnLoad) DontDestroyOnLoad(instance.gameObject);
		}
	}

    public static void Release()
    {
        if (!instance) return;
        Destroy(instance.gameObject);
    }
}
