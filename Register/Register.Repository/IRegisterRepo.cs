namespace Register.Repository
{
    public interface IRegisterRepo
    {
        bool IsExist(string name);
        bool HasAny(string name, string password);
        void Insert(string name, string password);
        int SaveChanges();
    }
}