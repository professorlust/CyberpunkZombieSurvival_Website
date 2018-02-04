﻿using System;
using System.Collections.Generic;
using System.Linq;
using CZS.Web.Attributes;
using CZS.Web.Constants;
using CZS.Web.Data;
using CZS.Web.Data.Entities;
using CZS.Web.Models;
using CZS.Web.Models.UI.QuestEditor;
using DotNetify;

namespace CZS.Web.ViewModels
{
    [RoleRequired(RoleType.Admin)]
    public class QuestEditorViewModel: BaseVM
    {
        private readonly DataContext _db;

        public string Quests_itemkey => nameof(QuestInfoUI.QuestID);
        public IEnumerable<QuestInfoUI> Quests
        {
            get => Get<IEnumerable<QuestInfoUI>>();
            set => Set(value);
        }
        
        public QuestDetailsUI ActiveQuest
        {
            get => Get<QuestDetailsUI>();
            set => Set(value);
        }

        public string KeyItems_itemkey => nameof(QuestKeyItemUI.KeyItemID);
        public IEnumerable<QuestKeyItemUI> KeyItems
        {
            get => Get<IEnumerable<QuestKeyItemUI>>();
            set => Set(value);
        }

        public QuestEditorViewModel(DataContext db)
        {
            _db = db;

            var quests = _db.Quests.OrderBy(o => o.Name)
                .Select(x => new QuestInfoUI
                {
                    Name = x.Name,
                    QuestID = x.QuestId
                }).ToList();

            QuestInfoUI selectOption = new QuestInfoUI()
            {
                Name = "Select a quest...",
                QuestID = -1
            };

            quests.Insert(0, selectOption);

            Quests = quests;
            ActiveQuest = new QuestDetailsUI{QuestID = -1};

            var keyItems = _db.KeyItems.OrderBy(x => x.Name)
                .Select(x => new QuestKeyItemUI
                {
                    Name = x.Name,
                    KeyItemID = x.KeyItemId
                }).ToList();

            QuestKeyItemUI selectKeyItem = new QuestKeyItemUI
            {
                Name = "None",
                KeyItemID = -1
            };
            keyItems.Insert(0, selectKeyItem);

            KeyItems = keyItems;
        }

        public Action<int> ChangeQuest => questID =>
        {
            if (questID <= -1)
            {
                ActiveQuest = new QuestDetailsUI { QuestID = -1 };
                return;
            }

            var quest = _db.Quests
                .SingleOrDefault(x => x.QuestId == questID);

            if (quest == null)
            {
                ActiveQuest = new QuestDetailsUI();
                return;
            }
            ActiveQuest = BuildQuestUIObject(quest);
        };

        private static QuestDetailsUI BuildQuestUIObject(Quests quest)
        {
            var uiQuest = new QuestDetailsUI
            {
                QuestID = quest.QuestId,
                Name = quest.Name,
                JournalTag = quest.JournalTag,
                FameRegionID = quest.FameRegionId,
                RequiredFameAmount = quest.RequiredFameAmount,
                AllowRewardSelection = quest.AllowRewardSelection,
                IsRepeatable = quest.IsRepeatable,
                MapNoteTag = quest.MapNoteTag,
                StartKeyItemID = quest.StartKeyItemId ?? -1,
                RemoveStartKeyItemAfterCompletion = quest.RemoveStartKeyItemAfterCompletion,
                Rewards = new QuestRewardsUI
                {
                    Fame = quest.RewardFame,
                    Gold = quest.RewardGold,
                    KeyItemID = quest.RewardKeyItemId ?? -1,
                    XP = quest.RewardXp,
                    RewardItems = quest.QuestRewardItems.Select(x => new QuestRewardItemUI()
                    {
                        Quantity = x.Quantity,
                        Resref = x.Resref
                    }).ToList()
                }
            };
            
            foreach (var prereq in quest.QuestPrerequisitesQuest)
            {
                uiQuest.PrerequisiteQuestIDs.Add(prereq.RequiredQuestId);
            }
            
            return uiQuest;
        }
    }
}
