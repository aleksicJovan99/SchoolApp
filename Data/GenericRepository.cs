using SchoolApp.Interfaces;

namespace SchoolApp;

public class GenericRepository : IGenericRepository
{
    private schoolDBContext _context;

    public GenericRepository(schoolDBContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll<T>() where T : class
    {
        return _context.Set<T>().ToList();
    }

    public void Insert<T>(T obj) where T : class
    {
        _context.Set<T>().Add(obj);
    }

    public void Delete<T>(int id) where T : class
    {
        T obj = _context.Set<T>().Find(id);
        _context.Set<T>().Remove(obj);
    }

    public T GetById<T>(int id) where T : class
    {
        T obj = _context.Set<T>().Find(id);
        return obj;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}