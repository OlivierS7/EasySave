using System;
using System.Collections.Generic;

namespace NSModel
{
	public interface SaveStrategy {
		public delegate void TemplateStatusDelegate(string status);
        public event TemplateStatusDelegate refreshStatusDelegate;
		public string PauseOrResume(bool play);
		void AbortExecution(bool isAbort);
		void Execute(SaveTemplate template, List<string> extensionsToEncrypt);
		void UpdateStatus(string status);
	}
}
