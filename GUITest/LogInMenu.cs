using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

namespace GUITest
{
    public partial class LogInMenu : Form
    {
        public string Password = "admin"; // Hardcoded password. Will be used to determine credsMatch bool
        public string userName = "admin"; // Hardcoded username. Will be used to determine credsMatch bool

        public bool credsMatch = false; // Set the credsMatch bool to false
        
        public LogInMenu()
        {
            InitializeComponent();
            panel3.Visible = false; // Hide the admin panel at the start
            panel3.Enabled = false; // Disable the admin panel at the start
        }


        private void button2_Click(object sender, EventArgs e) // This is for the Close button
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation); // Ask user if they really want to quit
            
            if(dialogResult == DialogResult.Yes) // If Yes is clicked above then do this 
            {
                this.Close(); // Close the app
            }
        }

        private void button1_Click(object sender, EventArgs e)  // This is for the Log In button
        {
            if (textBox2.Text == userName && textBox1.Text == Password)  // check if the Password and Username fields match the hardcoded passwords
            {
                credsMatch = true; // If they do then set credsMatch to true (normally false)
            }

            if (credsMatch) // If the credentials match then run these 
            {
                DialogResult = MessageBox.Show("Credentials Match, Welcome admin!"); // if credsMatch = true, then display this

                LogInFormPanel.Hide(); // Hide LogInForm Panel containing the Log In buttons etc

                pictureBox1.Load("https://images.dog.ceo/breeds/terrier-kerryblue/n02093859_248.jpg"); // Load some URL before pressing the "Fetch URL" button

                panel3.Visible = true; // Show the admin panel
                panel3.Enabled = true; // Enable the admin panel
            } 

            else DialogResult = MessageBox.Show("Wrong Credentials."); // if credsMatch = false, then display this
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e) // This controls the T&C checkbox
        {
            button1.Enabled = checkBox1.Checked; // Only enable the Sign Up button if this is checked
        }

       

        private void panel3_Paint(object sender, PaintEventArgs e) // Manages the text at the top of the "admin panel"
        {
            label5.Text = $"Hello, {userName}"; // Get username from stored parameter with $ before string.
        }

        private async void button3_Click(object sender, EventArgs e) // Manages the Fetch Photo button
        {
            var APIUrl = new Uri("https://dog.ceo/api/breeds/image/random"); // Store the API url in APIUrl

            HttpClient client = new HttpClient(); // Assing HTTPClient to client
            client.BaseAddress = APIUrl; // Make the address be what's stored in APIUrl

            var result = await client.GetStringAsync(APIUrl); // IIRC acts like CURL. Store the response in result variable.

            dynamic DeserResults = JsonConvert.DeserializeObject(result); // turn it into easy to read JSON form for easier parsing. * Dynamic should be changed?

            string messageURL = DeserResults.message; // Put the URL into the messageURL variable
            string statusCode = DeserResults.status; // Put the status message to statusCode variable

            pictureBox1.LoadAsync(messageURL); // Load the image asynchronously 
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Make the picture from the API stretch instead of cropping one part of the image


            if (statusCode == "success") // If the API responds with "success" then run these
            {
                OnlineLabel.Text = "Working"; // Change or keep the text as working
                OnlineLabel.ForeColor = Color.LimeGreen; // Change the color to "Green"
            }

            else // If the API DOESN'T respond with "success" then run these
            {
                OnlineLabel.Text = "Not Working"; // Change the "Working" text into "Not Working"
                OnlineLabel.ForeColor = Color.Red; // Set the color from "Green" to "Red"
            }
        }
    }
}
