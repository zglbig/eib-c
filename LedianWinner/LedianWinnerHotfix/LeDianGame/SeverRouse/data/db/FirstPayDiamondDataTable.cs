using System;
using System.Collections.Generic;
namespace org.zgl.datable{
	public class FirstPayDiamondDataTable : DataTableMessage {
		public int Id(){
			return id;
		}
		public static FirstPayDiamondDataTable get(int id){
			return StaticConfigMessage.Instance.get<FirstPayDiamondDataTable>(typeof(FirstPayDiamondDataTable),id);
		}
		public int id;
		public List<ItemDto> items;
		public void AfterInit(){}
	}
}
