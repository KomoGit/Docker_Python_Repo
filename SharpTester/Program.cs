using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading.Tasks.Sources;

namespace EdgeDriverSample
{
    class Program
    {
        static readonly EdgeDriver Driver = new();
        static readonly Dictionary<string, string> xPathMap = new();
        static void Main(string[] args)
        {
            //Sweetspot 180 ms
            const int THREAD_CONTROLLER = 500;
            const int USER_START = 22025;
            const int USER_END = 22126;
            #region xPath map
            xPathMap.Add("Answer", "//*[@id=\"answer_1_id\"]");
            xPathMap.Add("Next", "//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button[2]");
            xPathMap.Add("Exit", "//*[@id=\"root\"]/div/div[1]/div/ul/li[3]/a");
            xPathMap.Add("FINInput", "/html/body/div[3]/div/div[1]/div/div/div[2]/div[1]/div/input");
            xPathMap.Add("FINSubmitButton", "/html/body/div[3]/div/div[1]/div/div/div[2]/div[2]/div/button");
            #endregion

            Driver.Url = "INSERT URL";
            for (int userIndex = USER_START; userIndex <= USER_END; userIndex++)
            {
                FillInLogin(Driver, $"{userIndex}", "0522");
                Thread.Sleep(2000);
                ClickButton(Driver, xPathMap["Gray Next"]);
                Thread.Sleep(2000);
                //ANSWERS Loop
                for (int i = 1; i <= 60; i++)
                {
                    Console.WriteLine($"Question No: {i}");
                    Thread.Sleep(THREAD_CONTROLLER);
                    ClickButton(driver: Driver, xPathMap["Answer"]);
                    Thread.Sleep(THREAD_CONTROLLER);
                    if (i == 1)
                    {
                        ClickButton(driver: Driver, "//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button");
                    }
                    else
                    {
                        ClickButton(driver: Driver, $"//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button[2]");
                    }
                    Thread.Sleep(THREAD_CONTROLLER);
                }
                ClickButton(driver: Driver, xPathMap["Answer"]);
                Thread.Sleep(THREAD_CONTROLLER);
                ClickButton(driver: Driver, xPathMap["Next"]);
                Thread.Sleep(THREAD_CONTROLLER);
                FillInFIN(userIndex);
                Thread.Sleep(THREAD_CONTROLLER);
                ClickButton(Driver, xPathMap["FINSubmitButton"]);
                Thread.Sleep(THREAD_CONTROLLER);
                ClickButton(Driver, xPathMap["Exit"]);
                Thread.Sleep(THREAD_CONTROLLER);
            }  
        }

        private static void FillInLogin(EdgeDriver driver, string login, string paswd)
        {
            try
            {
                IWebElement element = driver.FindElement(By.Id("email"));
                element.SendKeys(login);
                element = driver.FindElement(By.Id("password"));
                element.SendKeys(paswd);
                element.Submit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void FillInFIN(int a)
        {
            try
            {
                IWebElement element = Driver.FindElement(By.XPath(xPathMap["FINInput"]));
                element.SendKeys(a.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }      
        }

        private static void ClickButton(EdgeDriver driver, string identifier)
        {
            try
            {
                IWebElement element = driver.FindElement(By.XPath(identifier));
                element.Click();
            }
            catch (Exception)
            {
                Console.WriteLine($"MALFUNCTION 54");
            }
        }
    }
}