namespace RPG_Test
{
    public partial class Form1 : Form
    {
        private Player _player;
        private Monster _currentMonster;

        public Form1()
        {
            InitializeComponent();

            _player = new Player(10, 10, 20, 0, 1);
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));

            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Use Weapon - использовать оружие
        private void button1_Click(object sender, EventArgs e) 
        {
            // Получаем выбранное в данный момент оружие из списка оружия cbo
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;
            // Определяем величину урона, который нужно нанести монстру
            int damageToMonster = RandomNumberGenerator.NumberBetween(currentWeapon.MinimumDamage, currentWeapon.MaximumDamage);
            // Применяем урон к текущим очкам жизни монстра
            _currentMonster.CurrentHitPoints -= damageToMonster;
            // Отображение сообщения
            rtbMessages.Text += "You hit the " + _currentMonster.Name + " for " + damageToMonster.ToString() + " points." + Environment.NewLine;
            // Проверяем, мертв ли ​​монстр
            if (_currentMonster.CurrentHitPoints <= 0)
            {
                // Монстр мертв
                rtbMessages.Text += Environment.NewLine;
                rtbMessages.Text += "You defeated the " + _currentMonster.Name + Environment.NewLine;
                // Даем игроку очки опыта за убийство монстра
                _player.ExperiencePoints += _currentMonster.RewardExperiencePoints;
                rtbMessages.Text += "You receive " + _currentMonster.RewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
                // Даем игроку золото за убийство монстра
                _player.Gold += _currentMonster.RewardGold;
                rtbMessages.Text += "You receive " + _currentMonster.RewardGold.ToString() + " gold" + Environment.NewLine;
                // Получаем случайную добычу от монстра
                List<InventoryItem> lootedItems = new List<InventoryItem>();
                // Добавляем предметы в список добытых предметов, сравнивая случайное число с процентом выпадения
                foreach (LootItem lootItem in _currentMonster.LootTable)
                {
                    if (RandomNumberGenerator.NumberBetween(1, 100) <= lootItem.DropPercentage)
                    {
                        lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                    }
                }
                // Если ни один предмет не был выбран случайным образом, добавьте предмет(ы) добычи по умолчанию.
                if (lootedItems.Count == 0)
                {
                    foreach (LootItem lootItem in _currentMonster.LootTable)
                    {
                        if (lootItem.IsDefaultItem)
                        {
                            lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                        }
                    }
                }
                // Добавляем добытые предметы в инвентарь игрока
                foreach (InventoryItem inventoryItem in lootedItems)
                {
                    _player.AddItemToInventory(inventoryItem.Details);
                    if (inventoryItem.Quantity == 1)
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.NamePlural + Environment.NewLine;
                    }
                }
                // Обновляем информацию об игроке и элементы управления инвентарем
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();
                lblGold.Text = _player.Gold.ToString();
                lblExperience.Text = _player.ExperiencePoints.ToString();
                lblLevel.Text = _player.Level.ToString();
                UpdateInventoryListInUI();
                UpdateWeaponListInUI();
                UpdatePotionListInUI();
                // Добавляем пустую строку в окно сообщений просто для внешнего вида.
                rtbMessages.Text += Environment.NewLine;
                // Перемещаем игрока в текущую локацию (чтобы исцелить игрока и создать нового монстра для боя)
                MoveTo(_player.CurrentLocation);
            }
            else
            {
                // Монстр все еще жив
                // Определяем количество урона, которое монстр наносит игроку
                int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentMonster.MaximumDamage);
                // Отображение сообщения
                rtbMessages.Text += "The " + _currentMonster.Name + " did " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
                // Вычитаем урон от игрока
                _player.CurrentHitPoints -= damageToPlayer;
                // Обновляем данные игрока в пользовательском интерфейсе
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();
                if (_player.CurrentHitPoints <= 0)
                {
                    // Отображение сообщения
                    rtbMessages.Text += "The " + _currentMonster.Name + " killed you." + Environment.NewLine;
                    // Перемещаем игрока в «Домой»
                    MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
                }
            }
        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            // Получаем выбранное в данный момент зелье из поля со списком
            HealingPotion potion = (HealingPotion)cboPotions.SelectedItem;
            // Добавляем количество исцеления к текущим очкам здоровья игрока
            _player.CurrentHitPoints = (_player.CurrentHitPoints + potion.AmountToHeal);
            // Текущие очки жизни не могут превышать максимальные очки жизни игрока
            if (_player.CurrentHitPoints > _player.MaximumHitPoints)
            {
                _player.CurrentHitPoints = _player.MaximumHitPoints;
            }
            // Удалить зелье из инвентаря игрока
            foreach (InventoryItem ii in _player.Inventory)
            {
                if (ii.Details.ID == potion.ID)
                {
                    ii.Quantity--;
                    break;
                }
            }
            // Отображение сообщения
            rtbMessages.Text += "You drink a " + potion.Name + Environment.NewLine;
            // Монстр получает очередь атаковать
            // Определяем количество урона, которое монстр наносит игроку
            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentMonster.MaximumDamage);
            // Отображение сообщения
            rtbMessages.Text += "The " + _currentMonster.Name + " did " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
            // Вычитаем урон от игрока
            _player.CurrentHitPoints -= damageToPlayer;
            if (_player.CurrentHitPoints <= 0)
            {
                // Отображение сообщения
                rtbMessages.Text += "The " + _currentMonster.Name + " killed you." + Environment.NewLine;
                // Перемещаем игрока в «Домой»
                MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            }
            // Обновляем данные игрока в пользовательском интерфейсе
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            UpdateInventoryListInUI();
            UpdatePotionListInUI();
        }

        private void MoveTo(Location newLocation)
        {
          
            if (!_player.HasRequiredItemToEnterThisLocation(newLocation))
            {
                 // Мы не нашли нужный предмет в их инвентаре, поэтому выводим сообщение и прекращаем попытки переместить
                 rtbMessages.Text += "You must have a " + newLocation.ItemRequiredToEnter.Name + " to enter this location." + Environment.NewLine;
                 return;
            }
            

            // Обновляем текущее местоположение игрока
            _player.CurrentLocation = newLocation;

            // Показать/скрыть доступные кнопки перемещения
            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnWest.Visible = (newLocation.LocationToWest != null);

            // Отображение названия и описания текущего местоположения
            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            // Полностью исцеляем игрока
            _player.CurrentHitPoints = _player.MaximumHitPoints;

            // Обновление очков жизни в пользовательском интерфейсе
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();

            // Есть ли в локации квест?
            if (newLocation.QuestAvailableHere != null)
            {
                // Проверяем, есть ли у игрока уже квест и выполнил ли он его
                bool playerAlreadyHasQuest = _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedQuest = _player.CompletedThisQuest(newLocation.QuestAvailableHere);

                // Проверяем, есть ли у игрока уже квест
                if (playerAlreadyHasQuest)
                {
                    // Если игрок еще не завершил квест
                    if (!playerAlreadyCompletedQuest)
                    {
                        // Проверяем, есть ли у игрока все предметы, необходимые для выполнения квеста
                        bool playerHasAllItemsToCompleteQuest = _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);


                        // У игрока есть все предметы, необходимые для выполнения квеста
                        if (playerHasAllItemsToCompleteQuest)
                        {
                            // Отображение сообщения
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You complete the '" + newLocation.QuestAvailableHere.Name + "' quest." + Environment.NewLine;

                            _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);

                            // Выдаем награды за квест
                            rtbMessages.Text += "You receive: " + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardGold.ToString() + " gold" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                            rtbMessages.Text += Environment.NewLine;

                            _player.ExperiencePoints += newLocation.QuestAvailableHere.RewardExperiencePoints;
                            _player.Gold += newLocation.QuestAvailableHere.RewardGold;

                            // Добавляем предмет-награду в инвентарь игрока
                            _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);

                            // Отмечаем квест как выполненный
                            _player.MarkQuestCompleted(newLocation.QuestAvailableHere);
                        }
                    }
                }

                else
                {
                    // У игрока еще нет квеста

                    // Отображение сообщений
                    rtbMessages.Text += "You receive the " + newLocation.QuestAvailableHere.Name + " quest." + Environment.NewLine;
                    rtbMessages.Text += newLocation.QuestAvailableHere.Description + Environment.NewLine;
                    rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
                    foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                    {
                        if (qci.Quantity == 1)
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                        }
                        else
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;

                    // Добавляем квест в список квестов игрока
                    _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
                }
            }

            // Есть ли в локации монстр?
            if (newLocation.MonsterLivingHere != null)
            {
                rtbMessages.Text += "You see a " + newLocation.MonsterLivingHere.Name + Environment.NewLine;

                // Создаём нового монстра, используя значения стандартного монстра из списка World.Monster
                Monster standardMonster = World.MonsterByID(newLocation.MonsterLivingHere.ID);

                _currentMonster = new Monster(standardMonster.ID, standardMonster.Name, standardMonster.MaximumDamage,
                    standardMonster.RewardExperiencePoints, standardMonster.RewardGold, standardMonster.CurrentHitPoints, standardMonster.MaximumHitPoints);

                foreach (LootItem lootItem in standardMonster.LootTable)
                {
                    _currentMonster.LootTable.Add(lootItem);
                }

                cboWeapons.Visible = true;
                cboPotions.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;
            }
            else
            {
                _currentMonster = null;

                cboWeapons.Visible = false;
                cboPotions.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;
            }

            // Обновляем список инвентаря игрока
            UpdateInventoryListInUI();
            // Обновляем список квестов игрока
            UpdateQuestListInUI();
            // Обновляем список оружия игрока
            UpdateWeaponListInUI();
            // Обновляем список зелий игрока
            UpdatePotionListInUI();
        }
        private void UpdateInventoryListInUI()
        {
            dgvInventory.RowHeadersVisible = false;
            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";
            dgvInventory.Rows.Clear();
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { inventoryItem.Details.Name, inventoryItem.Quantity.ToString() });
                }
            }
        }
        private void UpdateQuestListInUI()
        {
            dgvQuests.RowHeadersVisible = false;
            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";
            dgvQuests.Rows.Clear();
            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }
        }
        private void UpdateWeaponListInUI()
        {
            List<Weapon> weapons = new List<Weapon>();
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Weapon)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.Details);
                    }
                }
            }
            if (weapons.Count == 0)
            {
                // У игрока нет оружия, поэтому скройте список оружия и кнопку «Использовать»
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";
                cboWeapons.SelectedIndex = 0;
            }
        }
        private void UpdatePotionListInUI()
        {
            List<HealingPotion> healingPotions = new List<HealingPotion>();
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is HealingPotion)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        healingPotions.Add((HealingPotion)inventoryItem.Details);
                    }
                }
            }
            if (healingPotions.Count == 0)
            {
                // У игрока нет зелий, поэтому скройте список зелий и кнопку «Использовать»
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";
                cboPotions.SelectedIndex = 0;
            }
        }
    }
}