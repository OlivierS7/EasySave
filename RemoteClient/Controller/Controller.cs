using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RemoteClient.NSView;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NSModel;
using System.Collections.Generic;

namespace RemoteClient.NSController
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 2048;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
    public class Controller
    {
        private static bool disconnected = false;
        private IView _View;
        private Socket client;
        public static List<SaveTemplate> templates;
        private static IView view;
        public delegate void StatusDelegate(string name, string status);
        public static event StatusDelegate refreshStatusDelegate;
        public delegate void ProgressDelegate(string name, float progression);
        public static event ProgressDelegate refreshProgressDelegate;

        public List<SaveTemplate> GetTemplates()
        {
            return templates;
        }

        public IView View
        {
            get => this._View;
            set => this._View = value;
        }

        /* Constructor */
        public Controller()
        {
            this.View = new GraphicalView(this);
            Controller.view = this.View;
            this.View.Start();
        }

        /* Connect to the specified server */
        public void Connexion(string ipString, int portCommunication)
        {
            IPEndPoint pointTerminaison = new IPEndPoint(IPAddress.Parse(ipString), portCommunication);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(pointTerminaison);
                this.client = client;
                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReceiveCallback, state);
        }

        /* Method to send messages to server */
        public void Send(JObject myObject)
        {
            try
            {
                if(client != null && client.Connected)
                {
                    byte[] myBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(myObject));
                    client.BeginSend(myBuffer, 0, myBuffer.Length, 0, SendCallback, client);
                }
            } catch
            {
                throw new Exception("Error in beginsend");
            }
        }

        /* Callback method to send messages */
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
            } catch
            {
                throw new Exception("Error in endsend");
            }
        }

        /* Callback method to receive messages */
        private static void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            try
            {
                int bytesRec = client.EndReceive(ar);
                /* Checking for received message */
                if (bytesRec > 0)
                {
                    string data = Encoding.UTF8.GetString(state.buffer, 0, bytesRec);
                    JObject received = JObject.Parse(data);
                    string name;
                    string status;
                    float progress;
                    /* Actions depending on received message */
                    switch (received["title"].ToString())
                    {
                        case "getAllTemplates":
                            templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(received["templates"].ToString());
                            break;
                        case "refreshProgress":
                            name = (received["templateName"].ToString());
                            float.TryParse(received["progress"].ToString(), out progress);
                            refreshProgressDelegate?.Invoke(name, progress);
                            break;
                        case "refreshStatus":
                            name = (received["templateName"].ToString());
                            status = (received["status"].ToString());
                            refreshStatusDelegate?.Invoke(name, status);
                            break;
                    }
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReceiveCallback, state);
                }
            }
            catch
            {
                Disconnect(client, false);
            }
            
        }

        public Socket GetSocket()
        {
            return client;
        }

        /* Method to disconnect from server */
        public static void Disconnect(Socket client, bool local)
        {
            if (!disconnected)
            {
                disconnected = true;
                if(client != null)
                    client.Close();
                if(!local)
                    Controller.view.PrintMessage("Disconnected by remote host", -1);
                Environment.Exit(0);
            }
        }
    }
}
