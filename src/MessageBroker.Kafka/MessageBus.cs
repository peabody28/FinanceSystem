using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;

namespace MessageBroker.Kafka
{
    public sealed class MessageBus<T> : IDisposable
    {
        private IProducer<Null, T> _producer;
        private IConsumer<Null, T> _consumer;

        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _consumerConfig;

        public MessageBus() : this("localhost") { }

        public MessageBus(string host)
        {
            _producerConfig = new ProducerConfig { BootstrapServers = host };

            _consumerConfig = new ConsumerConfig { GroupId = "custom-group", BootstrapServers = host, AutoOffsetReset = AutoOffsetReset.Earliest, };

            _producer = new ProducerBuilder<Null, T>(_producerConfig).Build();
        }

        public void SendMessage(string topic, T data)
        {
            _producer.ProduceAsync(topic, new Message<Null, T> { Value = data });
        }

        public void SubscribeOnTopic<T>(string topic, Action<T> action, CancellationToken cancellationToken) where T : class
        {
            var msgBus = new MessageBus<T>();

            using (msgBus._consumer = new ConsumerBuilder<Null, T>(_consumerConfig).Build())
            {
                msgBus._consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topic, 0, -1) });

                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    var response = msgBus._consumer.Consume();
                    action(response.Message.Value);
                }
            }
        }

        public void Dispose()
        {
            _producer?.Dispose();
            _consumer?.Dispose();
        }

    }
}