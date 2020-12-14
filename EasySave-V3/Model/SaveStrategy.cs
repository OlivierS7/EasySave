using System;
using System.Collections.Generic;

namespace NSModel
{
	public interface SaveStrategy {
		public string PauseOrResume(bool play);
		void AbortExecution(bool isAbort);
		void Execute(SaveTemplate template, List<string> extensionsToEncrypt);
	}

}
