using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RageCure.EventUtils
{
    /// <summary>
    /// EventAggregator implementation from PlurialSight design patterns video
    /// </summary>
    public class EventAggregator : MonoBehaviour
    {
        private readonly IDictionary<Type, List<object>> _subscriptions = new Dictionary<Type, List<object>>();
        private readonly object _lock = new object();

        public void Publish<TEventType>(TEventType eventToPublish)
        {
            var subscriberType = typeof(ISubscriber<>).MakeGenericType(typeof(TEventType));
            var subscribers = GetSubscribers(subscriberType);

            foreach (var weakSubscriber in subscribers)
            {
                var subscriber = (ISubscriber<TEventType>)weakSubscriber;
                subscriber.OnEvent(eventToPublish);
            }
        }

        public void Subscribe(object subscriber)
        {
            lock (_lock)
            {
                //try
                //{
                    var subscriberTypes =
                    subscriber.GetType().GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));

                    var weakReference = subscriber;
                    foreach (var subscriberType in subscriberTypes)
                    {
                        var subscribers = GetSubscribers(subscriberType);
                        subscribers.Add(weakReference);
                    }
                //}
                //catch (Exception ex)
                //{
                //    throw
                //}
            }
        }

        public void Unsubscribe(object subscriber)
        {
            lock (_lock)
            {
                foreach (var subscription in _subscriptions)
                {
                    if (subscription.Value == subscriber)
                    {
                        _subscriptions.Remove(subscription.Key);
                        return;
                    }
                }
            }
        }

        private List<object> GetSubscribers(Type subscriberType)
        {
            List<object> subscribers;
            lock (_lock)
            {
                var found = _subscriptions.TryGetValue(subscriberType, out subscribers);
                if (!found)
                {
                    subscribers = new List<object>();
                    _subscriptions.Add(subscriberType, subscribers);
                }
            }
            return subscribers;
        }
    }
}

