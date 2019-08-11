// 이벤트 두 개가 잇달아 동시 도착했을 때 문제가 발생한다.
class Agent implements Runnable
{
    private BlockingQueue<Event> incoming;
    ...
    
    @Override
    public void run()
    {
        processEvents();
    }
    
    public void processEvents()
    {
        Event event;
        while (event = incoming.take())
        {
            processEvent(event);
        }
    }
    
    public void addEvent(Event event)
    {
        incoming.put(event);
    }
    
    private void processEvent(Event event)
    {
        // 이벤트로 어떤 일을 하라
    }
}

// 100% 버그가 재현되는 테스트
public class AgentTest
{
    @Test
    public void testProcessEvents()
    {
        Agent sut = new Agent();
        Thread thread = new Thread(sut);
        thread.start();
        
        while (thread.getState() != Thread.State.BLOCKED) {}
        thread.suspend(); // 스레드르 멈춰라
        
        // 이중 이벤트를 큐 넣어라
        sut.addEvent (new ProblematicEvent());
        sut.addEvent (new ProblematicEvent());
        thread.resume();    // 버그가 유발됐다!
    }
}
