namespace Authentication.Provider
{
    public interface IAuthProvider
    {
        string FetchCredential(string mailId);
    }
}
