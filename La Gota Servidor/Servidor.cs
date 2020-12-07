using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca.ClienteServidor;
using Biblioteca.Datos;
using Biblioteca.Entidades;

namespace La_Gota_Servidor
{
    public partial class Servidor : Form
    {
        public delegate void ClientCarrier(ConexionTcp conexionTcp);
        public event ClientCarrier OnClientConnected;
        public event ClientCarrier OnClientDisconnected;
        public delegate void DataRecieved(ConexionTcp conexionTcp, string data);
        public event DataRecieved OnDataRecieved;

        private TcpListener _tcpListener;
        private Thread _acceptThread;
        private List<ConexionTcp> connectedClients = new List<ConexionTcp>();

        
        public Servidor()
        {
            InitializeComponent();
        }

        private void StartServer()
        {
            OnDataRecieved += MensajeRecibido;
            OnClientConnected += ConexionRecibida;
            OnClientDisconnected += ConexionCerrada;

            EscucharClientes("127.0.0.1", 8900);
            Invoke(new Action(() => AgregarLog("Servidor Iniciado")));
        }

        private void StopServer()
        {
            _tcpListener.Stop();
            _acceptThread.Abort();
            connectedClients.Clear();
            label1.Text = "0";

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            Invoke(new Action(() => AgregarLog("Servidor Detenido")));
        }

        private void MensajeRecibido(ConexionTcp conexionTcp, string datos)
        {
            var paquete = new Paquete(datos);
            string comando = paquete.Comando;
            if (comando == "consulta")
            {
                string contenido = paquete.Contenido;
                List<string> valores = Formato.Deserializar(contenido);

                string nis="";
                string mes="";

                int iNis;
                int iMes;

                Invoke(new Action(() => nis = valores[0]));
                Invoke(new Action(() => mes = valores[1]));

                Invoke(new Action(() => AgregarLog("Colsulta monto Nis: " + nis)));

                iNis = int.Parse(nis);
                iMes = int.Parse(mes);

                Datos D = new Datos();
                List<Hidrometros> hidrometros = D.ObtenerHidrometros();
                string resultado = "No hay datos";
                bool mesExixte = false;
                int mesConsultado = iMes;
                int LecturaMes = 0;
                int LecuraMesAnterior = 0;
                int montoCategoria = 0;
                double monto;

                foreach (HistorialConsumo dato in D.ObtenerLecturas(iNis))
                {
                    if (dato.MES == iMes)
                    {
                        LecturaMes = dato.LECTURA;
                        mesExixte = true;
                    }
                    if (dato.MES < iMes)
                    {
                        LecuraMesAnterior = dato.LECTURA;
                    }
                }

                foreach (Hidrometros dato in hidrometros)
                {
                    if (dato.NIS == iNis)
                    {
                        montoCategoria = dato.CATEGORIA.MONTO;
                    }
                }

                if (mesExixte == true)
                {
                    monto = (((LecturaMes - LecuraMesAnterior) * montoCategoria) * 1.13);
                    resultado = monto.ToString();
                }

                Invoke(new Action(() => AgregarLog("Monto enviado al cliente: "+ resultado)));

                var msgPack = new Paquete("resultado", resultado);
                conexionTcp.EnviarPaquete(msgPack);
            }

        }

        private void ConexionRecibida(ConexionTcp conexionTcp)
        {
            lock (connectedClients)
                if (!connectedClients.Contains(conexionTcp))
                    connectedClients.Add(conexionTcp);
            Invoke(new Action(() => label1.Text = string.Format("{0}", connectedClients.Count)));
            Invoke(new Action(() => AgregarLog("Clientes conectado")));
        }

        private void ConexionCerrada(ConexionTcp conexionTcp)
        {
            lock (connectedClients)
                if (connectedClients.Contains(conexionTcp))
                {
                    int cliIndex = connectedClients.IndexOf(conexionTcp);
                    connectedClients.RemoveAt(cliIndex);
                }
            Invoke(new Action(() => label1.Text = string.Format("{0}", connectedClients.Count)));
            Invoke(new Action(() => AgregarLog("Clientes Desconectado")));


        }

        private void EscucharClientes(string ipAddress, int port)
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
                _tcpListener.Start();
                _acceptThread = new Thread(AceptarClientes);
                _acceptThread.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        private void AceptarClientes()
        {
            do
            {
                try
                {
                    var conexion = _tcpListener.AcceptTcpClient();
                    var srvClient = new ConexionTcp(conexion)
                    {
                        ReadThread = new Thread(LeerDatos)
                    };
                    srvClient.ReadThread.Start(srvClient);

                    if (OnClientConnected != null)
                        OnClientConnected(srvClient);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }

            } while (true);
        }

        private void LeerDatos(object client)
        {
            var cli = client as ConexionTcp;
            var charBuffer = new List<int>();

            do
            {
                try
                {
                    if (cli == null)
                        break;
                    if (cli.StreamReader.EndOfStream)
                        break;
                    int charCode = cli.StreamReader.Read();
                    if (charCode == -1)
                        break;
                    if (charCode != 0)
                    {
                        charBuffer.Add(charCode);
                        continue;
                    }
                    if (OnDataRecieved != null)
                    {
                        var chars = new char[charBuffer.Count];
                        //Convert all the character codes to their representable characters
                        for (int i = 0; i < charBuffer.Count; i++)
                        {
                            chars[i] = Convert.ToChar(charBuffer[i]);
                        }
                        //Convert the character array to a string
                        var message = new string(chars);

                        //Invoke our event
                        OnDataRecieved(cli, message);
                    }
                    charBuffer.Clear();
                }
                catch (IOException)
                {
                    break;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());

                    break;
                }
            } while (true);

            if (OnClientDisconnected != null)
                OnClientDisconnected(cli);
        }

        private void Servidor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void AgregarLog(string sLog)
        {
            string cadena = "";

            cadena += DateTime.Now + " - " + sLog + Environment.NewLine;
            txtLog.Text = cadena + txtLog.Text; 

        }

        private void btnRegCliente_Click(object sender, EventArgs e)
        {
            StartServer();
            btnStart.Enabled = false;
            btnStop.Enabled = true;

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }
    }
}
