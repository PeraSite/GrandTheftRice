using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PixelCrushers;
using UnityAtoms;
using UnityEngine;

namespace GrandTheftRice {
	public class AtomListSaver : Saver {
		public List<BaseAtomValueList> List;

		public override string RecordData() {
			var varDict = List.ToDictionary(
				GetAtomID,
				v => v.IList
			);
			var recordData = SaveSystem.Serialize(varDict);

			return recordData;
		}

		public override void ApplyData(string data) {
			if (data == null) return;

			var varDict = SaveSystem.Deserialize<Dictionary<string, IList>>(data);
			foreach (var variable in List) {
				var id = GetAtomID(variable);
				if (!varDict.ContainsKey(id)) continue;
				variable.Clear();
				foreach (var value in varDict[id]) {
					variable.Add(value);
				}
			}
		}

		public override void OnRestartGame() {
			foreach (var variable in List) {
				variable.Clear();
			}
		}

		private static string GetAtomID(BaseAtomValueList v) => v.name;
	}
}
