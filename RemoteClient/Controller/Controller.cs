using RemoteClient.NSView;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteClient.NSController
{
    public class Controller
    {
        private IView _View;

        public IView View
        {
            get => this._View;
            set => this._View = value;
        }

        public Controller()
        {
            this.View = new GraphicalView(this);
            this.View.Start(); ;
        }

        public Socket Connexion(string ipString, int portCommunication)
        {
            IPEndPoint pointTerminaison = new IPEndPoint(IPAddress.Parse(ipString), portCommunication);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(pointTerminaison);
                return client;
            }
            catch (SocketException)
            {
                throw new Exception("Can't connect to the server please try again..");
            }
        }

        private static void EcouterReseau(Socket client)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReceiveCallback, state);
                while (true)
                {

                    byte[] data = Encoding.UTF8.GetBytes("test");
                    client.Send(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            int bytesRead = client.EndReceive(ar);
            if (bytesRead > 0)
            {
                state.sb.Clear();
                state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));
                if (state.sb.Length > 1)
                {
                    string msg = state.sb.ToString();
                    Console.WriteLine(msg);
                }
            }
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReceiveCallback, state);
        }

        private static void Deconnecter(Socket client)
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

    }

    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
}
