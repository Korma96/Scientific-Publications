namespace ScientificPublications.DataAccess.User
{
    public interface IUserDataAccess : IDataAccess
    {
        Model.User FindByUsername(string username);
    }
}
