using System;
using System.IO;

namespace NSModel.Singleton {
	public class State {
		private static State state;
		private FileInfo file;

		private State() {
			throw new System.NotImplementedException("Not implemented");
		}
		public static State GetStateInstance() {
			throw new System.NotImplementedException("Not implemented");
		}
		private void InitWrite(SaveTemplate template, bool state, int totalFiles, int totalSize) {
			throw new System.NotImplementedException("Not implemented");
		}
		private void Write(int progress, int filesLeft, int sizeLeft) {
			throw new System.NotImplementedException("Not implemented");
		}
	}
}
