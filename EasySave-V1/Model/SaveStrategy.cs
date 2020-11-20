using System;

namespace NSModel
{
	public interface SaveStrategy {
		void Execute(SaveTemplate template);
	}

}
