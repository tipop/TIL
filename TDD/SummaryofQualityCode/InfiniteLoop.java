//
// InfiniteLoop
//

void infiniteLoop()
{
    while (true)
    {
        // do something

        if ( /* some condition */ ) 
        {
            break;
        }
    }
}

// Refactored this code above for testing
boolean shouldContinueLoop()
{
    return true;
}

void infiniteLoop()
{
    while(shouldContinueLoop())
    {
        // do something

        if ( /* some condition */ )
        {
            break;
        }
    }
}