using NSModel;
using NSView;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EasySave_V3.Properties;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NSModel.Singleton;

namespace NSController {
	public class Controller {

		private IView _View;
		private Model _model;
		private static Controller controller;

		/* Variables for Regex */
		private string error = "";
		private Regex nameForm = new Regex("^[^/\":*?\\<>|]+$");
		private Regex directoryName = new Regex(@"^([A-Za-z]:\\|\\)([^/:*?""\<>|]*\\)*[^/:*?""\<>|]*$");
		private static List<SaveTemplate> myTemplates;

		/* Variables for Socket */
		private static int maxUsers = 10;
		private static byte[] buffer = new byte[2048];
		private static List<Socket> clients = new List<Socket>();
		private static Socket server;

		public IView View
		{
			get => this._View;
			set => this._View = value;
		}

		public Model model
		{
			get => this._model;
			set => this._model = value;
		}
        public static List<SaveTemplate> MyTemplates { get => myTemplates; set => myTemplates = value; }

        /* Constructor */
        public Controller() {
			this.model = new Model();
			this.View = new GraphicalView(this);
			controller = this;
			server = Connection("127.0.0.1", 1234);
			server.BeginAccept(Accept, server);
			this.View.Start();
		}
		/* Method to create a save template */
		public void CreateSaveTemplate(string name, string srcDir, string destDir, int type) {
			Match nameMatch = nameForm.Match(name);
			Match srcDirNameMatch = directoryName.Match(srcDir);
			Match destDirNameMatch = directoryName.Match(destDir);
			bool isEqual = false;

			/* Checking if informations matches regex */
			if (nameMatch.Success && srcDirNameMatch.Success && destDirNameMatch.Success)
            {
				List<SaveTemplate> templates = model.templates;
				foreach(SaveTemplate template in templates)
                {
					if (template.backupName == name)
                    {
						error = Resources.InvalidSameName;
						PrintMessage(error, -1);
						isEqual = true;
					}
				}
                if (!isEqual)
                {
					try
					{
						model.CreateSaveTemplate(name, srcDir, destDir, type);
						PrintMessage(Resources.Success, 1);
					}
					catch (Exception err)
					{
						PrintMessage(err.Message, -1);
					}
				}
			}
            else
			{
				if(!nameMatch.Success)
					error = Resources.InvalidName + "\n";
				if(!srcDirNameMatch.Success)
					error += Resources.InvalidSrc + "\n";
				if(!destDirNameMatch.Success)
					error += Resources.InvalidDest;
				PrintMessage(error, -1);
            }
		}

		/* Method to delete a save template */
		public void DeleteSaveTemplate(int templateIndex) {
			try
			{
				model.DeleteSaveTemplate(templateIndex);
				PrintMessage(Resources.SuccessDel, 1);
			}
			catch (Exception err)
			{
				PrintMessage(err.Message, -1);
			}
		}

		/* Method to modify an existing save template */
		public void ModifySaveTemplate(int templateIndex, string name, string srcDir, string destDir, int type)
        {
			Match nameMatch = nameForm.Match(name);
			Match srcDirNameMatch = directoryName.Match(srcDir);
			Match destDirNameMatch = directoryName.Match(destDir);

			/* Checking if informations matches regex */
			if (nameMatch.Success && srcDirNameMatch.Success && destDirNameMatch.Success)
			{
				try
				{
					model.ModifySaveTemplate(templateIndex, name, srcDir, destDir, type);
					PrintMessage(Resources.SuccessModif, 1);
				}
				catch (Exception err)
				{
					PrintMessage(err.Message, -1);
				}
			} else
            {
				if (!nameMatch.Success)
					error = Resources.InvalidName + "\n";
				if (!srcDirNameMatch.Success)
					error += Resources.InvalidSrc + "\n";
				if (!destDirNameMatch.Success)
					error += Resources.InvalidDest;
				PrintMessage(error, -1);
			}
			
        }

		/* Method to execute one save */
		public void ExecuteOneSave(int templateIndex) {
			try
			{
				NotifyClients();
				List<string> extensionsToEncrypt = getExtensionsToEncrypt();
				model.ExecuteOneSave(templateIndex, extensionsToEncrypt);
				PrintMessage(Resources.SuccessExec, 1);
				//this.View.ChangeStatus
			}
			catch (Exception err)
			{
				PrintMessage(err.Message, -1);
			}
		}

		/* Method to execute all saves */
		public void ExecuteAllSave() {
			try
			{
				NotifyClients();
				List<string> extensionsToEncrypt = getExtensionsToEncrypt();
				model.ExecuteAllSave(extensionsToEncrypt);
				PrintMessage(Resources.SuccessExecAll, 1);
			}
			catch (Exception err)
			{
				PrintMessage(err.Message, -1);
			}
		}

