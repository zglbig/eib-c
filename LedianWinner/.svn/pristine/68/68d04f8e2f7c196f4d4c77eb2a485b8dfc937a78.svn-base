using System;
using System.Collections.Generic;
namespace org.zgl.datable{
	public class GiftBagAwardDataTable : DataTableMessage {
		public int Id(){
			return id;
		}
		public static GiftBagAwardDataTable get(int id){
			return StaticConfigMessage.Instance.get<GiftBagAwardDataTable>(typeof(GiftBagAwardDataTable),id);
		}
		public int id;
		public string describe;
		public int primary;
		public int intermediate;
		public int advanced;
		public List<GiftBagAwardModel> award;
		public void AfterInit(){}
	}
}
