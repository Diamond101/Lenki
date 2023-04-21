namespace LenkiMicroservice.Interface
{
    public  interface ITokenBuilder
    {
        string BuildToken(string username);
        
    }
}
