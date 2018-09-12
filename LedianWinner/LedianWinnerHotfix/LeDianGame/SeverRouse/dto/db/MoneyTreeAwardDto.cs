using System;
using System.Collections.Generic;
using ProtoBuf;
[ProtoContract]
public class MoneyTreeAwardDto : SerializeMessage {
	[ProtoMember(1)]
	public long award;
	[ProtoMember(2)]
	public long holdGold;
}
