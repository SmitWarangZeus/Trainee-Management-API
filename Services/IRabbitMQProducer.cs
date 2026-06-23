namespace TraineeManagement.api.Services;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}