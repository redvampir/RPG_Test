using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Test
{
    public class Player: LivingCreature
    {
        public int Gold { get; set; }
        public int ExperiencePoints { get; set;}
        public int Level
        {
            get { return ((ExperiencePoints / 100) + 1); }
        }
        public Location CurrentLocation { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }

        public Player(int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints) : base(currentHitPoints, maximumHitPoints)
        {
            Gold = gold;
            ExperiencePoints = experiencePoints;
            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if (location.ItemRequiredToEnter == null)
            {
                // Для этого местоположения нет обязательного элемента, поэтому возвращаем «true»
                return true;
            }

            // Проверяем, есть ли у игрока в инвентаре нужный предмет
            return Inventory.Exists(ii => ii.Details.ID == location.ItemRequiredToEnter.ID);
        }

        public bool HasThisQuest(Quest quest)
        {
            //foreach (PlayerQuest playerQuest in Quests)
            //{
            //    if (playerQuest.Details.ID == quest.ID)
            //    {
            //        return true;
            //    }
            //}

            //return false;
            // Пробник, научиться создавать
            return Quests.Exists(playerQuest => playerQuest.Details.ID == quest.ID);
        }

        public bool CompletedThisQuest(Quest quest)
        {
            //foreach (PlayerQuest playerQuest in Quests)
            //{
            //    if (playerQuest.Details.ID == quest.ID)
            //    {
            //        return playerQuest.IsCompleted;
            //    }
            //}

            PlayerQuest playrQuest = Quests.SingleOrDefault(q => q.Details.ID == quest.ID);
            //                  это типо если. : если условие выполнено 
            return playrQuest == null ? false: playrQuest.IsCompleted;
        }

        public bool HasAllQuestCompletionItems(Quest quest)
        {
            // Здесь можно проверить, есть ли у игрока все предметы, необходимые для выполнения квеста
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                //bool foundItemInPlayersInventory = false;
                
                // Проверяем каждый предмет в инвентаре игрока, чтобы узнать, есть ли он у него и достаточно ли его
                //foreach (InventoryItem ii in Inventory)
                //{
                //    if (ii.Details.ID == qci.Details.ID) // У игрока есть предмет в инвентаре
                //    {
                //        foundItemInPlayersInventory = true;

                //        if (ii.Quantity < qci.Quantity) // У игрока недостаточно этого предмета для выполнения квеста
                //        {
                //            return false;
                //        }
                //    }
                //}
                // У игрока в инвентаре нет ни одного предмета для завершения квеста
                if (!Inventory.Exists(ii=> ii.Details.ID == qci.Details.ID && ii.Quantity < qci.Quantity))
                {
                    return false;
                }
            }

            // Если мы сюда попали, то у игрока должны быть все необходимые предметы, и в достаточном количестве, чтобы выполнить квест.
            return true;
        }

        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                //foreach (InventoryItem ii in Inventory)
                //{
                //    if (ii.Details.ID == qci.Details.ID)
                //    {
                //        // Вычитаем из инвентаря игрока количество, необходимое для выполнения квеста
                //        ii.Quantity -= qci.Quantity;
                //        break;
                //    }
                //}
                InventoryItem item = Inventory.SingleOrDefault( ii=> ii.Details.ID == qci.Details.ID );
                if (item != null)
                {
                    // Вычтите из инвентаря игрока количество, необходимое для выполнения квеста.
                    item.Quantity -= qci.Quantity;
                }
            }
        }

        public void AddItemToInventory(Item itemToAdd)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToAdd.ID);
            if (item == null)
            {
                // They didn't have the item, so add it to their inventory, with a quantity of 1
                Inventory.Add(new InventoryItem(itemToAdd, 1));
            }
            else
            {
                // They have the item in their inventory, so increase the quantity by one
                item.Quantity++;
            }
        }

        public void MarkQuestCompleted(Quest quest)
        {
            // Find the quest in the player's quest list
            PlayerQuest playerQuest = Quests.SingleOrDefault(pq => pq.Details.ID == quest.ID);
            if (playerQuest != null)
            {
                playerQuest.IsCompleted = true;
            }
        }

    }
}
