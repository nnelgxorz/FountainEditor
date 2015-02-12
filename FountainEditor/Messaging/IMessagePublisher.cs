using System;

namespace FountainEditor.Messaging
{
    public interface IMessagePublisher<TMessage> where TMessage : class
    {
        void Publish(TMessage message);

        void Subscribe(Action<TMessage> message);
    }
}
