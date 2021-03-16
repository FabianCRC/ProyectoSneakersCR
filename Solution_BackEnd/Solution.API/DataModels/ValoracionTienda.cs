﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.API.DataModels
{
    public class ValoracionTienda
    {
        public decimal IdValoracion { get; set; }
        public decimal Valoracion { get; set; }
        public string Comentario { get; set; }
        public decimal IdUsuario { get; set; }
        public decimal IdTienda { get; set; }

        public virtual Tiendas IdTiendaNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
