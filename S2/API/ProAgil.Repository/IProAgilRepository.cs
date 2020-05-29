using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();

         //EVENTO
         Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
         Task<Evento> GetAllEventosAsyncById(int eventoId, bool includePalestrantes);
         Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes);

         //PALESTRANTE
         Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos);
         Task<Palestrante[]> GetAllPalestrantesByName(string name, bool includeEventos);
    }
}