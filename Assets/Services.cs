using System.Collections.Generic;
using System;
using UnityEngine;

public class Services
{
    static Dictionary<Type, object> services = new Dictionary<Type, object>();

    public static void Register<T>(T service) where T : UnityEngine.Object
    {
        if (services.ContainsKey(typeof(T)))
        {
            Debug.Log("There is already a service of this type registered");
            return;
        }
        Debug.Log(typeof(T) + " Service registered");
        services.Add(typeof(T), service);
    }

    public static void Unregister<T>(T service) where T : UnityEngine.Object
    {
        var registered = Request<T>();
        if (registered.Equals(service))
        {
            Debug.Log("Service Unregistered");
            services.Remove(typeof(T));
        }
        else
        {
            Debug.Log("Service is of a different source");
        }
    }

    public static T Request<T>(bool searchUnavailable = true) where T : UnityEngine.Object
    {
        object service;
        var hasService = services.TryGetValue(typeof(T), out service);
        if (!hasService && searchUnavailable)
        {
                service = GameObject.FindObjectOfType<T>();
                if (service == default)
                {

                    Debug.Log("There is no service of requested type");
                }
                else
                {
                    Register<T>(service as T);
                    hasService = true;
                }
        }
        return hasService ? (T)service : default;
    }
}
