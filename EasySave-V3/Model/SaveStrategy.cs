using System;
using System.Collections.Generic;

namespace NSModel
{
	public interface SaveStrategy {
		void AbortExecution();
		void Execute(SaveTemplate template, List<string> extensionsToEncrypt);
	}

}
