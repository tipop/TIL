//
// This code below is not thread safe.
//

public final class HostInfoService // Singleton
{
    private static HostInfoService instance;
  
    private HostInfoService() {...}

    public static synchronized HostInfoService getInstance()
    {
        if (instance == null) 
        {
            instance = new HostInfoService();
        }
    }

    public HostInfo getHostInfo(String hostName) 
    {
        HostInfo hostInfo = new HostInfo();
        ...
        return hostInfo;
    }
}

public class HostCache 
{
    private Map<String, HostInfo> hostInfoCache;

    public HostInfo getHostInfo(String hostName) 
    {
        HostInfo info = hostInfoCache.get(hostName);
        if (info == null)
        {
            info = HostInfoService.getInstance().getHostInfo(hostName); // 1. getInstance() 2. getHostInfo()
            hostInfoCache.put(hostName, info);  // 3. Map.put()
        }
        return info;
    }
}

// This code below is Callable to reproduce race condition
private class GetHostInfoRaceTask implements Callable<HostInfo> 
{
    private final HostCache cache;
    private final String hostName;
    
    public GetHostInfoRaceTask(HostCache cache, String hosteName)
    {
        this.cache = cache;
        this.hostName = hostName;
    }
    
    @Override
    public HostInfo call() throws Exception
    {
        return cache.getHostInfo(hostName);
    }
}

// This unit test code below is to reproduce race condition
@Test
public void testGetHostInfo_Race() throws InterrptedException, ExecutionException
{
    HostCache cache = new HostCache();
    String testHost = "katana";
    
    FutureTask<HostInfo> task1 = new FeatureTask<HostInfo> ( new GetHostInfoRaceTask(cache, testHost) );
    FutureTask<HostInfo> task2 = new FeatureTask<HostInfo> ( new GetHostInfoRaceTask(cache, testHost) );
    
    Thread t1 = new Thread(task1);
    Thread t2 = new Thread(task2);
    
    thread1.start();
    thread2.start();

    HostInfo result1 = task1.get();
    HostInfo result2 = task2.get();
    
    Assert.assertNotNull(result1);
    Assert.assertNotNull(result2);
    Assert.assertSame(result1, result2); // It will be sometimes failed.
}
