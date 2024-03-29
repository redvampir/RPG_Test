﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Test
{
    public static class World
    {
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Monster> Monsters = new List<Monster>();
        public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<Location> Locations = new List<Location>();
        public const int ITEM_ID_RUSTY_SWORD = 1;
        public const int ITEM_ID_RAT_TAIL = 2;
        public const int ITEM_ID_PIECE_OF_FUR = 3;
        public const int ITEM_ID_SNAKE_FANG = 4;
        public const int ITEM_ID_SNAKESKIN = 5;
        public const int ITEM_ID_CLUB = 6;
        public const int ITEM_ID_HEALING_POTION = 7;
        public const int ITEM_ID_SPIDER_FANG = 8;
        public const int ITEM_ID_SPIDER_SILK = 9;
        public const int ITEM_ID_ADVENTURER_PASS = 10;
        public const int ITEM_ID_WOLFSKIN = 11;
        public const int ITEM_ID_WOLF_FANG = 12;
        public const int ITEM_ID_OLD_SWORD = 13;
        public const int MONSTER_ID_RAT = 1;
        public const int MONSTER_ID_SNAKE = 2;
        public const int MONSTER_ID_GIANT_SPIDER = 3;
        public const int MONSTER_ID_WOLF = 4;
        public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
        public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;
        public const int QUEST_ID_HUNTER_S_ERRAND = 3;
        public const int LOCATION_ID_HOME = 1;
        public const int LOCATION_ID_TOWN_SQUARE = 2;
        public const int LOCATION_ID_GUARD_POST = 3;
        public const int LOCATION_ID_ALCHEMIST_HUT = 4;
        public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5;
        public const int LOCATION_ID_FARMHOUSE = 6;
        public const int LOCATION_ID_FARM_FIELD = 7;
        public const int LOCATION_ID_BRIDGE = 8;
        public const int LOCATION_ID_SPIDER_FIELD = 9;
        public const int LOCATION_ID_THE_DARK_FOREST = 10;
        public const int LOCATION_ID_HUNTER_S_HUT = 11;

        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuests();
            PopulateLocations();
        }

        private static void PopulateItems()
        {
            Items.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Rusty sword", "Rusty swords", 0, 5));
            Items.Add(new Item(ITEM_ID_RAT_TAIL, "Rat tail", "Rat tails"));
            Items.Add(new Item(ITEM_ID_PIECE_OF_FUR, "Piece of fur", "Pieces of fur"));
            Items.Add(new Item(ITEM_ID_SNAKE_FANG, "Snake fang", "Snake fangs"));
            Items.Add(new Item(ITEM_ID_SNAKESKIN, "Snakeskin", "Snakeskins"));
            Items.Add(new Weapon(ITEM_ID_CLUB, "Club", "Clubs", 3, 10));
            Items.Add(new HealingPotion(ITEM_ID_HEALING_POTION, "Healing potion", "Healing potions", 5));
            Items.Add(new Item(ITEM_ID_SPIDER_FANG, "Spider fang", "Spider fangs"));
            Items.Add(new Item(ITEM_ID_SPIDER_SILK, "Spider silk", "Spider silks"));
            Items.Add(new Item(ITEM_ID_ADVENTURER_PASS, "Adventurer pass", "Adventurer passes"));
            Items.Add(new Item(ITEM_ID_WOLFSKIN, "Wolfskin", "Wolfskins"));
            Items.Add(new Item(ITEM_ID_WOLF_FANG, "Wolf fang", "Wolf fangs"));
            Items.Add(new Weapon(ITEM_ID_OLD_SWORD, "Old sword", "Old swords", 5, 15));
        }

        public static Item ItemByID(int id)
        {
            foreach (Item item in Items)
            {
                if(item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }
        private static void PopulateMonsters()
        {
            Monster rat = new Monster(MONSTER_ID_RAT, "Rat", 5, 3, 10, 3, 3);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_PIECE_OF_FUR), 75, true));
            
            Monster snake = new Monster(MONSTER_ID_SNAKE, "Snake", 5, 3, 10, 3, 3);
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 75, false));
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 75, true));
            
            Monster giantSpider = new Monster(MONSTER_ID_GIANT_SPIDER, "Giant spider", 20, 5, 40, 10, 10);
            giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_FANG), 75, true));
            giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_SILK), 25, false));
            //Сделанный мной волк.
            Monster wolf = new Monster(MONSTER_ID_WOLF, "Wolf", 10, 4, 20, 10, 10);
            wolf.LootTable.Add(new LootItem(ItemByID(ITEM_ID_WOLFSKIN), 75, true));
            wolf.LootTable.Add(new LootItem(ItemByID(ITEM_ID_WOLF_FANG), 25, true));

            Monsters.Add(rat);
            Monsters.Add(snake);
            Monsters.Add(giantSpider);
            Monsters.Add(wolf);
        }

        private static void PopulateQuests()
        {
            Quest clearAlchemistGarden =
                new Quest(
                    QUEST_ID_CLEAR_ALCHEMIST_GARDEN,
                    "Clear the alchemist's garden",
                    "Kill rats in the alchemist's garden and bring back 3 rat tails. You will receive a healing potion and 10 gold pieces.", 20, 10);
            clearAlchemistGarden.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_RAT_TAIL), 3));
            clearAlchemistGarden.RewardItem = ItemByID(ITEM_ID_HEALING_POTION);
            Quests.Add(clearAlchemistGarden);
            
            Quest clearFarmersField =
                new Quest(
                    QUEST_ID_CLEAR_FARMERS_FIELD,
                    "Clear the farmer's field",
                    "Kill snakes in the farmer's field and bring back 3 snake fangs. You will receive an adventurer's pass and 20 gold pieces.", 20, 20);
            clearFarmersField.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SNAKE_FANG), 3));
            clearFarmersField.RewardItem = ItemByID(ITEM_ID_ADVENTURER_PASS);
            Quests.Add(clearFarmersField);

            //TODO сделать волчий квест 
            Quest hunterSErrand =
                new Quest(
                    QUEST_ID_HUNTER_S_ERRAND,
                    "Kill dangerous wolves",
                    "Go into the dark forest and kill two wolves. Bring their skins as proof. For this you will receive an old sword and 20 gold pieces.", 30, 20);
            hunterSErrand.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_WOLFSKIN), 2));
            hunterSErrand.RewardItem = ItemByID(ITEM_ID_OLD_SWORD);
            Quests.Add(hunterSErrand);

        }
        private static void PopulateLocations()
        {
            // Создаём каждую локацию
            Location home = new Location(LOCATION_ID_HOME, "Home", "Your house. You really need to clean up the place.");
            Location townSquare = new Location(LOCATION_ID_TOWN_SQUARE, "Town square", "You see a fountain.");
            Location alchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Alchemist's hut", "There are many strange plants on the shelves.");
            alchemistHut.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);
            Location alchemistsGarden = new Location(LOCATION_ID_ALCHEMISTS_GARDEN, "Alchemist's garden", "Many plants are growing here.");
            alchemistsGarden.MonsterLivingHere = MonsterByID(MONSTER_ID_RAT);
            Location farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Farmhouse", "There is a small farmhouse, with a farmer in front.");
            farmhouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD);
            Location farmersField = new Location(LOCATION_ID_FARM_FIELD, "Farmer's field", "You see rows of vegetables growing here.");
            farmersField.MonsterLivingHere = MonsterByID(MONSTER_ID_SNAKE);
            Location guardPost = new Location(LOCATION_ID_GUARD_POST, "Guard post", "There is a large, tough-looking guard here.", ItemByID(ITEM_ID_ADVENTURER_PASS));
            Location bridge = new Location(LOCATION_ID_BRIDGE, "Bridge", "A stone bridge crosses a wide river.");
            Location spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Forest", "You see spider webs covering covering the trees in this forest.");
            spiderField.MonsterLivingHere = MonsterByID(MONSTER_ID_GIANT_SPIDER);
            //Сделана мной локация
            Location theDarkForest = new Location(LOCATION_ID_THE_DARK_FOREST, "The Dark Forest", "You see a big black wolf with glowing red eyes.");
            theDarkForest.MonsterLivingHere = MonsterByID(MONSTER_ID_WOLF);
            Location hunterSHut = new Location(LOCATION_ID_HUNTER_S_HUT, "Hunter's hut", "You see a small hut near which a hunter is sitting on a bench.");
            hunterSHut.QuestAvailableHere = QuestByID(QUEST_ID_HUNTER_S_ERRAND);

            // Соедините локации вместе
            home.LocationToNorth = townSquare;
            townSquare.LocationToNorth = alchemistHut;
            townSquare.LocationToSouth = home;
            townSquare.LocationToEast = guardPost;
            townSquare.LocationToWest = farmhouse;
            farmhouse.LocationToEast = townSquare;
            farmhouse.LocationToWest = farmersField;
            farmersField.LocationToEast = farmhouse;
            alchemistHut.LocationToSouth = townSquare;
            alchemistHut.LocationToNorth = alchemistsGarden;
            alchemistHut.LocationToEast = theDarkForest;
            alchemistHut.LocationToWest = hunterSHut;
            alchemistsGarden.LocationToSouth = alchemistHut;
            guardPost.LocationToEast = bridge;
            guardPost.LocationToWest = townSquare;
            bridge.LocationToWest = guardPost;
            bridge.LocationToEast = spiderField;
            spiderField.LocationToWest = bridge;
            // Соединял локации
            theDarkForest.LocationToWest = alchemistHut;
            theDarkForest.LocationToSouth = guardPost;
            hunterSHut.LocationToEast = alchemistHut;

            //Добавьте местоположения в статический список
            Locations.Add(home);
            Locations.Add(townSquare);
            Locations.Add(guardPost);
            Locations.Add(alchemistHut);
            Locations.Add(alchemistsGarden);
            Locations.Add(farmhouse);
            Locations.Add(farmersField);
            Locations.Add(bridge);
            Locations.Add(spiderField);
            Locations.Add(theDarkForest);
            Locations.Add(hunterSHut);
        }

        public static Monster MonsterByID(int id)
        {
            foreach (Monster monster in Monsters)
            {
                if (monster.ID == id)
                {
                    return monster;
                }
            }
            return null;
        }
        public static Quest QuestByID(int id)
        {
            foreach (Quest quest in Quests)
            {
                if (quest.ID == id)
                {
                    return quest;
                }
            }
            return null;
        }
        public static Location LocationByID(int id)
        {
            foreach (Location location in Locations)
            {
                if (location.ID == id)
                {
                    return location;
                }
            }
            return null;
        }
    }
}
