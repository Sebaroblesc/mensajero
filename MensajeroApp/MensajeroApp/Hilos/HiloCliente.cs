using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MensajeroModel.DAL;
using MensajeroModel.DTO;
using SocketUtils;

namespace MensajeroApp.Hilos
{
    class HiloCliente
    {
        private IMensajesDAL dal = MensajesDALFactory.CreateDal();
        private ClienteSocket clienteSocket;

        public HiloCliente(ClienteSocket clientesocket)
        {
            this.clienteSocket = clientesocket;
        }

        public void Ejecutar()
        {
            string nombre, detalle;
            do
            {
                clienteSocket.Escribir("Ingrese nombre:");
                nombre = clienteSocket.Leer().Trim();

            } while (nombre == string.Empty);

            do
            {
                clienteSocket.Escribir("Ingrese detalle:");
                detalle = clienteSocket.Leer().Trim();
            } while (detalle == string.Empty || detalle.Length > 20);

            Mensaje m = new Mensaje()
            {
                Nombre = nombre,
                Detalle = detalle,
                Tipo = "TCP"
            };
            lock (dal)

                dal.Save(m);
                clienteSocket.CerrarConexion();
            }
        }
    }

