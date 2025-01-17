﻿using Solution.DAL.EF;
using Solution.DO.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using data = Solution.DO.Objects;
using Microsoft.EntityFrameworkCore;


namespace Solution.DAL.Repository
{
    public class RepositoryUsuarioTienda : Repository<data.UsuarioTienda>, IRepositoryUsuarioTienda
    {
        public RepositoryUsuarioTienda(SolutionDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<data.UsuarioTienda>> GetAllWithAsAsync()
        {
            return await _db.UsuarioTienda
                .Include(m => m.Tiendas)
                .Include(m => m.AspNetUsers)
                .ToListAsync();
        }

        public async Task<data.UsuarioTienda> GetOneByIdWithAsync(int id)
        {
            return await _db.UsuarioTienda
                 .Include(m => m.Tiendas)
                .Include(m => m.AspNetUsers)
             .SingleOrDefaultAsync(m => m.IdUsuarioTienda == id);
        }



        private SolutionDbContext _db
        {
            get { return dbContext as SolutionDbContext; }
        }
    }
}
