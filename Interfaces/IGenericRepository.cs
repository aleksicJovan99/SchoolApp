namespace SchoolApp.Interfaces;

public interface IGenericRepository
{
   IEnumerable<T> GetAll<T>() where T : class;
   void Insert<T>(T obj) where T : class;

   void Delete<T>(int id) where T : class;

   T GetById<T>(int id) where T : class;
   void Save();
}