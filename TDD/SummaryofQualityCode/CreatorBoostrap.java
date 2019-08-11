public void testMyObject() 
{ 
    MyObject sut = new MyObject(); 
    assertNotNull(sut); 
    assertEquals(expectedVaule, sut.getAttribute()); 
}
