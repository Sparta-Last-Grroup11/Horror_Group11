public class PuzzleManager : Singleton<PuzzleManager>
{
    public OpenPuzzle GateHouseSaveDial;
    private bool isPowerOn;

    public void OnPower()
    {
        isPowerOn = true;
    }

    public bool GetIsPowerOn()
    {
        return isPowerOn;
    }
}
