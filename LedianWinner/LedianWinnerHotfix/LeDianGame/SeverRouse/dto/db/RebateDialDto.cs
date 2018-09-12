using System;
using System.Collections.Generic;
using ProtoBuf;
[ProtoContract]
public class RebateDialDto : SerializeMessage {
	[ProtoMember(1)]
	public List<int> position;
	[ProtoMember(2)]
	public List<ItemDto> items;
}
