namespace App.Repository.Models.Attributes
{
    [Serializable]
    public class RepositoryErrorException : Exception
    {
        public RepositoryErrorException(string error) : base(error) { }
    }
}
