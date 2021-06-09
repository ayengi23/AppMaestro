using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSMaestro
{
    public class Connect
    {
        public string Conexion()
        {
            const string cn_string = @"Data Source = EIKO\DBLOCAL_1; Initial Catalog = dbConnect; User Id = usr_prueba; Password = Prueba2021$ ";

            return cn_string;
        } 
    }
}