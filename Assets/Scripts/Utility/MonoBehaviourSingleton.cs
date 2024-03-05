using UnityEngine;

// With apologies to ErkrodC @ https://github.com/ErkrodC & Kokowolo @ https://www.reddit.com/user/Kokowolo/
public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    public static T instance { get; private set; }

    public static void Set(T instance, bool dontDestroyOnLoad = true)
    {
        if (MonoBehaviourSingleton<T>.instance)
        {
            if (!ReferenceEquals(MonoBehaviourSingleton<T>.instance.gameObject, instance.gameObject))
                DestroyImmediate(instance.gameObject);
        }
		else
        {
			MonoBehaviourSingleton<T>.instance = instance;
            if (dontDestroyOnLoad) DontDestroyOnLoad(instance.gameObject);
		}
	}

    public static T Get()
    {
        if (!instance)
            Set(FindObjectOfType<T>() ?? new GameObject().AddComponent<T>());

        return instance;
    }

    public static void Release()
    {
        if (!instance) return;
        Destroy(instance.gameObject);
    }
}