		public void NotifyClients()
        {
			model.refreshStatusDelegate += (name, status) => {
				buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new JObject(new JProperty("title", "refreshStatus"), new JProperty("templateName", name), new JProperty("status", status))));
				foreach (Socket client in clients)
				{
					client.BeginSend(buffer, 0, buffer.Length, 0, SendCallback, client);
				}
			};
			model.refreshProgressDelegate += (name, progression) => {
				buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new JObject(new JProperty("title", "refreshProgress"), new JProperty("templateName", name), new JProperty("progress", progression))));
				foreach (Socket client in clients)
				{
					client.BeginSend(buffer, 0, buffer.Length, 0, SendCallback, client);
				}
			};
		}

		/* Method to get all existing templates */
		public List<SaveTemplate> GetAllTemplates()
		{
			return this.model.templates;
		}

		public void StopThread(int index)
        {
			model.StopThread(index);
        }

		public string PauseOrResume(int index, bool play)
        {
			return model.PauseOrResume(index, play);
        }

		/* Method to open logs */
		public void OpenLogs()
        {
			model.OpenLogs();
		}

		/* Method to print a popup message */
		public void PrintMessage(string message, int type)
        {
			View.PrintMessage(message, type);
        }

		/* Method to exit the program */
		public void Exit()
        {
			Environment.Exit(1);
		}

		/* Method to get all forbidden processes */
		public List<string> getForbiddenProcesses()
		{
			return model.GetForbiddenProcesses();
		}

		/* Method to get all extensions to encrypt */
		public List<string> getExtensionsToEncrypt()
		{
			return model.getExtensionsToEncrypt();
		}
		/* Method to get all priority files extensions */
		public List<string> getpriorityFilesExtensions()
		{
			return model.getPriorityFilesExtensions();
		}

		/* Method to get all priority files extensions */
		public int getMaxFileSize()
		{
			return Model.getMaxFileSize();
		}

		/* Method to add a forbidden process */
		public void addForbiddenProcess(string process)
		{
			model.addForbiddenProcess(process);
		}

		/* Method to add an extension to encrypt */
		public void addExtensionToEncrypt(string extension)
		{
			model.addExtensionToEncrypt(extension);
		}

		/* Method to add a priority file extension */
		public void addPriorityFilesExtension(string extension)
		{
			model.addPriorityFilesExtension(extension);
		}
		/* Method to add the max file size */
		public void addMaxFileSize(string size)
		{
			model.addMaxFileSize(size);
		}

		/* Method to remove a forbidden process */
		public void removeForbiddenProcess(int index)
        {
			model.removeForbiddenProcess(index);
        }

		/* Method to remove an extension to encrypt */
		public void removeExtensionToEncrypt(int index)
		{
			model.removeExtensionToEncrypt(index);
		}

		/* Method to remove a priority file extension */
		public void removePriorityFilesExtension(int index)
		{
			model.removePriorityFilesExtension(index);
		}

		  /***************************/
		 /******* Socket part *******/
		/***************************/

		private static Socket Connection(string ip, int port)
		{
			IPEndPoint adressPort = new IPEndPoint(IPAddress.Parse(ip), port);
			Socket serveurSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			serveurSocket.Bind(adressPort);
			serveurSocket.Listen(maxUsers);
			return serveurSocket;
		}

		private static void Accept(IAsyncResult ar)
		{
			Socket client = ((Socket)ar.AsyncState).EndAccept(ar);
			clients.Add(client);
			client = AccepterConnection(client);
			client.BeginReceive(buffer, 0, buffer.Length, 0, ListenNetwork, client);
			server.BeginAccept(Accept, server);
		}

		private static Socket AccepterConnection(Socket client)
		{
			Debug.WriteLine("Client connected");
			return client;
		}

		private static void ListenNetwork(IAsyncResult ar)
		{
			Socket client = (Socket)ar.AsyncState;
			int bytesRec = client.EndReceive(ar);
			if (bytesRec > 0)
			{
				string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);
				JObject received = JObject.Parse(data);
                switch (received["title"].ToString())
                {
					case "getAllTemplates":
						buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new JObject(new JProperty("title", "getAllTemplates"), new JProperty("templates", JsonConvert.SerializeObject(SaveTemplateConfig.GetInstance().GetTemplates())))));
						client.BeginSend(buffer, 0, buffer.Length, 0, SendCallback, client);
						Debug.WriteLine("Send save templates");
						break;
					case "executeOneSave":
						int index = JsonConvert.DeserializeObject<int>(received["index"].ToString());
						controller.ExecuteOneSave(index);
						break;

				}
				client.BeginReceive(buffer, 0, buffer.Length, 0, ListenNetwork, client);
			} else
            {
				Disconnect(client);
            }
		}

		private static void SendCallback(IAsyncResult ar)
		{
			try
			{
				Socket client = (Socket)ar.AsyncState;
				client.EndSend(ar);
			}
			catch
			{
				throw new Exception("Error in endsend");
			}
		}

		private static void Broadcast(IAsyncResult ar)
		{
			try
			{
				Socket client = (Socket)ar.AsyncState;
				int bytesSent = client.EndSend(ar);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		private static void Disconnect(Socket client)
		{
			Console.WriteLine("Client {0} disconnected from the server", client.RemoteEndPoint.ToString());
			clients.Remove(client);
			client.Close();
		}
	}
}
