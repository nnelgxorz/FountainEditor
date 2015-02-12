using System;
using System.Collections.Generic;

namespace FountainEditor.Messaging
{
    public sealed class MessagePublisher<TMessage> : IMessagePublisher<TMessage> where TMessage : class
    {
        private ICollection<Action<TMessage>> subscribers;

        public MessagePublisher(ICollection<Action<TMessage>> subscribers)
        {
            this.subscribers = subscribers;
        }

        public void Publish(TMessage message)
        {
            foreach (var subscriber in subscribers)
            {
                subscriber(message);
            }
        }

        public void Subscribe(Action<TMessage> message)
        {
            subscribers.Add(message);
        }
    }
}
