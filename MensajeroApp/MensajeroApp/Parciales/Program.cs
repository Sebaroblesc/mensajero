using MensajeroModel.DAL;
using MensajeroModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroApp
{
    public partial class Program
    {
        static IMensajesDAL dal = MensajesDALFactory.CreateDal();

        static void IngresarMensaje()
        {
            string nombre, detalle;
            do
            {
                Console.WriteLine("Ingrese nombre:");
                nombre = Console.ReadLine().Trim();

            } while (nombre == string.Empty);

            do
            {
                Console.WriteLine("Ingrese detalle:");
                detalle = Console.ReadLine().Trim();
            } while (detalle == string.Empty);

            Mensaje m = new Mensaje()
            {
                Nombre = nombre,
                Detalle = detalle,
                Tipo = "Aplicacion"
            };
            lock (dal)
            {
                dal.Save(m);
            }


        }

        static void MostrarMensajes()
        {
            List<Mensaje> mensajes = dal.GetAll();
            mensajes.ForEach(m =>
            {
                Console.WriteLine("Nombre:{0} | Detalle: {1} | Tipo:{2}"
                    , m.Nombre, m.Detalle, m.Tipo);
            });
        }

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("Seleccione Opcion:");
            Console.WriteLine("1. Ingresar Mensaje");
            Console.WriteLine("2. Mostrar Mensajes");
            string opcion = Console.ReadLine().Trim();
            switch (opcion)
            {
                case "1":
                    IngresarMensaje();
                    break;
                case "2":
                    MostrarMensajes();
                    break;
                case "0":
                    continuar = false;
                    break;
            }
            return continuar;
        }

    }
}