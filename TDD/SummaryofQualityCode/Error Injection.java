//
// Error Injection
//
public class NetRetriever
{
    public NetRetriever() {}
  
    public Response retrieveResponseFor(Request request) throws RetrievalException
    {
        try
        {
            openConnection();
            return makeRequest(request);    // We need to test in case of throwing RemoteException.
        }
        catch(RemoteException remoteEx)
        {
            logError("Error making request", remoteEx);
            throw new RetrievalException(remoteEx);
        }
        finally
        {
            closeConnection();
        }
    }
}

// Test
@Test(expected = RetrievalException.class)
public void testRetrievalResponseFor_Exception() throws RetrievalException
{
    NetRetriever sut = new NetRetriever()
    {
        @Override
        public Response makeRequest(Request request) throws RemoteException
        {
            throw new RemoteException();
        }
    };

    sut.retrieveResponseFor(null);
}

