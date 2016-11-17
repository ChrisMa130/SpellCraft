public class OrbTestClass
{
    // Class ctor used for testing purposes. Not called by the game itself.
    private const int MAGIC_STORED = 1;

    private int index;


    public OrbTestClass()
    {
        index = 0;
    }

    // Returns the index field of Orb
    public int GetIndex()
    {
        return index;
    }

    // Modifies index to match the value passed in
    public void SetIndex(int index)
    {
        this.index = index;
    }
}