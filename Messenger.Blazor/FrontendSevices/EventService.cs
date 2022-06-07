public class EventService 
{
    public Func<object, EventArgs, Task> MyEvent;
    
    public Task OnMessage(object sender, EventArgs e)
    {
        MyEvent?.Invoke(sender, e);
        return Task.CompletedTask;
    }
}