using org.zgl.service.client.lottery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LedianWinnerHotfix.LeDianGame.Game.TTL
{
    class GameLotteryOperationRoomNotifyIml : GameLotteryOperationRoomNotify
    {
        /// <summary>
        /// 下注通知 nowBetPlayerNumber:当前下注人数,nowLotteryAllGold:当前房间下注总金额
        /// </summary>
        public override void betNotify(int nowBetPlayerNumber, long nowLotteryAllGold)
        {
            UITianTianLePanel uITianTianLePanel = MessageManager.GetInstance.GetUIDict<UITianTianLePanel>();
            if (uITianTianLePanel != null)
            {
                if (uITianTianLePanel.gameObject.activeSelf)
                {
                    uITianTianLePanel.UpdateRoomBetInfo(nowBetPlayerNumber, nowLotteryAllGold);
                }
            }
        }

        /// <summary>
        /// 房间有玩家财富变更同步更新，uid：变更得uid，dto：变更得财富
        /// </summary>
        public override void playerWeathUpdate(long uid, ItemListDto dto)
        {
            if (PlayerCache.loginInfo.uid == uid)
            {
                // PlayerCache.SetWealthUpdate(dto.items,true);
            }
        }

        /// <summary>
        /// 开奖结果
        /// </summary>
        /// <param name="dto"></param>
        public override void result(LotteryHistoryDto dto, int timer)
        {
            if (PlayerCache.historyList.Count >= 30)
            {
                PlayerCache.historyList.RemoveAt(0);
            }
        
            PlayerCache.historyList.Add(dto);

            UITianTianLePanel uiTTLPanel = PlayerCache.TianTianLeObj.GetComponent<UITianTianLePanel>();
            if (uiTTLPanel != null)
            {
                uiTTLPanel.ShowCardsResult(dto);
                uiTTLPanel.UpdateTrend(PlayerCache.historyList);

            }
        }

        /// <summary>
        /// 结算通知   bonus:中奖金额
        /// </summary>
        /// <param name="bonus"></param>
        public override void settleAccount(long bonus, int timer)
        {
            GameObject go = GameTools.Instance.GetObject("Prefabs/TianTianLe/TTLAwardPanel");
            GameObject obj = UnityEngine.GameObject.Instantiate(go);
            AHResultMono aHResultMono = obj.AddComponent<AHResultMono>();
            obj.transform.SetParent(PlayerCache.GetCanvas());
            aHResultMono.SelfWin(bonus);
        }

        /// <summary>
        /// 房间开始
        /// </summary>
        public override void start(int timer)
        {
            //收到NUll 开局 开始计时
            if (PlayerCache.TianTianLeObj != null)
            {
                UITianTianLePanel uiTTLPanel = PlayerCache.TianTianLeObj.GetComponent<UITianTianLePanel>();
                if (uiTTLPanel != null)
                {
                    if (uiTTLPanel.gameObject.activeSelf)
                    {
                        AudioManager.Instance.PlaySound("ttl_start");

                    }
                    uiTTLPanel.HideCard();
                    uiTTLPanel.StartTimeDown(timer - 10);
                }

            }


            UIZJHPanel zjhPanel = MessageManager.GetInstance.GetUIDict<UIZJHPanel>();
            if (zjhPanel != null)
            {
                if (zjhPanel.gameObject.activeSelf)
                {
                    zjhPanel.ShowAHTime(timer - 10);
                }

            }
            UIZJHPanel cheatPanel = MessageManager.GetInstance.GetUIDict<CheatZjhPanel>();
            if (cheatPanel != null)
            {
                if (cheatPanel.gameObject.activeSelf)
                {
                    cheatPanel.ShowAHTime(timer - 10);
                }
            }
        }
        /// <summary>
        /// 停止下注
        /// </summary>
        public override void stopBet(int timer)
        {
            UITianTianLePanel uiTTLPanel = PlayerCache.TianTianLeObj.GetComponent<UITianTianLePanel>();
            if (uiTTLPanel != null)
            {
                if (uiTTLPanel.gameObject.activeSelf)
                {
                    AudioManager.Instance.PlaySound("ttl_end");
                }
                uiTTLPanel.AHEnd();
            }
        }
    }
}
