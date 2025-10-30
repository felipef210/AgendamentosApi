[System.Serializable]
public class NaoEncontradoException : System.Exception
{
    public NaoEncontradoException(string message) : base(message) { }
}