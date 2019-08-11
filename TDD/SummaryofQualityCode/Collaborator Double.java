//
// Collaborator Double
//
public class NetRetriever
{
    // Composition will be replaced by test double
    private Connection myConnection;
  
    public NetRetriever(Connection connection)
    {
        myConnection = connection;
    }
  
    public Response retrieveResponseFor(Request request) throws RetrievalException
    {
        try
        {
            myConnection.open();    // We need to test in case of throwing RemoteException.
            return makeRequest(myConnection, request);
        }
        catch(RemoteException remoteEx)
        {
            logError("Error making request", remoteEx);
            throw new RetrievalException(remoteEx);
        }
        finally
        {
            if (myConnection.isOpen())
            {
                myConnection.close();
            }
        }
    }
}

// Test
@Test(expected = RetrievalException.class)
public void testRetrievalResponseFor_Exception() throws RetrievalException
{
    Connection connection = new Connection()
    {
        @Override
        public void open() throws RemoteException
        {
            throw new RemoteException();
        }
    };

    NetRetriever sut = new NetRetriever(connection);

    sut.retrieveResponseFor(null);
}