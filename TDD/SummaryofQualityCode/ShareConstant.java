//
// Share constant
//

public class FixedThreadPool
{
    private final int myPoolSize;

    public FixedThreadPool(int poolSize)
    {
        myPoolSize = poolSize;
    }
    public FixedThreadPool()
    {
        this(10);
    }        
    public int getPoolSize()
    {
        return myPoolSize;
    }
}

// 기본값을 내부적으로 사용하는 클래스 테스트
@Test
public void testFixedThreadPool()
{
    FixedThreadPool sut = new FixedThreadPool();

    int actualPoolSize = sut.getPoolSize();

    assertEquals(10, actualPoolSize);
}

// Refactored this code above
public class FixedThreadPool
{
    public static final DEFAULT_POOL_SIZE = 10;
    private final int myPoolSize;
    
    ...
    public FixedThreadPool()
    {
        this(DEFAULT_POOL_SIZE);
    }        
    ...
}

@Test
public void testFixedThreadPool()
{
    FixedThreadPool sut = new FixedThreadPool();

    int actualPoolSize = sut.getPoolSize();

    assertEquals(FixedThreadPool.DEFAULT_POOL_SIZE, actualPoolSize);
}
