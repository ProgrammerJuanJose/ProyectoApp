using ProyectoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoApp
{
    public static class GlobalObjects
    {
        //definimos las propiedades de codificación de los json
        //que usaré en los modelos
        public static string MimeType = "application/json";
        public static string ContentType = "Content-Type";

        //Crear el objeto local (Global) de usuario
        public static Usuario MyLocalUser = new Usuario();
    }
}

