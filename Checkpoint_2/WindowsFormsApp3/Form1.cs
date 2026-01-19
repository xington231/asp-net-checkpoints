using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Check(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://accounts.google.com/signin");
            Thread.Sleep(2000);
            var LoginInput = driver.FindElement(By.CssSelector("input[type='email']"));
            LoginInput.SendKeys("alinaslef238@gmail.com");

            var button = driver.FindElement(By.XPath("//button/span[text()='Далее']/.."));
            button.Click();
 
        }
    }
}
