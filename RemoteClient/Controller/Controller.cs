using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RemoteClient.NSView;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NSModel;
using System.Collections.Generic;
using System.Diagnostics;

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
        private IView _View;
        private Socket client;
        public static List<SaveTemplate> templates;
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

        public Controller()
        {
            this.View = new GraphicalView(this);
            this.View.Start();
        }

        public void Connexion(string ipString, int portCommunication)
        {
            IPEndPoint pointTerminaison = new IPEndPoint(IPAddress.Parse(ipString), portCommunication);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(pointTerminaison);
                this.client = client;
                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReceiveCallback, state);
            }
            catch (SocketException)
            {
                throw new Exception("Can't connect to the server please try again..");
            }
        }

        public void Send(JObject myObject)
        {
            try
            {
                byte[] myBuffer = new byte[2048];
                myBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(myObject));
                client.BeginSend(myBuffer, 0, myBuffer.Length, 0, SendCallback, client);
            } catch
            {
                throw new Exception("Error in beginsend");
            }
        }

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

        private static void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            int bytesRec = client.EndReceive(ar);
            if (bytesRec > 0)
            {
                string data = Encoding.UTF8.GetString(state.buffer, 0, bytesRec);
                JObject received = JObject.Parse(data);
                string name;
                string status;
                float progress;
                switch (received["title"].ToString())
                {
                    case "getAllTemplates":
                        templates = JsonConvert.DeserializeObject<List<SaveTemplate>>(received["templates"].ToString());
                        break;
                    case "refreshProgress":
                        name = JsonConvert.DeserializeObject<string>(received["templateName"].ToString());
                        progress = JsonConvert.DeserializeObject<float>(received["progress"].ToString());
                        refreshProgressDelegate?.Invoke(name, progress);
                        break;
                    case "refreshStatus":
                        name = JsonConvert.DeserializeObject<string>(received["templateName"].ToString());
                        status = JsonConvert.DeserializeObject<string>(received["status"].ToString());
                        refreshStatusDelegate?.Invoke(name, status);
                        break;
                }
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReceiveCallback, state);
            }
            
        }

        private static void Disconnect(Socket client)
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}
