namespace RPG_Test
{
    public partial class Form1 : Form
    {
        private Player _player;

        public Form1()
        {
            InitializeComponent();
            _player = new Player(10, 10, 20, 0, 1);


            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Use Weapon
        private void button1_Click(object sender, EventArgs e) 
        {

        }

        private void btnNorth_Click(object sender, EventArgs e)
        {

        }

        private void btnEast_Click(object sender, EventArgs e)
        {

        }

        private void btnSouth_Click(object sender, EventArgs e)
        {

        }

        private void btnWest_Click(object sender, EventArgs e)
        {

        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {

        }
    }
}