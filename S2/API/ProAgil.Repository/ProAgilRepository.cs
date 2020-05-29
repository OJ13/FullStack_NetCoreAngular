using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;
        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                        .Include(l => l.Lotes)
                                        .Include(r => r.RedesSociais);
            if(includePalestrantes){
                query = query.Include(pe => pe.PalestranteEventos)
                            .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                .OrderBy(e => e.EventoId);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventosAsyncById(int eventoId, bool includePalestrantes = false)
        {
             IQueryable<Evento> query = _context.Eventos
                                        .Include(l => l.Lotes)
                                        .Include(r => r.RedesSociais);
            if(includePalestrantes){
                query = query.Include(pe => pe.PalestranteEventos)
                            .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                .OrderBy(e => e.EventoId)
                .Where(p => p.EventoId == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                        .Include(l => l.Lotes)
                                        .Include(r => r.RedesSociais);
            if(includePalestrantes){
                query = query.Include(pe => pe.PalestranteEventos)
                            .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking()
                .OrderByDescending(e => e.DataEvento)
                .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                        .Include(r => r.RedesSociais);
            if(includeEventos){
                query = query.Include(pe => pe.PalestranteEventos)
                            .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                .OrderBy(c => c.Nome)
                .Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByName(string name, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                        .Include(r => r.RedesSociais);
            if(includeEventos){
                query = query.Include(pe => pe.PalestranteEventos)
                            .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                .OrderBy(c => c.Nome)
                .Where(p => p.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}